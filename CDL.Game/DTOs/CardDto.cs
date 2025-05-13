using CDL.Lang.GameModel;

namespace CDL.Game.DTOs
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rarity { get; set; }
        public List<string> ValidTargets { get; set; } = [];
        public List<EffectDto> Effects { get; set; } = [];
        public CardDto(Guid id, string name, string rarity, List<string> validTargets, List<EffectDto> effects)
        {
            Id = id;
            Name = name;
            Rarity = rarity;
            ValidTargets = validTargets;
            Effects = effects;
        }
    }
}
