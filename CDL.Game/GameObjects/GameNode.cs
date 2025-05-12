using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameNode   
    {
        public readonly Node ModelNode;
        public List<GameEnemy> Enemies { get; private set; } = [];
        public GameNode(Node node)
        {
            ModelNode = node;
            CreateEnemies();
        }
        private void CreateEnemies()
        {
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
            return Enemies.Exists(x=>x.Health > 0);
        }
        public void AttackEnemy(Card card, GameEnemy enemy)
        {
            foreach((Effect effect, int cnt) in card.EffectsApplied)
            {
                enemy.ApplyEffect(effect, cnt);
            }
            if(enemy.Health <= 0)
            {
                // Not sure if this works
                // Enemies.Remove(enemy);
                // Instead of removal use a status
            }
        }
        public (EnemyAction EnemyAction, EnemyTarget target, int num) EnemyTurn(int idx,GameCharacter player)
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
        public (string rarity,int num) GetRewardRarityAndNumber()
        {
            Random r = new Random();
            int random = r.Next(0,101);
            int tmp = 100;
            foreach(var rarity in ModelNode.RarityNumChance)
            {
                if(tmp - rarity.Value.chance <= random)
                {
                    return (rarity.Key,rarity.Value.num);
                }
            }
            // Default, definitely should not happen
            // TODO error message
            return (ModelNode.RarityNumChance.First().Key,ModelNode.RarityNumChance.First().Value.num);
        }
    }
}
