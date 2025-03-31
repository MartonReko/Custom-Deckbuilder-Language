namespace CDL.game;

public class GameSetup()
{
    public GameCharacter? Player { get; set; }
    public List<Stage> Stages { get; set; } = [];
    public string? GameName { get; set; }
}