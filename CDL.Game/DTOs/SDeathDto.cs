namespace CDL.Game.DTOs
{
    public class SDeathDto : IGameDto
    {
        public GameService.PlayerStates PlayerState { get; set; }
    }
}
