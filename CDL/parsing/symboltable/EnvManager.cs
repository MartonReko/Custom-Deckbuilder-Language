using Antlr4.Runtime;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


namespace CDL.parsing;

public class EnvManager()
{

    private readonly ILogger<EnvManager> _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<EnvManager>();
    public Env Env { get; set; } = new();
    public TypeSystem Ts { get; set; } = new();
    public static string GetPos(ParserRuleContext context)
    {
        return $"line #{context.Start.Line}, column #{context.Start.Column}";
    }
    public static (int,int) GetPosLineCol(ParserRuleContext context){
        return (context.Start.Line,context.Start.Column);
    }
    public void AddVariableToScope(ParserRuleContext ctx, Symbol symbol)
    {
        try
        {
            Env[symbol.Name] = symbol;
        }
        catch
        {
            _logger.LogError("Error at {pos}: variable {symName} is already in scope", GetPos(ctx), symbol.Name);
        }
    }
    public void AddFnToScope(ParserRuleContext ctx, FnSymbol symbol)
    {
        try
        {
            Env[symbol.Name] = symbol;
        }
        catch
        {
            _logger.LogError("Error at {pos}: function {symName} is already in scope", GetPos(ctx), symbol.Name);
        }
    }
    public Symbol? GetVariableFromScope(ParserRuleContext context, string varName)
    {
        var symbol = Env[varName];
        if (symbol != null) return symbol;
        _logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
        return null;
    }
    public bool IsVariableOnScope( string varName)
    {
        var symbol = Env[varName];
        return symbol != null;
        //_logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
    }
    public bool CheckVarType(string varName, CDLType type){
        var symbol = Env[varName];
        if(symbol == null){
            return false;
        }
        return symbol.Type == type;
    }
    public CDLType? GetType(CDLParser.LiteralExpressionContext context)
    {
        if (context.varName() != null)
        {
            Symbol? symbol = null;
            var varName = context.varName();
            symbol = GetVariableFromScope(varName, varName.GetText());
            return symbol?.Type ?? Ts.ERROR;
        }
        if (context.INT() != null)
            return Ts.INT;
        if (context.boolean() != null)
            return Ts.BOOLEAN;
        if (context.STRING() != null)
            return Ts.STRING;
        if (context.DOUBLE() != null)
            return Ts.DOUBLE;

        return null;
    }
}