using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCharacter
    {
        private readonly ModelCharacter ModelCharacter;
        public int Health { get; private set; } = 0;
        public Dictionary<Effect, int> CurrentEffects { get; private set; } = [];

        public GameCharacter(ModelCharacter modelCharacter)
        {
            ModelCharacter = modelCharacter;
            Health = modelCharacter.Health;
        }
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
                if(effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
                {
                    if (CurrentEffects.ContainsKey(effect))
                    {
                        int oldCnt = CurrentEffects[effect];
                        CurrentEffects[effect] = cnt + oldCnt;
                    }
                    else
                    {
                        CurrentEffects.Add(effect, cnt);
                    }
                }
                if(effect.EffectType == EffectType.INSTANT)
                {
                    Damage(effect.DamageDealt);
                }
            }
        }
    }
}
