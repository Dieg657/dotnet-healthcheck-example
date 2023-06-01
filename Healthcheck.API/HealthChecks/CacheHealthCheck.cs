using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace Healthcheck.API.HealthChecks
{
    public class CacheHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _redisCache;

        public CacheHealthCheck(IConnectionMultiplexer redisCache)
        {
            _redisCache = redisCache;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(_redisCache.IsConnected ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy(description: "Not connected"));
        }
    }
}
