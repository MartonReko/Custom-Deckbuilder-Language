namespace CDL.game;

public class Node(string name){
    public string Name { get; set; } = name;
    // Enemy and number of it
    public Dictionary<Enemy, int> Enemies { get; set; } = [];
    // Card rarity string and number and chance combo
    public Dictionary<string,(int,int)> RarityNumChance { get; set; } = [];
}