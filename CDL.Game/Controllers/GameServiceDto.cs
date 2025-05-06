namespace CDL.Game.Controllers
{
    public class GameServiceDto
    {
        public GameService.PlayerStates PlayerState { get; set; }
        public GameServiceDto()
        {
            Map = new GameMapDto();
        }
        public GameMapDto Map { get; set; }
        public GameNodeDto CurrentNode { get; set; }
    }
}
