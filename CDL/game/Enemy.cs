using System.Diagnostics;

namespace CDL.game;

public class Enemy(string name) : Entity
{
    public string Name { get; set; } = name;
    public List<Effect> Actions { get; set; } = [];
}

public enum EnemyTarget{
    PLAYER
}