using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Healthcheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly IDbConnection _connection;

        public TestController(IDbConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("OK");
        }

        [HttpGet(nameof(CheckDatabase))]
        public async Task<IActionResult> CheckDatabase()
        {
            return Ok(await _connection.QueryAsync<int>("SELECT 1"));
        }
    }
}
