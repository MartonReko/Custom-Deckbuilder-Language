using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCard(Card gameCard) : GameEntity
    {
        public readonly Card ModelCard = gameCard;
    }
}
