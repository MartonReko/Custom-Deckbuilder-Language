using CDL.Game.DTOs;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing;
using Microsoft.AspNetCore.Mvc;

namespace CDL.Game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly GameService _gameService;

        public TestController(GameService gameService) {
            _gameService = gameService;
        }

        [HttpPost("MoveToNode")]
        public IActionResult Receive([FromBody] MoveResponse response)
        {
            if (response == null)
            {
                return BadRequest("Invalid received");
            }
            _gameService.Move(_gameService.GetMoves()[response.Index]);
            return Ok(new { message = "Data received successfully!", receivedData = response});
        }

        [HttpGet("GameState")]
        public GameServiceDto Get()
        {
            GameServiceDto response = new GameServiceDto();

            response.PlayerState = _gameService.PlayerState;

            foreach(var item in _gameService.GameMap.CurrentStage.NodesByLevel)
            {
                List<string> nodeNames = [];
                foreach (Node node in item.Value)
                {
                    nodeNames.Add(node.Name);
                }

                response.Map.NodesByLevel.Add(nodeNames);
            }

            response.CurrentNode =  new GameNodeDto();
            response.CurrentNode.Name = _gameService.GameMap.CurrentNode?.Name ?? "";

            return response;
        }
    }
}
