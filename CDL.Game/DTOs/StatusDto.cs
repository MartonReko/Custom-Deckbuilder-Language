namespace CDL.Game.DTOs
{
    public record StatusDto(
        string Name,
        int Health,
        Guid? CurrentNode,
        GameService.PlayerStates CurrentState
    );
}
