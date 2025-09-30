using CDL.Game.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CDL.Game.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController(GameServiceManager gameServiceManager) : ControllerBase
    {
        private readonly GameServiceManager _gameServiceManager = gameServiceManager;

        [HttpGet(template: "status", Name = "GetGameState")]
        public ActionResult<StatusDto> GetStatus()
        {
            try
            {
                GameService gs = _gameServiceManager.GetService();
                StatusDto response = new(
                        Name: gs.Player.Name,
                        Health: gs.Player.Health,
                        CurrentNode: gs.CurrentGameNode.Id,
                        CurrentState: gs.PlayerState
                        );
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(template: "map", Name = "Map")]
        public ActionResult<MapDto> GetMap()
        {
            try
            {
                GameService gs = _gameServiceManager.GetService();
                MapDto response = new(
                        StageName: gs.GameMap.CurrentStage.ModelStage.Name,
                        Nodes: [.. gs.GameMap.CurrentStage.GameNodesByLevel.SelectMany(x => x.Value.Select(y => new NodeDto(Name: y.ModelNode.Name, Level: x.Key)).ToList())]
                        );
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(Name = "ReadCDL")]
        [Consumes("text/plain")]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> ParseCDL()
        {
            using var reader = new StreamReader(Request.Body);
            string content = await reader.ReadToEndAsync();
            //             try
            //             {
            _gameServiceManager.Initialize(content);
            return Ok("GameService initialized successfully.");
            //             }
            //             catch (Exception e)
            //             {
            //                 return BadRequest(e.Message);
            //             }
        }


        // TODO: Fix logic in GameService first
        //        public class MoveDto
        //        {
        //            public Guid NodeId { get; set; }
        //        }
        //        [HttpPost(Name = "MoveToNode")]
        //        public ActionResult<MoveDto> MoveToNode([FromBody] Guid NodeId)
        //        {
        //            //_gameServiceManager.GetService().MoveById(NodeId)
        //            return Ok("Successfully moved");
        //        }
    }
}
