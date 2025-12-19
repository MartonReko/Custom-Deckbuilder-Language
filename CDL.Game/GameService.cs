using System.Text.Json.Serialization;
using CDL.Game.GameObjects;
using CDL.Lang.GameModel;
using CDL.Lang.Parsing;
using NLog.Targets;

namespace CDL.Game
{
    public class GameService
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
        // TODO:
        // Block actions upon death
        public PlayerStates PlayerState { get; private set; }
        public CombatStates CombatState { get; private set; }
        public GameCharacter Player { get; private set; }
        public ObjectsHelper Model { get; }
        public GameMap? GameMap { get; private set; }
        public GameNode? CurrentGameNode { get; private set; }
        public List<GameCard> NodeRewards { get; } = [];
        public List<GameCard> Deck { get; } = [];
        public List<GameCard> Hand { get; } = [];
        public List<GameCard> DiscardPile { get; } = [];
        public List<GameCard> DrawPile { get; } = [];

        // How many cards can be used in a turn, maybe tune later
        public int Energy { get; private set; } = 3;

        private readonly Random random = new();
        public GameService(ObjectsHelper gameObjects)
        {
            Model = gameObjects;
            Player = new(gameObjects.Character!);


            GameMap = new GameMap(Model!.Game!, Model.Stages, Model.Nodes);
            GameMap.LoadNextStage();

            CreateDeck();
            PlayerState = PlayerStates.MAP;
        }

        public void DealPlayerDamage(double num)
        {
            Player.Damage(num);
            if (Player.Health < 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
        }


        private void CreateDeck()
        {
            foreach ((Card card, int num) in Model!.Character!.Deck)
            {
                for (int i = 0; i < num; i++)
                {
                    Deck.Add(new GameCard(card));
                }
            }
        }

        public bool Move(Guid nodeId)
        {
            if (!GameMap!.CurrentStage.GameNodesByLevel.TryGetValue(GameMap.LevelCounter, out List<GameNode>? nodes))
            {
                Console.WriteLine("Could not get nodes");
                return false;
            }
            GameNode? node = nodes.FirstOrDefault(x => x.Id.Equals(nodeId));
            if (node == null)
            {
                Console.WriteLine("Node with ID not found!");
                return false;
            }
            //  if (CurrentGameNode != null && !GameMap.CurrentStage.Edges[CurrentGameNode.Id].Contains(nodeId))
            //  {
            //      Console.WriteLine("Invalid move");
            //      return false;
            //  }
            if (CurrentGameNode != null && GameMap.LevelCounter != 0)
            {
                GameMap.CurrentStage.Edges.TryGetValue(CurrentGameNode.Id, out var nextNodes);
                if (!nextNodes.Contains(nodeId))
                {
                    Console.WriteLine("Invalid move");
                    return false;
                }
            }
            if (PlayerState == PlayerStates.MAP && GameMap.MoveTo(node))
            {
                PlayerState = PlayerStates.COMBAT;
                CombatState = CombatStates.PLAYER;
                CurrentGameNode = node;

                (string rarity, int num) = CurrentGameNode.GetRewardRarityAndNumber();
                GenerateRewards(rarity, num);
                // TODO: Set energy in one location with a method
                Energy = 3;
                DiscardPile.Clear();
                Hand.Clear();
                DrawPile.AddRange(Deck);
                GetNextHand();
                return true;
            }
            else
            {
                return false;
            }

        }
        private int handCounter = 0;
        public void GetNextHand()
        {
            DiscardPile.AddRange(Hand);
            Hand.Clear();
            if (DrawPile.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    var card = DrawPile[random.Next(0, DrawPile.Count)];
                    Hand.Add(card);
                    DrawPile.Remove(card);
                }
            }
            else
            {
                foreach (var card in DrawPile)
                {
                    Hand.Add(card);
                }
                DrawPile.Clear();
                DrawPile.AddRange(Deck);
                DiscardPile.Clear();
            }
        }
        private int EnemyTurnCounter;
        public void EndTurn()
        {
            PlayerEndTurn();
            if (PlayerState == PlayerStates.DEATH) return;

            if (CurrentGameNode.Cleared())
            {
                if (GameMap.IsLastOnMap())
                {
                    PlayerState = PlayerStates.WIN;
                }
                else if (GameMap.IsLastOnStage())
                {
                    GameMap.LoadNextStage();
                    // For correct handling in front-end
                    Player.Restore();
                    CurrentGameNode = null;
                    PlayerState = PlayerStates.MAP;
                }
                else
                {
                    PlayerState = PlayerStates.REWARD;
                }
            }
            else
            {
                // All enemies make their turn
                //
                //EnemyTurnCounter = 0;
                //nombatState = CombatStates.ENEMY;
                for (int i = 0; i < CurrentGameNode.Enemies.Count; i++)
                {
                    var turn = NextEnemyTurn();
                }
            }
        }
        public void Cleared()
        {
            if (GameMap.IsLastOnMap())
            {
                PlayerState = PlayerStates.WIN;
            }
            else if (GameMap.IsLastOnStage())
            {
                GameMap.LoadNextStage();
                Player.Restore();
                PlayerState = PlayerStates.REWARD;
            }
            else
            {
                PlayerState = PlayerStates.REWARD;
            }
        }
        public void EndEnemiesTurn()
        {
            CurrentGameNode.EndTurn();
            if (CurrentGameNode.Cleared())
            {
                // if (GameMap.IsLast())
                // {
                //     PlayerState = PlayerStates.WIN;
                // }
                //PlayerState = PlayerStates.REWARD;
                Cleared();
            }
            else
            {
                CombatState = CombatStates.PLAYER;
                EnemyTurnCounter = 0;
                Energy = 3;
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

        // NOTE: Used for temporary player targeting workaround
        public void AttackPlayer(GameCard card)
        {
            foreach ((Effect effect, int cnt) in card.ModelCard.EffectsApplied)
            {
                Player.ApplyCard(effect, cnt);
            }
        }
        public bool PlayCard(Guid cardId, Guid targetId)
        {

            if (!CurrentGameNode.Enemies.Exists(x => x.Id.Equals(targetId)) && targetId != Player.Id)
            {
                throw new InvalidOperationException("Target not valid");
            }
            if (PlayerState != PlayerStates.COMBAT)
            {
                throw new InvalidOperationException("Can't play cards outside of combat");
            }
            if (Energy <= 0)
            {
                throw new InvalidOperationException("Out of energy, can't play any more cards");
            }

            GameCard card = Deck.First(x => x.Id.Equals(cardId));

            if (card.ModelCard.Cost > Energy)
            {
                throw new InvalidOperationException("Not enough energy to play card");
            }

            if (card.ModelCard.ValidTargets.Contains(TargetTypes.ENEMIES) && card.ModelCard.ValidTargets.Contains(TargetTypes.PLAYER))
            {
                AttackPlayer(card);
                CurrentGameNode.AttackEnemies(card, Player.CurrentEffects);
            }
            else if (targetId.Equals(Player.Id) && card.ModelCard.ValidTargets.Contains(TargetTypes.PLAYER))
            {
                // HACK: Temp "fix"
                AttackPlayer(card);
            }
            else if (card.ModelCard.ValidTargets.Contains(TargetTypes.ENEMIES))
            {
                CurrentGameNode.AttackEnemies(card, Player.CurrentEffects);
            }
            else if (card.ModelCard.ValidTargets.Contains(TargetTypes.ENEMY))
            {
                GameEnemy enemy = CurrentGameNode.Enemies.First(x => x.Id.Equals(targetId));
                CurrentGameNode.AttackEnemy(card, enemy, Player.CurrentEffects);

            }
            else
            {
                throw new InvalidOperationException("Card target does not match ValidTargets");
            }
            if (CurrentGameNode.Enemies.Count == 0)
            {
                Cleared();
            }
            Energy -= card.ModelCard.Cost;
            Hand.Remove(card);
            DiscardPile.Add(card);
            return true;
        }

        private void GenerateRewards(string rarity, int cnt)
        {
            List<Card> candidates = [.. Model.Cards.Where(x => x.Rarity.Equals(rarity))];

            if (candidates.Count == 0)
            {
                throw new InvalidOperationException("Rarity not found!");
            }

            for (int i = 0; i < cnt; i++)
            {
                if (candidates.Count != 0)
                {
                    Card cardToAdd = candidates[random.Next(0, candidates.Count)];
                    NodeRewards.Add(new GameCard(cardToAdd));
                    // TODO:
                    // Check if this works
                    candidates.Remove(cardToAdd);
                }
            }
        }
        public bool ChooseReward(Guid Id)
        {
            // TODO:
            // Nicer handling of wrong state
            if (PlayerState != PlayerStates.REWARD) return false;
            // TODO:
            // Card not found error
            GameCard chosen = NodeRewards.Where(x => x.Id.Equals(Id)).First();
            Deck.Add(chosen);
            NodeRewards.Clear();
            PlayerState = PlayerStates.MAP;
            return true;
        }


        // TODO
        // Code was reused from GameEnemy...
        public void PlayerEndTurn()
        {
            Energy = 3;
            List<Effect> toRemove = [];
            foreach (var item in Player.CurrentEffects)
            {
                if (item.Key.EffectType == EffectType.TURNEND)
                {
                    DealPlayerDamage((int)Math.Round(item.Key.DamageDealt));
                    Player.CurrentEffects[item.Key] = item.Value - 1;
                    if (Player.CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
                if (item.Key.EffectType == EffectType.MOD)
                {
                    Player.CurrentEffects[item.Key] = item.Value - 1;
                    if (Player.CurrentEffects[item.Key] <= 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
            }
            foreach (Effect effect in toRemove)
            {
                //character.CurrentEffects.Remove(effect);
                Player.CurrentEffects.Remove(effect);
            }

            // Maybe unnecessary
            if (Player.Health <= 0)
            {
                PlayerState = PlayerStates.DEATH;
            }


            // NOTE: this stays until cards have an energy cost and there is logic for it
            Energy = 3;

            GetNextHand();
        }
        // Also reused code from GameEnemy
        public void PlayerApplyEffect(Effect effect, int cnt)
        {
            if (effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
            {
                if (Model.Character.CurrentEffects.TryGetValue(effect, out int oldCnt))
                {
                    Model.Character.CurrentEffects[effect] = cnt + oldCnt;
                }
                else
                {
                    Model.Character.CurrentEffects.Add(effect, cnt);
                }
            }
            if (effect.EffectType == EffectType.INSTANT)
            {
                DealPlayerDamage((int)Math.Round(effect.DamageDealt));
            }
        }
    }
}
