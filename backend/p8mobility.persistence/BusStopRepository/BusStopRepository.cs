using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using p8_shared;
using p8mobility.persistence.Connection;

namespace p8mobility.persistence.BusStopRepository;

public class BusStopRepository : IBusStopRepository
{
    private static readonly string TableName = "BusStops";
    private readonly IDbConnectionFactory _connectionFactory;
    private IDbConnection Connection => _connectionFactory.Connection;

    public BusStopRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> UpdatePeopleCount(Guid id, int peopleCount)
    {
        var query = $@"
            UPDATE {TableName}
            SET PeopleCount = @PeopleCount, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var parameters = new
        {
            Id = id,
            PeopleCount = peopleCount,
            UpdatedAt = DateTime.UtcNow
        };
        return await Connection.ExecuteAsync(query, parameters) > 0;

    }

    public async Task<bool> UpsertBusStop(Guid id, decimal latitude, decimal longitude)
    {
        var query = $@"
            INSERT INTO {TableName} (Id, Latitude, Longitude, UpdatedAt)
            VALUES (@Id, @Latitude, @Longitude, @UpdatedAt)";
        
        var parameters = new
        {
            Id = id,
            Latitude = latitude,
            Longitude = longitude,
            UpdatedAt = DateTime.UtcNow
        };
        return await Connection.ExecuteAsync(query, parameters) > 0;
    }

    public async Task<bool> DeleteBusStop(Guid id)
    {
        var query = $@"
            DELETE FROM {TableName}
            WHERE Id = @Id";

        var parameters = new
        {
            Id = id
        };
        return await Connection.ExecuteAsync(query, parameters) > 0;
    }

    public async Task<BusStop> GetPeopleCountFromId(Guid id)
    {
        var query = $@"
            SELECT *
            FROM {TableName}
            WHERE Id = @Id";

        var parameters = new
        {
            Id = id
        };
        return await Connection.QueryFirstOrDefaultAsync<BusStop>(query, parameters);
    }

    public async Task<List<BusStop>> GetAllBusStops()
    {
        var query = $@"
            SELECT *
            FROM {TableName}";

        var res = await Connection.QueryAsync<BusStop>(query);
        return res.AsList();
    }

    public async Task<BusStop> GetBusStopFromId(Guid id)
    {
        var query = $@"
            SELECT *
            FROM {TableName}
            WHERE Id = @Id";

        var parameters = new
        {
            Id = id
        };
        return await Connection.QueryFirstOrDefaultAsync<BusStop>(query, parameters);
    }
}