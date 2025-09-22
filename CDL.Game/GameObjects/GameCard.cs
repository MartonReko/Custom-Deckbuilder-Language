using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCard : GameEntity
    {
        public readonly Card ModelCard;
        public GameCard(Card gameCard) => this.ModelCard = gameCard;
    }
}
