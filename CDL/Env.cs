using System.Text;

namespace CDL;

public class Env{
    public Env PrevEnv {get; private set;}
    private Dictionary<string, Symbol> table;

    public Env(Env prevEnv = null){
        this.table = new Dictionary<string, Symbol>();
        this.PrevEnv = prevEnv;
    }
    
    public Symbol this[string name]
    {
        get{
            return this.table.ContainsKey(name) ? this.table[name] : this.PrevEnv?[name];
        }
        set{
            if (this.table.ContainsKey(name))
                throw new Exception($"Name {name} already in scope");
            this.table[name] = value;

        }
    }

    public override string ToString()
    {
        StringBuilder bld = new StringBuilder();
        if(PrevEnv != null)
            bld.Append(this.PrevEnv.ToString());
        //bld.AppendLine("-----------------");
        foreach(var symbol in this.table.Values)
            bld.AppendLine(symbol.ToString());
        return bld.ToString();
    }
}