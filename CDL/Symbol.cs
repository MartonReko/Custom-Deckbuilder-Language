namespace CDL;

public class Symbol
{
    public string Name { get; set; }
    public CDLType Type { get; set; }
    public List<CDLType>? Properties { get; set; }

    public Symbol(string name, CDLType type, List<CDLType>? props = null)
    {
        this.Name = name;
        this.Type = type;
        this.Properties = props;
    }

    public override string ToString()
    {
        if (Properties == null)
        {
            return $"{Name} : {Type.Name}"; 
        }
        else
        {
            return $"{Name}({PropsToString()}) : {Type.Name}"; ;
        }
    }
    private string PropsToString()
    {
        string p = "";
        if (Properties != null)
        {
            for (int i = 0; i < Properties.Count(); i++)
            {
                if (i > 0)
                    p += ", " + Properties[i].Name;
                else
                    p += Properties[i].Name;
            }
        }
        return p;
    }
}