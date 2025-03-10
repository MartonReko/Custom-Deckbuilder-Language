using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace CDL;

public class CDLVisitor(ILoggerFactory loggerFactory) : CDLBaseVisitor<object>
{
    Env env;
    TypeSystem ts;
    private static string GetPos(ParserRuleContext context)
    {
        return $"line #{context.Start.Line}, column #{context.Start.Column}";
    }
    private void AddVariableToScope(ParserRuleContext ctx, Symbol symbol)
    {
        try
        {
            env[symbol.Name] = symbol;
        }
        catch
        {
            _logger.LogError("Error at {pos}: variable {symName} is already in scope", GetPos(ctx), symbol.Name);
        }
    }
    private Symbol getVariableFromScope(ParserRuleContext context, string varName)
    {
        var symbol = env[varName];
        if (symbol != null) return symbol;
        _logger.LogError("Error at {pos}: name {varName} does not exist", GetPos(context), varName);
        return null;
    }
    private CDLType GetType(CDLParser.LiteralExpressionContext context)
    {
        if (context.varName() != null)
        {
            Symbol symbol = null;
            var varName = context.varName();
            symbol = getVariableFromScope(varName, varName.GetText());
            return symbol?.Type ?? ts.ERROR;
        }
        if (context.INT() != null)
            return ts.INT;
        if (context.boolean() != null)
            return ts.BOOLEAN;
        if (context.STRING() != null)
            return ts.STRING;
        if (context.DOUBLE() != null)
            return ts.DOUBLE;

        return null;
    }
    private readonly ILogger<CDLVisitor> _logger = loggerFactory.CreateLogger<CDLVisitor>();

    public override object Visit(IParseTree tree)
    {
        return base.Visit(tree);
    }
    public override object VisitProgram([NotNull] CDLParser.ProgramContext context)
    {
        env = new Env();
        ts = new TypeSystem();
        int gameSetupCount = 0;
        int charSetupCount = 0;
        foreach (var child in context.children)
        {
            if (child.GetChild(0) is CDLParser.GameSetupContext)
            {
                gameSetupCount++;
            }
            if (child.GetChild(0) is CDLParser.CharSetupContext)
                charSetupCount++;

            if (child is CDLParser.VariableDeclarationContext context1)
            {
                //TODO gets visited twice
                //VisitVariableDeclaration(context1);

                Visit(context1);
            }
        }
        if (gameSetupCount == 0)
            _logger.LogError("Missing game setup");
        else if (gameSetupCount > 1)
            _logger.LogError("Multiple game setups found");
        if (charSetupCount == 0)
            _logger.LogError("Missing char setup");
        else if (charSetupCount > 1)
            _logger.LogError("Multiple char setups found");

        var result = base.VisitProgram(context);
        return result;
    }
    public override object VisitConfigBlock([NotNull] CDLParser.ConfigBlockContext context)
    {
        //this.env = new Env(this.env);
        _logger.LogInformation("Config block \"{name}\" visited, Env:\n{env}", context.GetChild(0).GetType(), env.ToString());
        var result = base.VisitConfigBlock(context);
        //this.env = this.env.PrevEnv;
        return result;
    }
    public override object VisitVariableDeclaration([NotNull] CDLParser.VariableDeclarationContext context)
    {
        var varName = context.varName().GetText();

        var type = ts[context.typeName().GetText()];
        if (type == null)
        {
            _logger.LogError("Unknown type \"{type}\" at {pos}", context.typeName().GetText(), GetPos(context));
        }
        else
        {
            if (context.literalExpression() == null)
            {
                _logger.LogError("Undefined variable, this should not happen");
            }
            else
            {
                var expressionType = GetType(context.literalExpression());
                if (!expressionType.InheritsFrom(type))
                {
                    _logger.LogError("Error at {pos}, type {type} of expression is not compatible with the type {typeName} of the variable", GetPos(context.literalExpression()), expressionType.Name, type.Name);
                }
                else
                {
                    var symbol = new Symbol(context.varName().GetText(), type);
                    AddVariableToScope(context.varName(), symbol);
                }
            }
        }

        _logger.LogInformation("Var declaration visited, enviroment:\n{env}", this.env.ToString());
        return base.VisitVariableDeclaration(context);
    }
    public override List<CDLType> VisitParamsDef([NotNull] CDLParser.ParamsDefContext context)
    {
        string propsString = "";
        List<CDLType> typesInProps = new List<CDLType>();
        //return base.VisitParamsDef(context);
        foreach(var item in context.typeName()){
            if(item != null){
                propsString += item.GetText();
                typesInProps.Add(ts[item.GetText()]);
            }
        }
        //return "(" + propsString + ")";
        return typesInProps;
    }
    public override object VisitEffectDefinition([NotNull] CDLParser.EffectDefinitionContext context)
    {
        var type = ts[context.GetChild(0).GetText()];
        string symbolText = context.varName().GetText();
        List<CDLType> props = new List<CDLType>();
        if(context.paramsDef() != null){
            props = VisitParamsDef(context.paramsDef());
        }
        //System.Console.WriteLine(props);
        var symbol = new Symbol(symbolText, type,props);
        AddVariableToScope(context.varName(), symbol);

        var effectName = context.GetChild(1).GetText();

        Effect newEffect = new(effectName);

        // TODO
        // visit effect config and return values OR .getChild()

        //var effectName = context.GetChild(1).GetText();
        _logger.LogInformation("Effect definition visited, enviroment:\n{env}", this.env.ToString());
        return base.VisitEffectDefinition(context);
    }
    public override object VisitCardDefinition([NotNull] CDLParser.CardDefinitionContext context)
    {
        var type = ts[context.GetChild(0).GetText()];
        var symbol = new Symbol(context.varName().GetText(), type);
        AddVariableToScope(context.varName(), symbol);
        return base.VisitCardDefinition(context);
    }
    public override object VisitGameSetup([NotNull] CDLParser.GameSetupContext context)
    {
        return base.VisitGameSetup(context);
    }
    public override object VisitCharSetup([NotNull] CDLParser.CharSetupContext context)
    {
        var charName = context.varName().GetText();
        var health = context.charProperties().number(0).GetText();
        _logger.LogDebug("Context: {c}\nName: {n}\nHealth: {h}", context.GetType(), charName, health);
        return base.VisitCharSetup(context);
    }
}