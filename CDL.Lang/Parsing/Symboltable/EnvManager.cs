using Antlr4.Runtime;
using CDL.Lang.Exceptions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


namespace CDL.Lang.Parsing.Symboltable;

public class EnvManager(CDLExceptionHandler exceptionHandler)
{

    private readonly ILogger<EnvManager> _logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<EnvManager>();
    public Env Env { get; set; } = new();
    public TypeSystem Ts { get; set; } = new();
    public static string GetPos(ParserRuleContext context)
    {
        //return $"line #{context.Start.Line}, column #{context.Start.Column}";
        return $"Line #{context.Start.Line}";
    }
    public static (int, int) GetPosLineCol(ParserRuleContext context)
    {
        return (context.Start.Line, context.Start.Column);
    }
    public bool AddVariableToScope(ParserRuleContext ctx, Symbol symbol)
    {
        try
        {
            Env[symbol.Name] = symbol;
            return true;
        }
        catch
        {
            if (symbol.Type != Ts.RARITY)
                //_logger.LogError("Error at {pos}: multiple definitions for {symName}", GetPos(ctx), symbol.Name);
                exceptionHandler.AddException(ctx, symbol.Name);
            return false;
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

        //_logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
        return null;
    }
    public bool IsVariableOnScope(string varName)
    {
        var symbol = Env[varName];
        return symbol != null;
        //_logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
    }
    public bool CheckType(string varName, CDLType type)
    {
        var symbol = Env[varName];
        if (symbol == null)
        {
            return false;
        }
        return symbol.Type.InheritsFrom(type);
        //return symbol.Type == type;
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
