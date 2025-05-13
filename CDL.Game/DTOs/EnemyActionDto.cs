namespace CDL.Game.DTOs
{
    public class EnemyActionDto
    {
        public string Name { get; set; }
        public List<EffectDto> Effects { get; set; } = [];
        public EnemyActionDto (string name, List<EffectDto> effects)
        {
            Name = name;
            Effects = effects;
        }
    }
}
