namespace CDL.Game.DTOs
{
    public class EnemyDto
    {
        public int Health { get; set; }
        public List<EnemyActionDto> Actions { get; set; } = [];
        public List<EffectDto> Effects { get; set; } = [];
        public EnemyDto(int health, List<EnemyActionDto> actions, List<EffectDto> effects)
        {
            Health = health;
            Actions = actions;
            Effects = effects;
        }
    }
}
