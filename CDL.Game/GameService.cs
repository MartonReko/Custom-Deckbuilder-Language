using CDL.Lang.GameModel;
using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class GameService(ObjectsHelper gameObjects)
    {
        public enum PlayerStates
        {
            COMBAT, MAPMOVE, DEATH, REWARD, ENEMYTURN
        }
        // TODO
        // Block actions upon death
        public PlayerStates PlayerState { get; private set; }
        private ObjectsHelper GameObjects { get; } = gameObjects;
        public GameMap? GameMap { get; private set; } 
        public GameNode? CurrentGameNode { get; private set; }
        public List<Card> Rewards { get; private set; } = [];
        private readonly Random random = new();
        public void DealPlayerDamage(int num)
        {
            GameObjects.Character.Health -= num;
            if(GameObjects.Character.Health < 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
        }

        public void Initialize()
        {
            GameMap = new GameMap(GameObjects!.Game!, GameObjects.Stages, GameObjects.Nodes);
            GameMap.LoadNextStage();
            PlayerState = PlayerStates.MAPMOVE;
        }

        public List<Node> GetMoves()
        {
            return GameMap.GetPossibleSteps();
        }

        public bool Move(Node node)
        {
            if (PlayerState == PlayerStates.MAPMOVE && GameMap.MoveTo(node))
            {
                GameNode gameNode = new(node);
                PlayerState = PlayerStates.COMBAT;
                CurrentGameNode = gameNode;
                (string rarity, int num) = CurrentGameNode.GetRewardRarityAndNumber();
                Rewards = GetPossibleRewards(rarity,num);
                return true;
            }else {
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
                PlayerState = PlayerStates.ENEMYTURN;
                EnemyTurnCounter = 0;
            }
        }
        public void EndEnemiesTurn()
        {
            CurrentGameNode.EndTurn();
            if (CurrentGameNode.Cleared())
            {
                PlayerState = PlayerStates.REWARD;
            }
            else
            {
                PlayerState = PlayerStates.COMBAT;
                EnemyTurnCounter = 0;
            }
        }

        public (EnemyAction EnemyAction, EnemyTarget target, int num) NextEnemyTurn()
        {
            var result = CurrentGameNode.EnemyTurn(EnemyTurnCounter, GameObjects.Character);
            // Reset to 0 on enemies turn end
            EnemyTurnCounter++;
            if(EnemyTurnCounter >= CurrentGameNode.Enemies.Count)
            {
                EndEnemiesTurn();
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
            if(CurrentGameNode.Enemies.Count == 0)
            {
                PlayerState = PlayerStates.REWARD;
            }
        }
        // TODO
        // Could probably create a dictionary for rarities
        private List<Card> GetPossibleRewards(string rarity, int cnt)
        {
            List<Card> possibleCards = [];
            foreach(Card card in GameObjects.Cards)
            {
                if(card.Rarity == rarity)
                {
                    possibleCards.Add(card);
                }
            }
            List<Card> cardsChosen = [];
            for (int i = 0; i < cnt; i++)
            {
                if(possibleCards.Count != 0)
                {
                    Card cardToAdd = possibleCards[random.Next(0, possibleCards.Count)];
                    cardsChosen.Add(cardToAdd);
                    // TODO
                    // Check if this works
                    possibleCards.Remove(cardToAdd);
                }
                else
                {
                    // Maybe there arent enough unique cards for a rarity, then duplicate last one
                    cardsChosen.Add(cardsChosen.Last());
                }
            }
            return possibleCards;
        }
        public void ChooseReward(int idx)
        {
            // TODO
            // Nicer handling of wrong state
            if (PlayerState != PlayerStates.REWARD) return;
            Card chosen = Rewards[idx];
            if(GameObjects.Character.Deck.TryGetValue(chosen, out int cnt))
            {
                GameObjects.Character.Deck[chosen] = cnt + 1;
            }
            else
            {
                GameObjects.Character.Deck.Add(chosen,1);
            }
            PlayerState = PlayerStates.MAPMOVE;
        }

        public void PlayCard(Card card)
        {
            
            if (PlayerState != PlayerStates.COMBAT)
            {
                foreach((Effect effect, int cnt) in card.EffectsApplied)
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
            GameCharacter character = GameObjects.Character!;
            foreach (var item in character.CurrentEffects)
            {
                if (item.Key.EffectType == EffectType.TURNEND)
                {
                    DealPlayerDamage((int)Math.Round(item.Key.DamageDealt));
                    character.CurrentEffects[item.Key] = item.Value - 1;
                    if(character.CurrentEffects[item.Key] == 0)
                    {
                        toRemove.Add(item.Key);
                    }
                }
                if (item.Key.EffectType == EffectType.MOD)
                {
                    character.CurrentEffects[item.Key] = item.Value - 1;
                    if(character.CurrentEffects[item.Key] == 0)
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
            if(character.Health <= 0)
            {
                PlayerState = PlayerStates.DEATH;
            }
        }
        // Also reused code from GameEnemy
        public void PlayerApplyEffect(Effect effect, int cnt)
        {
            if(effect.EffectType == EffectType.MOD || effect.EffectType == EffectType.TURNEND)
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
            if(effect.EffectType == EffectType.INSTANT)
            {
                DealPlayerDamage((int)Math.Round(effect.DamageDealt));
            }
        }
    }
}
