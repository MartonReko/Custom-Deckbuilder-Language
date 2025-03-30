namespace CDL.game;

public class Effect(string name){
    public string Name { get; set; } = name;
    //public string Type {get; set;} = "";
    public double InDmgMod { get; set; } = 1;
    public double OutDmgMod { get; set; } = 1;
    public List<Action<Entity>> InstantAction { get; set; } = [];
    public List<Action<Entity>> PerTurnAction { get; set; } = [];
}