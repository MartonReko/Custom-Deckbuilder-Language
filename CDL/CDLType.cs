namespace CDL;

public class CDLType(string name)
{
    public string Name { get; private set; } = name;
    public HashSet<CDLType> Parents { get; private set; } = new HashSet<CDLType>();

    public bool InheritsFrom(CDLType t)
    {
        return this == t || this.Parents.Any(p => p.InheritsFrom(t));
    }
}