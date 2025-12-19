using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCharacter(ModelCharacter modelCharacter) : GameEntity
    {
        //private readonly ModelCharacter ModelCharacter = modelCharacter;
        public int Health { get; private set; } = modelCharacter.Health;
        public string Name { get; private set; } = modelCharacter.Name;
        // NOTE:
        // Is this even a good feature?
        // public Dictionary<Effect, int> EffectEveryTurn { get; private set; } = [];
        public Dictionary<Effect, int> CurrentEffects { get; private set; } = [];

        public void Restore()
        {
            Health = modelCharacter.Health;
        }
        public void Damage(double value)
        {
            double damage = value;
            foreach ((Effect effect, int num) in CurrentEffects)
            {
                Console.WriteLine($"Damage modded {damage} * {effect.InDmgMod}");
                damage *= effect.InDmgMod;
            }
            Health -= (int)damage;
        }
        public void ApplyAction(EnemyAction ea, Dictionary<Effect, int> enemyEffects)
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
                    double moddedDamage = effect.DamageDealt;
                    foreach ((Effect e, int num) in enemyEffects)
                    {

                        moddedDamage *= e.OutDmgMod;
                        Console.WriteLine($"Player incoming damage: {moddedDamage} * {e.OutDmgMod}, {e.Name}, {e.EffectType}");
                    }
                    Damage(moddedDamage);
                }
            }
        }

        // Damage mods not counted when playing card on self
        public void ApplyCard(Effect effect, int cnt)
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
                Health -= (int)Math.Round(effect.DamageDealt); ;
            }
        }
    }
}
