namespace CDL.game;

public class GameCharacter{
    public string Name { get; set; } = "";
    public int Health { get; set; } = 0;
    public List<Effect> EffectEveryTurn { get; set; } = [];
}