using CDL.game;

namespace CDL.gameModel;

public class Attack(string name)
{
    public readonly string Name = name;
    public List<EnemyAttackTargets> Targets = [];
    public List<Effect> EffectsApplied = [];
}

public enum EnemyAttackTargets { SELF, PLAYER }
