namespace CDL.Game.DTOs
{
    public class SCombatDto : IGameDto
    {
        public string PlayerState { get; set; }
        public string CombatState { get; set; }
        public PlayerDto PlayerInfo { get; set; }
        public List<EnemyDto> EnemiesInfo { get; set; } = [];

        public SCombatDto(string playerState, string combatState, PlayerDto playerInfo, List<EnemyDto> enemiesInfo)
        {
            PlayerState = playerState;
            CombatState = combatState;
            PlayerInfo = playerInfo;
            EnemiesInfo = enemiesInfo;
        }
    }
}
