namespace CDL.Lang.GameModel;

public class ModelCharacter(string name) : Entity
{
    public readonly string Name = name;
    public List<Effect> EffectEveryTurn { get; set; } = [];
    public Dictionary<Card, int> Deck { get; set; } = [];
}