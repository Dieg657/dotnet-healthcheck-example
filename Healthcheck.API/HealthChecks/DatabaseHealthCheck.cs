using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace Healthcheck.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IDbConnection _dbConnection;

        public DatabaseHealthCheck(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbConnection.QueryAsync<int>("SELECT 1");

                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy(exception: exception);
            }
        }
    }
}
