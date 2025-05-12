using System.Diagnostics.Contracts;
using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameEnemy
    {
        public readonly Enemy _modelEnemy;
        private int AttackCounter = 0;
        public int Health { get; private set; }
        public Dictionary<Effect, int> CurrentEffects { get; private set; } = [];
       
        public GameEnemy(Enemy enemy)
        {
            _modelEnemy = enemy;
           Health =_modelEnemy.Health; 
        }

        // Return info that can be used to actually show stuff in the GUI
        public (EnemyAction EnemyAction, EnemyTarget target, int num) Attack(GameCharacter player)
        {
            int nextAttack = AttackCounter % _modelEnemy.Actions.Count;
            AttackCounter++;
            if (_modelEnemy.Actions[nextAttack].target == EnemyTarget.PLAYER)
            {
                ApplyAction(player, _modelEnemy.Actions[nextAttack].EnemyAction);
                return _modelEnemy.Actions[nextAttack];
            }else
            {
                ApplyAction(_modelEnemy.Actions[nextAttack].EnemyAction);
                return _modelEnemy.Actions[nextAttack];
            }
        }

        private void ApplyAction(GameCharacter player, EnemyAction ea)
        {
            foreach ((Effect effect, int cnt) in ea.EffectsApplied)
            {
                if(effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
                {
                    if (player.CurrentEffects.ContainsKey(effect))
                    {
                        int oldCnt = player.CurrentEffects[effect];
                        player.CurrentEffects[effect] = cnt + oldCnt;
                    }
                    else
                    {
                        player.CurrentEffects.Add(effect, cnt);
                    }
                }
                if(effect.EffectType == EffectType.INSTANT)
                {
                    player.Health += (int)Math.Round(effect.DamageDealt);
                }
            }
        }
        private void ApplyAction(EnemyAction ea)
        {
            foreach ((Effect effect, int cnt) in ea.EffectsApplied)
            {
                ApplyEffect(effect, cnt);
            }
        }

        public void ApplyEffect(Effect effect, int cnt)
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
                Health += (int)Math.Round(effect.DamageDealt);
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
                    Health += (int)Math.Round(item.Key.DamageDealt);
                    CurrentEffects[item.Key] = item.Value - 1;
                    if(CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
                if (item.Key.EffectType == EffectType.MOD)
                {
                    CurrentEffects[item.Key] = item.Value - 1;
                    if(CurrentEffects[item.Key] == 0)
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
