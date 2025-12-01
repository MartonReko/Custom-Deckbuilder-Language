using CDL.Game.DTOs;
using CDL.Game.GameObjects;
using CDL.Lang.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CDL.Game.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GameController(GameServiceManager gameServiceManager) : ControllerBase
    {
        private static int statusCounter = 0;
        private static int mapCounter = 0;
        private static int combatCounter = 0;
        private readonly GameServiceManager _gameServiceManager = gameServiceManager;

        [HttpGet(template: "status", Name = "GetGameState")]
        public ActionResult<StatusDto> GetStatus()
        {
            statusCounter++;
            try
            {
                GameService gs = _gameServiceManager.GetService();
                StatusDto response = new(
                        Name: gs.Player.Name,
                        PlayerId: gs.Player.Id,
                        Health: gs.Player.Health,
                        CurrentLevel: gs.GameMap.LevelCounter,
                        CurrentNode: gs.CurrentGameNode?.Id ?? null,
                        CurrentState: gs.PlayerState,
                        Deck: [.. gs.Deck.Select((x) => new CardDto(x.Id, x.ModelCard.Name, x.ModelCard.Cost, [.. x.ModelCard.EffectsApplied.Select(y => new EffectDto(y.effect.Name, 1, "Temp desc"))]))],
                        Effects: [.. gs.Player.CurrentEffects.Select(x => new EffectDto(x.Key.Name, x.Value, "Temp desc"))]
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
            mapCounter++;
            try
            {
                GameService gs = _gameServiceManager.GetService();
                Dictionary<Guid, HashSet<Guid>> edges = [];
                foreach (var pair in gs.GameMap.CurrentStage.Edges)
                {
                    edges.Add(pair.Key, pair.Value);
                }
                MapDto response = new(
                        StageName: gs.GameMap.CurrentStage.ModelStage.Name,
                        Edges: edges,
                        Nodes: [.. gs.GameMap.CurrentStage.GameNodesByLevel.SelectMany(x => x.Value.Select(y => new NodeDto(
                             Id: y.Id,
                             Name: y.ModelNode.Name,
                             Level: x.Key
                             )))]);
                // foreach (var (level, nodes) in gs.GameMap.CurrentStage.GameNodesByLevel)
                // {
                //     foreach (var node in nodes)
                //     {
                //         response.Nodes.Add(node.Id, new NodeDto(node.Id, node.ModelNode.Name, level, [.. gs.GameMap.CurrentStage.Edges[node.Id]]));
                //     }
                // }
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(template: "combat", Name = "Combat")]
        public ActionResult<CombatDto> GetCombat()
        {
            combatCounter++;
            try
            {
                GameService gs = _gameServiceManager.GetService();
                CombatDto response = new(
                        Energy: gs.Energy,
                        Enemies: [.. gs.CurrentGameNode.Enemies.Select(x => new EnemyDto(x.Id, x.ModelEnemy.Name, x.Health, x.CurrentEffects.Select(y => new EffectDto(y.Key.Name, y.Value, "Temp desc")).ToList()))],
                        Hand: [.. gs.Hand.Select((x) => new CardDto(x.Id, x.ModelCard.Name, x.ModelCard.Cost, [.. x.ModelCard.EffectsApplied.Select(y => new EffectDto(y.effect.Name, 1, "Temp desc"))]))]
                        );
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(template: "reward", Name = "Reward")]
        public ActionResult<RewardDto> GetReward()
        {
            try
            {
                GameService gs = _gameServiceManager.GetService();
                RewardDto response = new(
                        Cards: [.. gs.NodeRewards.Select(x => new CardDto(x.Id, x.ModelCard.Name, x.ModelCard.Cost, [.. x.ModelCard.EffectsApplied.Select(y => new EffectDto(y.effect.Name, 1, "Temp desc"))]))]
                        );
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(template: "readcdl", Name = "ReadCDL")]
        [Consumes("text/plain")]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> ParseCDL()
        {
            using var reader = new StreamReader(Request.Body);
            string content = await reader.ReadToEndAsync();

            try
            {
                _gameServiceManager.Initialize(content);
                return Ok("GameService initialized successfully.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet(template: "codeErrors", Name = "CodeErrors")]
        public ActionResult<CodeErrorListDto> GetCodeErrors()
        {
            try
            {
                CodeErrorListDto errors = new([.. _gameServiceManager.CDLExceptions.Select(x => new CodeErrorDto(x.Column, x.Message))]);
                return Ok(errors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(template: "move", Name = "Move")]
        public ActionResult<MoveDto> MoveToNode([FromBody] Guid NodeId)
        {
            if (_gameServiceManager.GetService().Move(NodeId))
                return Ok("Successfully moved");
            else
                return BadRequest("Not so successfully moved");
        }

        [HttpPost(template: "getReward", Name = "GetReward")]
        public ActionResult<MoveDto> GetReward([FromBody] Guid CardId)
        {
            if (_gameServiceManager.GetService().ChooseReward(CardId))
                return Ok("Successfully chosen reward");
            else
                return BadRequest("Reward could not be choosen");
        }

        [HttpPost(template: "playCard", Name = "PlayCard")]
        public IActionResult PlayCard([FromBody] PlayCardDto received)
        {
            if (_gameServiceManager.GetService().PlayCard(received.CardId, received.TargetId))
                return Ok("Successfully played card");
            else
                return BadRequest("Playing card failed");
        }

        [HttpPost(template: "endTurn", Name = "EndTurn")]
        public IActionResult EndTurn()
        {
            _gameServiceManager.GetService()?.EndTurn();
            return Ok();
        }

    }
}
