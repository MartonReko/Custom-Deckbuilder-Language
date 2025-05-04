namespace CDL.Lang.GameModel;

public class Stage(string name)
{
    public readonly string Name = name;
    public int? StageLength { get; set; } 
    public int? StageWidthMin { get; set; } 
    public int? StageWidthMax { get; set; } 
    public List<Node> FillWith { get; set; } = [];
    public Dictionary<Node, int> MustContain { get; set; } = [];
    public Node? EndsWith { get; set; }
}