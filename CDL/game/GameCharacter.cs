namespace CDL.game;

public class GameCharacter : Entity
{
    public string Name { get; set; } = "";
    public List<Effect> EffectEveryTurn { get; set; } = [];
}