namespace CDL.Game.DTOs
{
    public class StatusDto
    {
        public string Name { get; set; } = "";
        public int Health { get; set; }
        public Guid CurrentNode { get; set; }
    }
}
