using CDL.exceptions;
using CDL.game;

namespace CDL;

public class ObjectsHelper(EnvManager em, CDLExceptionHandler exceptionHandler){

    // All game objects are stored in these lists
    public GameSetup? Game { get; set; }
    public GameCharacter? Character { get; set; }
    public List<Stage> Stages { get; set; } = [];
    public List<Node> Nodes { get; set; } = [];
    public List<Enemy> Enemies { get; set; } = [];
    public List<Effect> Effects { get; set; } = [];
    public List<Card> Cards { get; set; } = [];

    public bool IsValid(){
        bool validity = true;
        if(Game == null){
            exceptionHandler.AddException("Game missing");
            validity = false;
        }else if(Game.GameName == null || Game.Player == null || Game.Stages.Count == 0){
            exceptionHandler.AddException("Game definition missing settings");
            validity = false;
        }

        return validity;
    }
}