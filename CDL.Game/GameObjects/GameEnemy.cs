using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameEnemy : GameEntity
    {
        public readonly Enemy ModelEnemy;
        public int Health { get; private set; }
        private int AttackCounter = 0;
        public Dictionary<Effect, int> CurrentEffects { get; private set; } = [];

        public GameEnemy(Enemy enemy)
        {
            ModelEnemy = enemy;
            Health = ModelEnemy.Health;
        }

        // Return info that can be used to actually show stuff in the GUI
        public (EnemyAction EnemyAction, EnemyTarget target, int num) Attack(GameCharacter player)
        {
            if (ModelEnemy.Actions.Count == 0)
                throw new InvalidOperationException("ZERO ACTIONS");
            int nextAttack = AttackCounter % ModelEnemy.Actions.Count;
            AttackCounter++;
            if (ModelEnemy.Actions[nextAttack].target == EnemyTarget.PLAYER)
            {
                //ApplyAction(player, ModelEnemy.Actions[nextAttack].EnemyAction);
                player.ApplyAction(ModelEnemy.Actions[nextAttack].EnemyAction, CurrentEffects);
                return ModelEnemy.Actions[nextAttack];
            }
            else if (ModelEnemy.Actions[nextAttack].target == EnemyTarget.SELF)
            {
                ApplyAction(ModelEnemy.Actions[nextAttack].EnemyAction);
                return ModelEnemy.Actions[nextAttack];
            }
            else
            {
                throw new InvalidOperationException("Action target was no set");
            }
        }

        //        private void ApplyAction(ModelCharacter player, EnemyAction ea)
        //        {
        //            foreach ((Effect effect, int cnt) in ea.EffectsApplied)
        //            {
        //                if (effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
        //                {
        //                    if (player.CurrentEffects.TryGetValue(effect, out int oldCnt))
        //                    {
        //                        player.CurrentEffects[effect] = cnt + oldCnt;
        //                    }
        //                    else
        //                    {
        //                        player.CurrentEffects.Add(effect, cnt);
        //                    }
        //                }
        //                if (effect.EffectType == EffectType.INSTANT)
        //                {
        //                    player.Health += (int)Math.Round(effect.DamageDealt);
        //                }
        //            }
        //        }
        private void ApplyAction(EnemyAction ea)
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
                    Health += (int)effect.DamageDealt;
                }
            }
        }

        public void ApplyEffect(Effect effect, int cnt, Dictionary<Effect, int> playerEffects)
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
                double damage = effect.DamageDealt;
                foreach ((Effect e, int num) in playerEffects)
                {
                    damage *= e.OutDmgMod;
                }
                foreach ((Effect e, int num) in CurrentEffects)
                {
                    damage *= e.InDmgMod;
                }
                Health -= (int)Math.Round(damage); ;
            }
        }

        public void EndTurn()
        {
            if (Health <= 0) return;
            List<Effect> toRemove = [];
            foreach (var item in CurrentEffects)
            {
                if (item.Key.EffectType == EffectType.TURNEND)
                {
                    Health -= (int)Math.Round(item.Key.DamageDealt);
                    Console.WriteLine($"TURNEND : {item.Key.DamageDealt} was dealt");
                    CurrentEffects[item.Key] = item.Value - 1;
                    if (CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
                if (item.Key.EffectType == EffectType.MOD)
                {
                    CurrentEffects[item.Key] = item.Value - 1;
                    if (CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
            }
            foreach (Effect effect in toRemove)
            {
                CurrentEffects.Remove(effect);
            }

        }
    }
}
