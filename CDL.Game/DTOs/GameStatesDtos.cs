namespace CDL.Game.DTOs
{
    public record StatusDto(
        string Name,
        int Health,
        Guid? CurrentNode,
        GameService.PlayerStates CurrentState,
        List<CardDto> Deck
    );
    public record MapDto(
            string StageName,
            List<NodeDto> Nodes
            );
    public record CombatDto(
            List<EnemyDto> Enemies
            );
    public record EnemyDto(
            Guid Id,
            string Name,
            int Health
            //Dictionary<string, int> Effects
            );
    public record NodeDto(
            Guid Id,
            string Name,
            int Level
            );
    public record CardDto(
            Guid Id,
            string Name
            //List<EffectDto> Effects
            );
    public record EffectDto(
            Guid Id,
            string Name,
            string Desc
            );
}
