namespace CDL.Game.DTOs
{
    public class PlayerDto
    {
        public int Health { get; set; }
        public List<CardDto> Deck { get; set; } = [];
        public List<EffectDto> Effects { get; set; } = [];
    }
}
