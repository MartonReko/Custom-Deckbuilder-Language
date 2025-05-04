namespace CDL.Lang.Parsing.Symboltable;

public class Symbol
{
    public string Name { get; set; }
    public CDLType Type { get; set; }
    public string? Value {get;set;}
    public Symbol(string name, CDLType type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} : {Type.Name}";
    }
}