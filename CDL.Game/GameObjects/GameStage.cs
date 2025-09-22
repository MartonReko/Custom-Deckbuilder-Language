using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameStage(Stage model) : GameEntity
    {
        private readonly Stage ModelStage = model;
        public Dictionary<int, List<Node>> NodesByLevel { get; private set; } = [];

        public void Init()
        {
            Random r = new();
            Stack<Node> nodesUnordered = new();

            // Fill unordered nodes stack with nodes
            // that must be present on the stage
            foreach (var item in ModelStage.MustContain)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    nodesUnordered.Push(item.Key);
                }
            }

            // Initialize list for each level in stage
            // And randomly fill with nodes that are required
            while (nodesUnordered.Count > 0)
            {
                // Length - 1 so there is space for boss node
                for (int i = 0; i < ModelStage.StageLength - 1; i++)
                {
                    if (!NodesByLevel.TryGetValue(i, out List<Node> val))
                    {
                        List<Node> nodesOnLevel = [];
                        if (nodesUnordered.Count > 0)
                        {
                            if (r.Next(0, 2) == 0)
                            {
                                nodesOnLevel.Add(nodesUnordered.Pop());
                            }
                        }
                        NodesByLevel.Add(i, nodesOnLevel);
                    }
                    else if (NodesByLevel[i].Count < ModelStage.StageWidthMax)
                    {
                        if (nodesUnordered.Count > 0)
                        {
                            if (r.Next(0, 2) == 0)
                            {
                                NodesByLevel[i].Add(nodesUnordered.Pop());
                            }
                        }
                    }
                    else
                    {
                        // Error, but should not happen because it is checked in CDL.Lang
                    }
                }
            }

            // Finally fill with nodes given in FillWith property
            for (int i = 0; i < ModelStage.StageLength - 1; i++)
            {
                // max : 3 min : 1 cnt : 2 
                if (NodesByLevel[i].Count < ModelStage.StageWidthMax)
                {
                    int spotsLeft = (int)ModelStage.StageWidthMax - NodesByLevel[i].Count;
                    int lowerBound = (int)ModelStage.StageWidthMin! - NodesByLevel[i].Count;
                    if (lowerBound < 0) lowerBound = 0;
                    int spotsToFill = r.Next(lowerBound, spotsLeft);
                    for (int j = 0; j < spotsToFill; j++)
                    {
                        // From the FillWith node list, choose a random node and add it
                        // to the stage level
                        NodesByLevel[i].Add(ModelStage.FillWith[r.Next(0, ModelStage.FillWith.Count - 1)]);
                    }
                }
            }

            // Also add last node to the end
            NodesByLevel.Add(NodesByLevel.Last().Key + 1, [ModelStage.EndsWith]);

        }
    }
}
