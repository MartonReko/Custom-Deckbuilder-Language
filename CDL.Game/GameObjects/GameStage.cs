using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameStage(Stage model) : GameEntity
    {
        public readonly Stage ModelStage = model;
        //public Dictionary<int, List<Node>> NodesByLevel { get; private set; } = [];
        public Dictionary<int, List<GameNode>> GameNodesByLevel { get; private set; } = [];

        public void Init()
        {
            Random r = new();
            Stack<GameNode> gameNodesUnordered = new();

            // Fill unordered nodes stack with nodes
            // that must be present on the stage
            foreach (var item in ModelStage.MustContain)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    gameNodesUnordered.Push(new(item.Key));
                }
            }

            // Initialize list for each level in stage
            // Length - 1 so there is space for boss node
            for (int i = 0; i < ModelStage.StageLength - 1; i++)
            {
                GameNodesByLevel.Add(i, []);
            }

            // Randomly fill with nodes that are required
            while (gameNodesUnordered.Count > 0)
            {
                // Length - 1 so there is space for boss node
                for (int i = 0; i < ModelStage.StageLength - 1; i++)
                {
                    if (GameNodesByLevel[i].Count < ModelStage.StageWidthMax)
                    {
                        if (gameNodesUnordered.Count > 0)
                        {
                            if (r.Next(0, 2) == 0)
                            {
                                GameNodesByLevel[i].Add(gameNodesUnordered.Pop());
                            }
                        }
                    }
                    //  else
                    //  {
                    //      throw new InvalidOperationException("Fatal error in creating stage");
                    //      // Error, but should not happen because it is checked in CDL.Lang
                    //  }
                }
            }

            // Finally fill with nodes given in FillWith property
            for (int i = 0; i < ModelStage.StageLength - 1; i++)
            {
                // max : 3 min : 1 cnt : 2 
                if (GameNodesByLevel[i].Count < ModelStage.StageWidthMax)
                {
                    int spotsLeft = (int)ModelStage.StageWidthMax - GameNodesByLevel[i].Count;
                    int lowerBound = (int)ModelStage.StageWidthMin! - GameNodesByLevel[i].Count;
                    if (lowerBound < 0) lowerBound = 0;
                    int spotsToFill = r.Next(lowerBound, spotsLeft);
                    for (int j = 0; j < spotsToFill; j++)
                    {
                        // From the FillWith node list, choose a random node and add it
                        // to the stage level
                        GameNodesByLevel[i].Add(new(ModelStage.FillWith[r.Next(0, ModelStage.FillWith.Count - 1)]));
                    }
                }
            }

            GameNode bossNode = new(ModelStage.EndsWith);
            // Also add last node (bossnode) to the end
            GameNodesByLevel.Add(GameNodesByLevel.Last().Key + 1, [bossNode]);

        }
    }
}
