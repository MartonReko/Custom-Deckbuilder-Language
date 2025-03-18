namespace CDL;

public class Symbol
{
    public string Name { get; set; }
    public CDLType Type { get; set; }

    public Symbol(string name, CDLType type)
    {
        this.Name = name;
        this.Type = type;
    }

    public override string ToString()
    {
        return $"{Name} : {Type.Name}";
    }
}