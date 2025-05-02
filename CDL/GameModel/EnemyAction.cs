namespace CDL.GameModel;

public class EnemyAction(string name){
    public readonly string Name = name;
    public List<(Effect effect, int effectCount)> EffectsApplied { get; set; } = [];
    
}