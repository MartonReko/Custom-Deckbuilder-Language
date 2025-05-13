namespace CDL.Lang.GameModel;

public class GameSetup()
{
    public ModelCharacter? Player { get; set; }
    public List<Stage> Stages { get; set; } = [];
    public string? GameName { get; set; }
}