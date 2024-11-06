public class Immune : CombatEffect
{
    public Immune(int duration) : base(duration,1,eCombatEffect.Immune)
    {
    
    }

    public override void ApplyEffect(Character self,Character target)
    {   
        target.ClearAllEffects();
        base.ApplyEffect(self,target);
        target.IsImmune = true;
        
    }
    public override void PrintApplication(Character character)
    {
        Console.Write($"{character.Name} is ");
        Utilities.ConsoleWriteColor("Immune",ConsoleColor.DarkMagenta);
        Console.WriteLine($" for {Duration} rounds");
    }
    public override void AfterRound(Character character)
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
            if(Duration > 0 )
            {
                Duration--;
            }  
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