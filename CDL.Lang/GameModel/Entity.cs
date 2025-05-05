namespace CDL.Lang.GameModel;

public abstract class Entity{
    public int Health { get; set; } = 0;
    public Dictionary<Effect, int> CurrentEffects { get; set; } = [];
}