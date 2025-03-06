using System.Reflection.Metadata.Ecma335;

namespace CDL;

public class TypeSystem
{
    Dictionary<string, CDLType> types;
    public TypeSystem()
    {
        this.types = new Dictionary<string, CDLType>();
        InitializeConstants();
    }

    public CDLType BOOLEAN { get; private set; }
    public CDLType INT { get; private set; }
    public CDLType STRING { get; private set; }
    public CDLType DOUBLE { get; private set; }
    public CDLType ERROR { get; private set; }

    private void InitializeConstants()
    {
        this.ERROR = new CDLType("ErrorType");
        this.STRING = new CDLType("string");
        this.DOUBLE = new CDLType("double");
        this.INT = new CDLType("int");
        this.BOOLEAN = new CDLType("bool");

        this.ERROR.Parents.Add(this.STRING);
        this.ERROR.Parents.Add(this.DOUBLE);
        this.ERROR.Parents.Add(this.INT);
        this.ERROR.Parents.Add(this.BOOLEAN);

        this.DOUBLE.Parents.Add(this.INT);

        types[this.ERROR.Name] = this.ERROR;
        types[this.INT.Name] = this.INT;
        types[this.STRING.Name] = this.STRING;
        types[this.BOOLEAN.Name] = this.BOOLEAN;
        types[this.DOUBLE.Name] = this.DOUBLE;
    }

    public CDLType this[string name]
    {
        get
        {
            if (this.types.ContainsKey(name))
                return types[name];
            return null;
        }
    }
    public CDLType CreateType(string name){
        if (this.types.ContainsKey(name))
            return null;

        var type = new CDLType(name);
        this.types[name] = type;
        return type;
    }
}