namespace CDL;

public class Effect{
    public string Name { get; set; }
    public string? Type {get; set;}
    public Effect(string name){
        this.Name = name;
    }
}