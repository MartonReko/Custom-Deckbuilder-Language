namespace CDL.Game.DTOs
{
    public class GameServiceDto : IGameDto
    {
        public GameService.PlayerStates PlayerState { get; set; }
        public GameServiceDto()
        {
            Map = new GameMapDto();
        }
        public GameMapDto Map { get; set; }
        public GameNodeDto? CurrentNode { get; set; }
        string IGameDto.PlayerState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
