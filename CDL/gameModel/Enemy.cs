using System.Diagnostics;

namespace CDL.game;

public class Enemy(string name) : Entity
{
    public readonly string Name = name;
    public List<(Effect, EnemyTarget, int)> Actions { get; set; } = [];
}

public enum EnemyTarget{
    PLAYER
}

// TODO
// Enemywrapper