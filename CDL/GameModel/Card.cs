namespace CDL.GameModel;

public class Card(string name)
{
    public readonly string Name = name;
    public string Rarity { get; set; } = "";
    public HashSet<TargetTypes> ValidTargets { get; set; } = [];
    public List<(Effect effect, int effectCount)> EffectsApplied { get; set; } = [];
    
}

public enum TargetTypes
{
    ENEMY,
    ENEMIES,
    PLAYER
}

// TODO
// common project