using System.Text.Json.Serialization;
using CDL.Game.GameObjects;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class GameService(ObjectsHelper gameObjects)
    {
        [JsonConverter(typeof(JsonStringEnumConverter<PlayerStates>))]
        public enum PlayerStates
        {
            COMBAT, MAP, DEATH, REWARD, WIN
        }
        public enum CombatStates
        {
            PLAYER, ENEMY
        }
        // TODO
        // Block actions upon death
        public PlayerStates PlayerState { get; private set; }
        public CombatStates CombatState { get; private set; }
        public GameCharacter Player { get; private set; } = new(gameObjects.Character);
        public ObjectsHelper GameObjects { get; } = gameObjects;
        public GameMap? GameMap { get; private set; }
        public GameNode? CurrentGameNode { get; private set; }
        public List<GameCard> Rewards { get; } = [];
        public List<GameCard> Deck { get; } = [];

        private readonly Random random = new();
        public void DealPlayerDamage(double num)
        {
            Player.Damage(num);
            if (Player.Health < 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
        }

        public void Initialize()
        {
            GameMap = new GameMap(GameObjects!.Game!, GameObjects.Stages, GameObjects.Nodes);
            GameMap.LoadNextStage();
            CreateDeck();
            PlayerState = PlayerStates.MAP;
            // TODO: Temporary "fix" because map creation is messed up :(
            // Console.WriteLine(GameMap.CurrentStage.GameNodesByLevel.ToString());
            Move(GameMap.CurrentStage.GameNodesByLevel[0].First().Id);
            foreach (var thing in GameMap.CurrentStage.GameNodesByLevel)
            {
                Console.WriteLine($"{thing.Key} : {thing.Value.FirstOrDefault().ModelNode.Name}");
            }
            Console.WriteLine("Inited");
        }
        private void CreateDeck()
        {
            foreach ((Card card, int num) in GameObjects!.Character!.Deck)
            {
                Deck.Add(new GameCard(card));
            }
        }

        // WARN: Forgot i had this function...
        public List<GameNode> GetMoves()
        {
            return GameMap.GetPossibleSteps();
        }

        // TODO: Probably implement checks for movement validity here?
        // TODO: Should I use Guid or GameNode?
        public bool Move(Guid nodeId)
        {
            // WARN: Really have to fix all these null warnings
            if (!GameMap!.CurrentStage.GameNodesByLevel.TryGetValue(GameMap.LevelCounter, out List<GameNode>? nodes))
            {
                // Throw error....
                return false;
            }
            GameNode? node = nodes.FirstOrDefault(x => x.Id.Equals(nodeId));
            if (node == null)
            {
                //TODO: Need to do some logging and returning error messages
                return false;
            }
            if (PlayerState == PlayerStates.MAP && GameMap.MoveTo(node))
            {
                //GameNode gameNode = new(node);
                PlayerState = PlayerStates.COMBAT;
                CombatState = CombatStates.PLAYER;
                CurrentGameNode = node;

                // TODO:
                // Causes sequence contains no elements exception
                //(string rarity, int num) = CurrentGameNode.GetRewardRarityAndNumber();
                //GenerateRewards(rarity, num);
                return true;
            }
            else
            {
                return false;
            }

        }
        private int EnemyTurnCounter;
        public void EndTurn()
        {
            PlayerEndTurn();
            if (PlayerState == PlayerStates.DEATH) return;

            if (CurrentGameNode.Cleared())
            {
                PlayerState = PlayerStates.REWARD;
            }
            else
            {
                EnemyTurnCounter = 0;
                CombatState = CombatStates.ENEMY;
            }
        }
        public void EndEnemiesTurn()
        {
            CurrentGameNode.EndTurn();
            if (CurrentGameNode.Cleared())
            {
                if (GameMap.IsLast())
                {
                    PlayerState = PlayerStates.WIN;
                }
                PlayerState = PlayerStates.REWARD;
            }
            else
            {
                CombatState = CombatStates.PLAYER;
                EnemyTurnCounter = 0;
            }
        }

        public (EnemyAction EnemyAction, EnemyTarget target, int num) NextEnemyTurn()
        {
            var result = CurrentGameNode.EnemyTurn(EnemyTurnCounter, Player);
            // Reset to 0 on enemies turn end
            EnemyTurnCounter++;
            if (EnemyTurnCounter >= CurrentGameNode.Enemies.Count)
            {
                EndEnemiesTurn();
            }
            if (Player.Health < 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
            return result;
        }

        public void PlayCard(Card card, GameEnemy enemy)
        {
            if (PlayerState != PlayerStates.COMBAT)
            {
                // TODO
                // Should error
                return;
            }
            CurrentGameNode.AttackEnemy(card, enemy);
            if (CurrentGameNode.Enemies.Count == 0)
            {
                PlayerState = PlayerStates.REWARD;
            }
        }
        // TODO
        // Could probably create a dictionary for rarities
        private void GenerateRewards(string rarity, int cnt)
        {
            List<Card> possibleCards = [];
            foreach (Card card in GameObjects.Cards)
            {
                if (card.Rarity == rarity)
                {
                    possibleCards.Add(card);
                }
            }
            List<GameCard> cardsChosen = [];
            for (int i = 0; i < cnt; i++)
            {
                if (possibleCards.Count != 0)
                {
                    Card cardToAdd = possibleCards[random.Next(0, possibleCards.Count)];
                    Rewards.Add(new GameCard(cardToAdd));
                    // TODO
                    // Check if this works
                    possibleCards.Remove(cardToAdd);
                }
                else
                {
                    // Maybe there arent enough unique cards for a rarity, then duplicate last one
                    Rewards.Add(cardsChosen.Last());
                }
            }
        }
        public void ChooseReward(Guid Id)
        {
            // TODO
            // Nicer handling of wrong state
            if (PlayerState != PlayerStates.REWARD) return;
            // TODO
            // Card not found error
            GameCard chosen = Rewards.Where(x => x.Id.Equals(Id)).First();
            Deck.Add(chosen);
            PlayerState = PlayerStates.MAP;
        }

        public void PlayCard(GameCard card)
        {
            if (PlayerState != PlayerStates.COMBAT)
            {
                foreach ((Effect effect, int cnt) in card.ModelCard.EffectsApplied)
                {
                    PlayerApplyEffect(effect, cnt);
                }
                return;
            }
        }

        // TODO
        // Code was reused from GameEnemy...
        public void PlayerEndTurn()
        {
            List<Effect> toRemove = [];
            ModelCharacter character = GameObjects.Character!;
            foreach (var item in character.CurrentEffects)
            {
                if (item.Key.EffectType == EffectType.TURNEND)
                {
                    DealPlayerDamage((int)Math.Round(item.Key.DamageDealt));
                    character.CurrentEffects[item.Key] = item.Value - 1;
                    if (character.CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
                if (item.Key.EffectType == EffectType.MOD)
                {
                    character.CurrentEffects[item.Key] = item.Value - 1;
                    if (character.CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
            }
            foreach (Effect effect in toRemove)
            {
                character.CurrentEffects.Remove(effect);
            }

            // Maybe unnecessary
            if (character.Health <= 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
        }
        // Also reused code from GameEnemy
        public void PlayerApplyEffect(Effect effect, int cnt)
        {
            if (effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
            {
                if (GameObjects.Character.CurrentEffects.TryGetValue(effect, out int oldCnt))
                {
                    GameObjects.Character.CurrentEffects[effect] = cnt + oldCnt;
                }
                else
                {
                    GameObjects.Character.CurrentEffects.Add(effect, cnt);
                }
            }
            if (effect.EffectType == EffectType.INSTANT)
            {
                DealPlayerDamage((int)Math.Round(effect.DamageDealt));
            }
        }
    }
}
