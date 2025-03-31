namespace CDL.game;

public class GameCharacter(string name) : Entity
{
    public readonly string Name = name;
    public List<Effect> EffectEveryTurn { get; set; } = [];
}