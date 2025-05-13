using CDL.Lang.Exceptions;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing.Symboltable;

namespace CDL.Lang.Parsing;

public class ObjectsHelper(EnvManager em, CDLExceptionHandler exceptionHandler)
{

    // All game objects are stored in these lists
    public GameSetup? Game { get; set; }
    public ModelCharacter? Character { get; set; }
    public List<Stage> Stages { get; set; } = [];
    public List<Node> Nodes { get; set; } = [];
    public List<Enemy> Enemies { get; set; } = [];
    public List<Effect> Effects { get; set; } = [];
    public List<Card> Cards { get; set; } = [];
    public List<EnemyAction> EnemyActions { get; set; } = [];

    public void CheckObjectsValidity()
    {

        // Checks for Game
        if (Game == null)
        {
            exceptionHandler.AddException("Game missing");
        }
        else
        {
            if (Game.GameName == null)
            {
                exceptionHandler.AddException("Game missing name");
            }
            if (Game.Player == null)
            {
                exceptionHandler.AddException("Game missing player");
            }
            if (Game.Stages.Count == 0)
            {
                exceptionHandler.AddException("Game missing stages");
            }
        }

        // Checks for Stages
        if (Stages.Count == 0)
        {
            exceptionHandler.AddException("No stages were defined");
        }
        else
        {
            foreach (Stage s in Stages)
            {
                if (s.StageLength < 1)
                {
                    exceptionHandler.AddException($"Invalid length for stage {s.Name}");
                }
                if (s.StageWidthMin < 1)
                {
                    exceptionHandler.AddException($"Invalid min width for stage {s.Name}");
                }
                if (s.StageWidthMax < 1)
                {
                    exceptionHandler.AddException($"Invalid max width for stage {s.Name}");
                }
                if (s.FillWith.Count == 0)
                {
                    exceptionHandler.AddException($"Fill with must not be empty for stage {s.Name}");
                }
                if (s.EndsWith == null)
                {
                    exceptionHandler.AddException($"No end node defined for stage {s.Name}");
                }
            }
        }

        // Checks for Nodes
        if (Nodes.Count == 0)
        {
            exceptionHandler.AddException("No nodes were defined");
        }
        else
        {
            foreach (Node n in Nodes)
            {
                if (n.Enemies.Count < 0)
                {
                    exceptionHandler.AddException($"Invalid number of enemies for node {n.Name}");
                }
            }
        }

        // Checks for character
        if (Character == null)
        {
            exceptionHandler.AddException("Character definition missing");
        }
        else
        {
            if (Character.Health < 1)
            {
                exceptionHandler.AddException($"Invalid value for character health");
            }
            if (Character.Deck.Count < 1)
            {
                exceptionHandler.AddException("Deck must include at least one card");
            }
        }

        //Checks for enemies        
        foreach (Enemy e in Enemies)
        {
            if (e.Health < 1)
            {
                exceptionHandler.AddException($"Invalid value for health for enemy {e.Name}");
            }
        }

        //Checks for effects
        foreach (Effect e in Effects)
        {
            if(e.EffectType == EffectType.MOD && e.DamageDealt != 0)
            {
                exceptionHandler.AddException($"{e.Name}: Effects must either be passive or active, not mixed");
            }
        }


    }
}