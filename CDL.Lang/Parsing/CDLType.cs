namespace CDL.Lang.Parsing;

public class CDLType(string name)
{
    public string Name { get; private set; } = name;
    public HashSet<CDLType> Parents { get; private set; } = [];

    public bool InheritsFrom(CDLType t)
    {
        return this == t || Parents.Any(p => p.InheritsFrom(t));
    }
}
