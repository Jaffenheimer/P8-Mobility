using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using p8_restapi.PusherService;
using p8_restapi.Requests;
using p8_restapi.StateController;
using p8_shared;
using p8mobility.persistence.BusRepository;
using p8mobility.persistence.BusStopRepository;
using p8mobility.persistence.RouteRelationsRepository;
using Action = p8_shared.Action;

namespace p8_restapi.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly IBusStopRepository _busStopRepository;
    private readonly IBusRepository _busRepository;
    private readonly IRouteRelationsRepository _routeRelationsRepository;
    private readonly IPusherService _pusherService;
    
    public AdminController(IBusStopRepository busStopRepository,
        IBusRepository busRepository, IRouteRelationsRepository routeRelationsRepository, IPusherService pusherService)
    {
        _busStopRepository = busStopRepository;
        _busRepository = busRepository;
        _routeRelationsRepository = routeRelationsRepository;
        _pusherService = pusherService;
    }



    [HttpPost("startdemo")]
    public async Task<IActionResult> StartDemo()
    {

        Program._stateController.Run();

        return Ok();
    }

    /// <summary>
    /// Creates an Instance of a bus in the system
    /// </summary>
    /// <param name="req"></param>
    /// <returns>Ok if successful otherwise bad request</returns>
    [HttpPost("bus")]
    public async Task<IActionResult> CreateBus([FromBody] CreateBusRequest req)
    {
        var routeId = await _routeRelationsRepository.GetRouteFromPassword(req.Password);
        if (routeId == Guid.Empty || routeId == null)
            return BadRequest("Could not log in");

        if (Program._stateController.BussesInitialized >= 9)
        {
            return BadRequest("Maximum amount of busses initialized");
        }
        
        var ids = Program._stateController.DummyBusIds.Values.ToList();
        var bus = new Bus(req.Latitude, req.Longitude, ids[Program._stateController.BussesInitialized], routeId.Value);
        Program._stateController.BussesInitialized +=1;
        
        
        var res = await _busRepository.Upsert(bus.Id, routeId.Value, bus.Latitude, bus.Longitude, Action.Default);
        if (!res)
            return BadRequest("Could not log in");
        Program._stateController.AddBus(bus);
        return Ok(bus.Id);
    }

    /// <summary>
    /// Creates a bus stop
    /// </summary>
    /// <param name="req"></param>
    /// <returns>Ok if bus stop is created otherwise bad request</returns>
    [HttpPost("busStop")]
    public async Task<IActionResult> CreateBusStop([FromBody] CreateBusStopRequest req)
    {
        var res = await _busStopRepository.UpsertBusStop(Guid.NewGuid(), req.Latitude, req.Longitude);
        if (!res)
            return BadRequest("Could not create bus stop");

        return Ok("Bus stop created successfully");
    }

    /// <summary>
    /// Creates a bus route
    /// </summary>
    /// <param name="req"></param>
    /// <returns>Ok if bus route is created otherwise bad request</returns>
    [HttpPost("route")]
    public async Task<IActionResult> CreateRoute([FromBody] CreateRouteRequest req)
    {
        var res = await _routeRelationsRepository.UpsertRoute(Guid.NewGuid(), req.Password, req.BusStopIds);
        if (!res)
            return BadRequest("Could not create route");

        return Ok("Route created successfully");
    }

    /// <summary>
    /// Updates bus to be at a new location
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="busId"></param>
    /// <returns>Ok with confirmation of where bus is</returns>
    [HttpPost("bus/location")]
    public async Task<IActionResult> UpdateBusLocation([FromBody] UpdateBusLocationRequest req)
    {
        var res = await Program._stateController.UpdateBusLocation(req.BusId, req.Latitude, req.Longitude,
            _busRepository);

        if (res)
            return Ok($"Bus with id {req.BusId} was updated to location: {req.Latitude}, {req.Longitude}");

        return BadRequest("Could not update bus location");
    }

    /// <summary>
    /// Retrieves the calculated action for a specific bus
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Action</returns>
    [HttpGet("bus/action")]
    public Task<IActionResult> BusAction(Guid id)
    {
        var res = Program._stateController.GetBus(id);
        return Task.FromResult<IActionResult>(Ok(res));
    }

    /// <summary>
    /// Shutdown/Deletes bus
    /// </summary>
    /// <param name="req"></param>
    /// <returns>Ok with confirmation of where id of bus</returns>
    [HttpDelete("bus/delete")]
    public async Task<IActionResult> DeleteBus([FromBody] DeleteBusRequest req)
    {
        Program._stateController.DeleteBus(req.BusId);

        if (await _busRepository.DeleteBus(req.BusId))
            return Ok($"Bus with id {req.BusId} was deleted");

        return BadRequest("Could not delete bus");
    }

    /// <summary>
    /// Updates the amount of people at a bus stop
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="busStopId"></param>
    /// <returns>Ok with confirmation of what bus stop is updated with how many people</returns>
    [HttpPost("people/amount")]
    public async Task<IActionResult> UpdatePeopleAmount([FromBody] UpdatePeopleCountRequest req)
    {
        await _busStopRepository.UpdatePeopleCount(req.Id, req.PeopleCount);
        Program._stateController.UpdatePeopleCount(req.Id, req.PeopleCount);
        return Ok($"Successfully updated people amount on bus stop with id: {req.Id} to {req.PeopleCount}");
    }

    [HttpPost("Pusher/Test")]
    public async Task<IActionResult> TestPusher([FromBody]PusherRequest req)
    {
        var dic = new Dictionary<Guid, Action>();
        for (var i = 0; i < req.Ids.Count; i++)
        {
            dic.Add(req.Ids[i], req.Actions[i]);
        }
        var msg = new PusherMessage(dic);
        _pusherService.PublishAction("action", "test_event", msg);
        return Ok("Pusher test sent");
    }
}