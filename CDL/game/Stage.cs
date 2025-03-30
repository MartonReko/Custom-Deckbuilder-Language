namespace CDL.game;

public class Stage(string name){
    public string Name { get; set; } = name;
    public int StageLength { get; set; } = 0;
    public int StageWidthMin { get; set; } = 0;
    public int StageWidthMax { get; set; } = 0;
    public List<Node> FillWith { get; set; } = [];
    public Dictionary<Node, int> MustContain { get; set; } = [];
    public Node? EndsWith { get; set; } 

}