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

    public CDLType STAGE { get; private set; }
    public CDLType NODE { get; private set; }
    public CDLType CHAR { get; private set; }
    public CDLType ENEMY { get; private set; }
    public CDLType EFFECT { get; private set; }
    public CDLType CARD { get; private set; }

    private void InitializeConstants()
    {
        this.ERROR = new CDLType("ErrorType");
        this.STRING = new CDLType("string");
        this.DOUBLE = new CDLType("double");
        this.INT = new CDLType("int");
        this.BOOLEAN = new CDLType("bool");

        this.STAGE = new CDLType("Stage");
        this.NODE = new CDLType("Node");
        this.CHAR = new CDLType("Character");
        this.ENEMY = new CDLType("Enemy");
        this.EFFECT = new CDLType("Effect");
        this.CARD = new CDLType("Card");

        this.ERROR.Parents.Add(this.STRING);
        this.ERROR.Parents.Add(this.DOUBLE);
        this.ERROR.Parents.Add(this.INT);
        this.ERROR.Parents.Add(this.BOOLEAN);

        this.ERROR.Parents.Add(this.STAGE);
        this.ERROR.Parents.Add(this.NODE);
        this.ERROR.Parents.Add(this.CHAR);
        this.ERROR.Parents.Add(this.ENEMY);
        this.ERROR.Parents.Add(this.EFFECT);
        this.ERROR.Parents.Add(this.CARD);

        this.DOUBLE.Parents.Add(this.INT);

        types[this.ERROR.Name] = this.ERROR;
        types[this.INT.Name] = this.INT;
        types[this.STRING.Name] = this.STRING;
        types[this.BOOLEAN.Name] = this.BOOLEAN;
        types[this.DOUBLE.Name] = this.DOUBLE;

        types[this.STAGE.Name] = this.STAGE;
        types[this.NODE.Name] = this.NODE;
        types[this.CHAR.Name] = this.CHAR;
        types[this.ENEMY.Name] = this.ENEMY;
        types[this.EFFECT.Name] = this.EFFECT;
        types[this.CARD.Name] = this.CARD;
    }

    public CDLType this[string name]
    {
        get
        {
            if (this.types.TryGetValue(name, out CDLType? value))
                return value;
            return null;
        }
    }
    public CDLType CreateType(string name)
    {
        if (this.types.ContainsKey(name))
            return null;

        var type = new CDLType(name);
        this.types[name] = type;
        return type;
    }
}