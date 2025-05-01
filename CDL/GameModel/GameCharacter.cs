namespace CDL.GameModel;

public class GameCharacter(string name) : Entity
{
    public readonly string Name = name;
    public List<Effect> EffectEveryTurn { get; set; } = [];
    public Dictionary<Card, int> Deck { get; set; } = [];
}