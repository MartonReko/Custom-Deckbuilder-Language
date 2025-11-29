using System.Diagnostics.CodeAnalysis;
using CDL.Game.DTOs;
using CDL.Lang;
using CDL.Lang.Exceptions;
using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class GameServiceManager
    {
        private GameService? _gameService;
        private string storeddlCode = "";

        public void Reset()
        {
            _gameService = null;
            //Initialize(storeddlCode);
        }

        public CDLExceptionHandler? Initialize(string CdlCode)
        {
            if (_gameService != null)
            {
                throw new InvalidOperationException("Already initialized.");
            }

            LanguageProcessor lp = new();
            (ObjectsHelper?, CDLExceptionHandler) lpReturnValue = lp.ProcessText(CdlCode);
            ObjectsHelper objectsHelper = lpReturnValue.Item1;
            CDLExceptionHandler excHandler = lpReturnValue.Item2;

            if (objectsHelper == null)
            {
                return excHandler;
                //throw new InvalidOperationException("Invalid code.");
            }
            else
            {
                _gameService = new(objectsHelper);
                storeddlCode = CdlCode;
                //_gameService.Initialize();
                return null;
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

