using CDL.Lang;
using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class GameServiceManager
    {
        private GameService? _gameService;

        public bool Initialize(string CdlCode)
        {
            if (_gameService != null)
            {
                throw new InvalidOperationException("Already initialized.");
            }

            LanguageProcessor lp = new();
            ObjectsHelper? objectsHelper = lp.ProcessText(CdlCode);

            if (objectsHelper == null)
            {
                // Input processing failed
                // throw new Exception("Could not initialize from given cdl code.");
                return false;
            }
            else
            {
                _gameService = new(objectsHelper);
                _gameService.Initialize();
                return true;
            }
        }

        public GameService GetService()
        {
            if (_gameService != null)
                return _gameService;
            else
                throw new InvalidOperationException("Game service not yet initialized.");
        }
    }
}

