namespace CDL.Game.GameObjects
{
    public abstract class GameEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
