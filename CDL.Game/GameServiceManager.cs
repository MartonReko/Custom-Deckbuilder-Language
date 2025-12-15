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
        public List<CDLException> CDLExceptions { get; private set; }

        public void Initialize(string CdlCode)
        {
            LanguageProcessor lp = new();
            (ObjectsHelper?, CDLExceptionHandler) lpReturnValue = lp.ProcessText(CdlCode);
            CDLExceptions = lpReturnValue.Item2.GetExceptions();

            if (lpReturnValue.Item1 == null)
            {
                throw new InvalidOperationException("Failed to parse code.");
            }
            else
            {
                _gameService = new(lpReturnValue.Item1);
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

