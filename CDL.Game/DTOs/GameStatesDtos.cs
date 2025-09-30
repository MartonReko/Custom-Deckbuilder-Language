namespace CDL.Game.DTOs
{
    public record MapDto(
            string StageName,
            List<NodeDto> Nodes
            );

    public record NodeDto(
            string Name,
            int Level
            );
}
