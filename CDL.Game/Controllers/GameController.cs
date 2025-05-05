using CDL.Lang.Parsing;
using Microsoft.AspNetCore.Mvc;

namespace CDL.Game.Controllers
{
    public class TestModel
    {
        public string Name { get; set; }
    }
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly GameService _gameService;

        public TestController(GameService gameService) {
            _gameService = gameService;
        }

        [HttpPost]
        public IActionResult Receive([FromBody] TestModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid received");
            }

            return Ok(new { message = "Data received successfully!", receivedData = model});
        }

        [HttpGet(Name = "GetPlayerState")]
        public GameService Get()
        {
            return _gameService;
            /*
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            */
        }
    }
}
