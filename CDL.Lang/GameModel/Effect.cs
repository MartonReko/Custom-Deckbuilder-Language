namespace CDL.Lang.GameModel;

public class Effect(string name)
{
    public readonly string Name = name;
    //public string Type {get; set;} = "";
    public double InDmgMod { get; set; } = 1;
    public double OutDmgMod { get; set; } = 1;
    public double DamageDealt {get;set;} = 0;
    public List<(Effect effect, int num, EffectTarget target)> EffectsApplied { get; set; } = [];
}
public enum EffectTarget
{
    TARGET,
    ENEMIES,
    PLAYER
}