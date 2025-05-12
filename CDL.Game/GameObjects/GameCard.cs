using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameCard
    {
        public readonly Card ModelCard;
        public Guid Id { get; } = Guid.NewGuid();
        public GameCard(Card gameCard)
        {
            this.ModelCard = gameCard;
        }
    }
}
