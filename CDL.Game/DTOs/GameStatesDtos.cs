namespace CDL.Game.DTOs
{
    public record StatusDto(
        string Name,
        Guid PlayerId,
        int Health,
        Guid? CurrentNode,
        GameService.PlayerStates CurrentState,
        List<CardDto> Deck,
        List<EffectDto> Effects
    );
    public record MapDto(
            string StageName,
            List<NodeDto> Nodes
            );
    public class MoveDto
    {
        public Guid NodeId { get; set; }
    }
    public record CombatDto(
            int Energy,
            List<EnemyDto> Enemies
            );
    public record EnemyDto(
            Guid Id,
            string Name,
            int Health,
            List<EffectDto> Effects
            );
    public record NodeDto(
            Guid Id,
            string Name,
            int Level
            );
    public record CardDto(
            Guid Id,
            string Name,
            List<EffectDto> Effects
            );
    public record EffectDto(
            string Name,
            int Stack,
            string Desc
            );
    public record RewardDto(
            List<CardDto> Cards
            );

    public record ReceivedCdlDto(string CodeString);
    public record PlayCardDto(Guid CardId, Guid TargetId);
}
