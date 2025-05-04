using System.Text;

namespace CDL.Lang.Parsing.Symboltable;

public class Env
{
    public Env? PrevEnv { get; private set; }
    private readonly Dictionary<string, Symbol> table;

    public Env(Env? prevEnv = null)
    {
        table = [];
        PrevEnv = prevEnv;
    }

    public Symbol? this[string name]
    {
        get
        {
            if (table.ContainsKey(name))
            {
                return table[name];
            }
            else if (PrevEnv != null)
            {
                return PrevEnv[name];
            }
            return null;
            //return this.table.ContainsKey(name) ? this.table[name] : this.PrevEnv?[name];
        }
        set
        {
            if (table.ContainsKey(name))
                throw new Exception($"Name {name} already in scope");
            if (value != null)
            {
                table[name] = value;
            }

        }
    }

    public override string ToString()
    {
        StringBuilder bld = new StringBuilder();
        if (PrevEnv != null)
            bld.Append(PrevEnv.ToString());
        //bld.AppendLine("-----------------");
        foreach (var symbol in table.Values)
        {
            bld.AppendLine(symbol.ToString());
        }
        return bld.ToString();
    }
}