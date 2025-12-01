namespace CDL.Game.DTOs
{
    public record StatusDto(
        string Name,
        Guid PlayerId,
        int Health,
        Guid? CurrentNode,
        int CurrentLevel,
        GameService.PlayerStates CurrentState,
        List<CardDto> Deck,
        List<EffectDto> Effects
    );
    public record MapDto(
            string StageName,
            Dictionary<Guid, HashSet<Guid>> Edges,
            //Dictionary<Guid, NodeDto> Nodes
            List<NodeDto> Nodes
            );
    public class MoveDto
    {
        public Guid NodeId { get; set; }
    }
    public record CombatDto(
            int Energy,
            List<EnemyDto> Enemies,
            List<CardDto> Hand
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
            int Cost,
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
    public record CodeErrorListDto(List<CodeErrorDto> Errors);
    public record CodeErrorDto(int Line, string ErrorMessage);

    public record PlayCardDto(Guid CardId, Guid TargetId);
}
