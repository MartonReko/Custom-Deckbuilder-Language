namespace CDL.Game.DTOs
{
    public record MapDto(
            string StageName,
            List<NodeDto> Nodes
            );

    public record NodeDto(
            Guid Id,
            string Name,
            int Level
            );
}
