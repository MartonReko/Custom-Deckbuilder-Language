using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCharacter(ModelCharacter modelCharacter) : GameEntity
    {
        //private readonly ModelCharacter ModelCharacter = modelCharacter;
        public int Health { get; private set; } = modelCharacter.Health;
        public string Name { get; private set; } = modelCharacter.Name;
        public Dictionary<Effect, int> CurrentEffects { get; private set; } = [];

        public void Damage(double value)
        {
            double damage = value;
            foreach ((Effect effect, int num) in CurrentEffects)
            {
                damage *= effect.InDmgMod;
            }
            Health += (int)damage;
        }
        public void ApplyAction(EnemyAction ea)
        {
            foreach ((Effect effect, int cnt) in ea.EffectsApplied)
            {
                if (effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
                {
                    if (CurrentEffects.TryGetValue(effect, out int oldCnt))
                    {
                        CurrentEffects[effect] = cnt + oldCnt;
                    }
                    else
                    {
                        CurrentEffects.Add(effect, cnt);
                    }
                }
                if (effect.EffectType == EffectType.INSTANT)
                {
                    Damage(effect.DamageDealt);
                }
            }
        }
    }
}
