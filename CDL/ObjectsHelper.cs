using CDL.game;

namespace CDL;

public class ObjectsHelper{

    // All game objects are stored in these lists
    public GameSetup Game { get; set; } = new();
    public GameCharacter Character { get; set; } = new();
    public List<Stage> Stages { get; set; } = [];
    public List<Node> Nodes { get; set; } = [];
    public List<Enemy> Enemies { get; set; } = [];
    public List<Effect> Effects { get; set; } = [];
    public List<Card> Cards { get; set; } = [];
}