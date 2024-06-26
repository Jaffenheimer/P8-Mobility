using Dapper;
using Microsoft.Extensions.DependencyInjection;
using p8mobility.persistence.BusRepository;
using p8mobility.persistence.BusStopRepository;
using p8mobility.persistence.Connection;
using p8mobility.persistence.Dapper;
using p8mobility.persistence.RouteRelationsRepository;

namespace p8mobility.persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigurePersistenceMySqlConnection(this IServiceCollection services,
        string? connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(new MySqlConnectionFactory(connectionString));

        // Add repositories
        services.AddScoped<IBusStopRepository, BusStopRepository.BusStopRepository>();
        services.AddScoped<IBusRepository, BusRepository.BusRepository>();
        services.AddScoped<IRouteRelationsRepository, RouteRelationsRepository.RouteRelationsRepository>();


        // Dapper
        SqlMapper.AddTypeHandler(new GuidTypeHandler());

        return services;
    }
}