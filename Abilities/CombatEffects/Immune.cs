public class Immune : CombatEffect
{
    public Immune(int duration,bool areaEffect) : base(duration,1,eCombatEffect.Immune,areaEffect)
    {
    
    }

    public override void ApplyEffect(Character caster,Character target,List<Character> targetTeam)
    {   
        base.ApplyEffect(caster,target,targetTeam);
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = targetTeam;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            c.ClearAllEffects();
            c.IsImmune = true;
        }
        
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
        return new Immune(Duration,AreaEffect);
    }
}