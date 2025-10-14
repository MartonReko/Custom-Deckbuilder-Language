using CDL.Lang.GameModel;

namespace CDL.Game.GameObjects
{
    public class GameMap : GameEntity
    {
        private readonly GameSetup GameProps;
        private readonly List<Stage> StagesProps;
        private readonly List<Node> NodesProps;
        public int StageCounter { get; private set; } = 0;
        public GameStage CurrentStage { get; private set; }
        public int LevelCounter { get; private set; } = 0;
        public GameNode? CurrentNode { get; private set; } = null;

        public GameMap(GameSetup game, List<Stage> stages, List<Node> Nodes)
        {
            GameProps = game;
            StagesProps = stages;
            NodesProps = Nodes;

            CurrentStage = new GameStage(GameProps.Stages[StageCounter]);
            CurrentStage.Init();
            LevelCounter = 0;
            StageCounter = 0;
        }

        public void LoadNextStage()
        {
            CurrentStage = new GameStage(GameProps.Stages[StageCounter]);
            CurrentStage.Init();
            StageCounter++;
            LevelCounter = 0;
        }

        public List<GameNode> GetPossibleSteps()
        {
            if (LevelCounter == CurrentStage.GameNodesByLevel.Count)
            {
                LoadNextStage();
            }
            return CurrentStage.GameNodesByLevel[LevelCounter];
        }
        public bool IsLastOnStage()
        {
            bool isLast = LevelCounter == CurrentStage.GameNodesByLevel.Count;
            return isLast;
        }
        public bool IsLastOnMap()
        {
            bool isLast = IsLastOnStage() && StageCounter == GameProps.Stages.Count;
            Console.WriteLine($"Scounter: {StageCounter}, SCount: {GameProps.Stages.Count}, LevelCounter: {CurrentStage.GameNodesByLevel.Count}");
            return isLast;
        }
        public bool MoveTo(GameNode node)
        {
            if (CurrentStage.GameNodesByLevel[LevelCounter].Contains(node))
            {
                LevelCounter++;
                CurrentNode = node;
                return true;
            }
            else
            {
                // TODO: error cant move there
                return false;
            }
        }
    }
}
