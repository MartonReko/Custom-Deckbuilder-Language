namespace CDL.Lang.GameModel;

public class Effect(string name)
{
    public readonly string Name = name;
    public double InDmgMod { get; set; } = 1;
    public double OutDmgMod { get; set; } = 1;
    public double DamageDealt {get;set;} = 0;
    public EffectType EffectType { get; set; } = EffectType.MOD;
}
public enum EffectType
{
    INSTANT, TURNEND, MOD
}