namespace CDL.game;

public class Card
{
    public string Name { get; set; } = "";
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