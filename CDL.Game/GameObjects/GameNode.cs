using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameNode : GameEntity
    {
        public readonly Node ModelNode;
        public List<GameEnemy> Enemies { get; private set; } = [];
        public GameNode(Node node)
        {
            ModelNode = node;

            // Create enemies
            foreach (var enemy in ModelNode.Enemies)
            {
                for (int i = 0; i < enemy.Value; i++)
                {
                    Enemies.Add(new GameEnemy(enemy.Key));
                }
            }
        }
        public bool Cleared()
        {
            // foreach (var e in Enemies)
            // {
            //     Console.WriteLine("Enemy " + e.Id + " health is " + e.Health);
            // }
            // return !Enemies.Exists(x => x.Health > 0);
            return !(Enemies.Count > 0);
        }
        public void AttackEnemy(GameCard card, GameEnemy enemy)
        {
            foreach ((Effect effect, int cnt) in card.ModelCard.EffectsApplied)
            {
                enemy.ApplyEffect(effect, cnt);
            }
            if (enemy.Health <= 0)
            {
                // TODO:
                // Not sure if this works
                // Instead of removal use a status
                Enemies.Remove(enemy);
            }
        }
        public (EnemyAction EnemyAction, EnemyTarget target, int num) EnemyTurn(int idx, GameCharacter player)
        {
            return Enemies[idx].Attack(player);
        }
        public void EndTurn()
        {
            foreach (var enemy in Enemies)
            {
                enemy.EndTurn();
            }

        }
        public (string rarity, int num) GetRewardRarityAndNumber()
        {
            Random r = new();
            int random = r.Next(0, 101);
            int acc = 0;

            foreach (var rarity in ModelNode.RarityNumChance)
            {
                if (acc + rarity.Value.chance > random)
                {
                    return (rarity.Key, rarity.Value.num);
                }
                acc += rarity.Value.chance;
            }
            // Default, definitely should not happen
            // TODO: error message
            throw new InvalidOperationException("Could not generate rewards in node");
            // return (ModelNode.RarityNumChance.First().Key, ModelNode.RarityNumChance.First().Value.num);
        }
    }
}
