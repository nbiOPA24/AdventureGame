public class Immune : CombatEffect
{
    public Immune(int duration,bool areaEffect) : base(duration,1,eCombatEffect.Immune,areaEffect)
    {
    
    }

    public override void ApplyEffect(Character caster,Character target)
    {   
        base.ApplyEffect(caster,target);
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = target.FriendList;
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
        if(!FirstRound)
        {   
            //reduces duration by 1round if its not the first round
            if(Duration > 0 )
            {
                Duration--;
            }  
        }
        else
        {
            FirstRound = false;
        }
        if(Duration == 0)
        {
            character.IsImmune = false;
        }
        else
        {
            Console.Write($"{character.Name} is ");
            Utilities.ConsoleWriteColor("Immune ",ConsoleColor.DarkMagenta);
            Console.WriteLine($" for {Duration}more rounds");
            
        }
        

    }
    public override CombatEffect CloneEffect()
    {
        return new Immune(Duration,AreaEffect);
    }
}