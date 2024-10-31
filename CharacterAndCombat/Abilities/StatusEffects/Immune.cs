public class Immune : CombatEffect
{
    public Immune(int duration) : base(duration,1,eCombatEffect.Immune)
    {
    
    }

    public override void ApplyEffect(Character character)
    {   
        base.ApplyEffect(character);
        character.IsImmune = true;
    }
    public override void PrintApplication(Character character)
    {
        Console.Write($"{character.Name} is ");
        Utilities.ConsoleWriteColor("Immune",ConsoleColor.DarkMagenta);
        Console.WriteLine($" for {Duration} rounds");
    }
        public override void EndOfRound(Character character)
    {
        if(Duration == 0)
        {
            Console.WriteLine($"{character.Name} is no longer immune to damage ");
            character.IsImmune = false;
        }
        else
        {
            Console.Write($"{character.Name} is ");
            Utilities.ConsoleWriteColor("Immune ",ConsoleColor.DarkMagenta);
            Console.WriteLine($" for {Duration}more rounds");
            
        }
        //reduces duration by 1round
        if(!FirstRound)
        {
            base.EndOfRound(character);
        }
        else
        {
            FirstRound = false;
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Immune(Duration);
    }
}