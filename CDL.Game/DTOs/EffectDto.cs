namespace CDL.Game.DTOs
{
    public class EffectDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double InDmgMod { get; set; }
        public double OutDmgMod { get; set; }
        public double DamageDealt { get; set; }
       
        public EffectDto(string name, string type, double inDmgMod, double outDmgMod, double damageDealt)
        {
            Name = name;
            Type = type;
            InDmgMod = inDmgMod;
            OutDmgMod = outDmgMod;
            DamageDealt = damageDealt;
        }
    }
}
