public class Cleanse : CombatEffect
{
    List<eCombatEffect> TypesToCleanse {get;set;}
    public Cleanse(List<eCombatEffect> typesToCleanse) : base(1,1,eCombatEffect.Cleanse)
    {
        TypesToCleanse = typesToCleanse;
    }

    public override void ApplyEffect(Character character)
    {   

        character.IsImmune = true;
        
    }
    public override void PrintApplication(Character character)
    {
        Console.Write($"{character.Name} is ");
        Utilities.ConsoleWriteColor("Immune",ConsoleColor.DarkMagenta);
        Console.WriteLine($" for {Duration} rounds");
    }

    public override CombatEffect CloneEffect()
    {
        return new Immune(Duration);
    }
}