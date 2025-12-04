using System.Reflection.Metadata.Ecma335;

namespace CDL.Lang.Parsing;

public class TypeSystem
{
    Dictionary<string, CDLType> types;
    public TypeSystem()
    {
        types = new Dictionary<string, CDLType>();

        // initialize contraints

        ERROR = new CDLType("ErrorType");
        STRING = new CDLType("string");
        DOUBLE = new CDLType("double");
        INT = new CDLType("int");
        BOOLEAN = new CDLType("bool");

        STAGE = new CDLType("Stage");
        NODE = new CDLType("Node");
        CHAR = new CDLType("Character");
        ENEMY = new CDLType("Enemy");
        EFFECT = new CDLType("Effect");
        CARD = new CDLType("Card");
        RARITY = new CDLType("Rarity");
        ENEMYACTION = new CDLType("EnemyAction");

        ERROR.Parents.Add(STRING);
        ERROR.Parents.Add(DOUBLE);
        ERROR.Parents.Add(INT);
        ERROR.Parents.Add(BOOLEAN);

        ERROR.Parents.Add(STAGE);
        ERROR.Parents.Add(NODE);
        ERROR.Parents.Add(CHAR);
        ERROR.Parents.Add(ENEMY);
        ERROR.Parents.Add(EFFECT);
        ERROR.Parents.Add(CARD);
        ERROR.Parents.Add(RARITY);
        ENEMYACTION.Parents.Add(ENEMYACTION);

        INT.Parents.Add(DOUBLE);
        //DOUBLE.Parents.Add(INT);

        types[ERROR.Name] = ERROR;
        types[INT.Name] = INT;
        types[STRING.Name] = STRING;
        types[BOOLEAN.Name] = BOOLEAN;
        types[DOUBLE.Name] = DOUBLE;

        types[STAGE.Name] = STAGE;
        types[NODE.Name] = NODE;
        types[CHAR.Name] = CHAR;
        types[ENEMY.Name] = ENEMY;
        types[EFFECT.Name] = EFFECT;
        types[CARD.Name] = CARD;
        types[RARITY.Name] = RARITY;
        types[ENEMYACTION.Name] = ENEMYACTION;
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
    public CDLType RARITY { get; private set; }
    public CDLType ENEMYACTION { get; private set; }

    public CDLType this[string name]
    {
        get
        {
            if (types.TryGetValue(name, out CDLType? value))
                return value;
            return ERROR;
        }
    }
    public CDLType CreateType(string name)
    {
        if (types.ContainsKey(name))
            return ERROR;

        var type = new CDLType(name);
        types[name] = type;
        return type;
    }
}
