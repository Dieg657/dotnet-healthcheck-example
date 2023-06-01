using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;
using System.Data;
using Healthcheck.API.HealthChecks;
using StackExchange.Redis;

namespace Healthcheck.API.DependencyInjection
{
    public static class IoC
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddRedis(configuration);
            services.AddHealthCheck();
            
            return services;
        }

        private static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                        .AddCheck<DatabaseHealthCheck>("SqlServer")
                        .AddCheck<CacheHealthCheck>("Redis");
            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnection>(provider => new SqlConnection(configuration.GetConnectionString("MSSQL_DATABASE")));
            return services;
        }

        private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("REDIS_CACHE") ?? string.Empty;

            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(connection));
            return services;
        }
    }
}
