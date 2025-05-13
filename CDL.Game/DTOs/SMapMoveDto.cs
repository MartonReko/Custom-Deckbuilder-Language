namespace CDL.Game.DTOs
{
    public class SMapMoveDto : IGameDto
    {
        public string PlayerState { get; set; }
        public GameMapDto Map { get; set; }
        public SMapMoveDto(string playerState, GameMapDto map)
        {
            PlayerState = playerState;
            Map = map;
        }
    }
}
