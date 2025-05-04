using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class GameService(ObjectsHelper gameObjects)
    {
        private ObjectsHelper GameObjects { get; } = gameObjects;
        
        public string GetValue()
        {
            return GameObjects!.Game!.GameName!; 
        }
    }
}
