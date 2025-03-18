using System.Diagnostics;

namespace CDL.game;

public class Enemy(string name)
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = 0;
    public List<Effect> Actions { get; set; } = [];
}