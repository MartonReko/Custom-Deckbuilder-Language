using System.Diagnostics;

namespace CDL.Lang.GameModel;

public class Enemy(string name) : Entity
{
    public readonly string Name = name;
    public List<(EnemyAction EnemyAction, EnemyTarget target, int num)> Actions { get; set; } = [];
}

public enum EnemyTarget{
    PLAYER, SELF
}

// TODO
// Enemywrapper