using CDL.Game.DTOs;
using CDL.Game.GameObjects;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing;
using Microsoft.AspNetCore.Mvc;

namespace CDL.Game.Controllers
{
    [ApiController]
    [Route("[controller]/Test")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService) {
            _gameService = gameService;
        }

        [HttpPost("MoveToNode")]
        public IActionResult Receive([FromBody] MoveResponse response)
        {
            if (response == null)
            {
                return BadRequest("Invalid received");
            }
            // TODO
            // Should also move to using IDs here
            _gameService.Move(_gameService.GetMoves()[response.Index]);
            return Ok(new { message = "Data received successfully!", receivedData = response});
        }
        [HttpGet("GetState")]
        public IGameDto? GetState()
        {
            /*
            switch (_gameService.PlayerState)
            {
                case GameService.PlayerStates.MAPMOVE:
                    return new SMapMoveDto
                    (
                        _gameService.PlayerState.ToString(),
                        GetGameMap()
                    );
                case GameService.PlayerStates.COMBAT:
                    return new SCombatDto(
                        _gameService.PlayerState.ToString(),
                        _gameService.CombatState.ToString(),
                        GetPlayerInfo()
                        GetEnemiesInfo()
                    );
            }
            */
            return null;
        }
        private GameMapDto GetGameMap() {
            GameMapDto gameMap = new();
            foreach(var item in _gameService.GameMap.CurrentStage.NodesByLevel)
            {
                List<string> nodeNames = [];
                foreach (Node node in item.Value)
                {
                    nodeNames.Add(node.Name);
                }

                gameMap.NodesByLevel.Add(nodeNames);
            }
            return gameMap;
        }
        private PlayerDto GetPlayerInfo()
        {
            PlayerDto playerInfo = new PlayerDto
            {
                Health = _gameService.Player.Health
            };

            foreach(GameCard card in _gameService.Deck)
            {
                List<string> targets = [];
                foreach(var target in card.ModelCard.ValidTargets)
                {
                    targets.Add(target.ToString()); 
                }
                List<EffectDto> effects = new List<EffectDto>();
                /*
                foreach((Effect effect, int num) in card.ModelCard.EffectsApplied)
                {
                    effects.Add(new EffectDto
                    (
                        effect.    
                    ));
                }
                playerInfo.Deck.Add(new CardDto
                {
                    Id = card.Id,
                    Name = card.ModelCard.Name,
                    Rarity = card.ModelCard.Rarity,
                    ValidTargets = targets,

                });
                */
            }

            return playerInfo;
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
