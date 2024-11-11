public class Freeze : CombatEffect
{
    public Freeze(int duration,bool areaEffect) : base(duration,1,eCombatEffect.Freeze,areaEffect)
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
            c.AbleToAct = false;
        }
    }
    public override void PrintApplication(Character character)
    {

            Utilities.ConsoleWriteColor(character.Name,character.NameColor);
            Console.Write($" Has been ");
            Utilities.ConsoleWriteColor("Frozen",ConsoleColor.Blue);
            Console.WriteLine($" for {Duration} rounds");
    }
    public override void AfterTurn(Character character)
    {
        //reduces duration by 1round if its not the first round
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
    public override void AfterRound(Character character)
    {
        Utilities.ConsoleWriteColor(character.Name,character.NameColor);
        Console.Write(" is ");
        Utilities.ConsoleWriteColor("Frozen ",ConsoleColor.Blue);
        Console.WriteLine($"for {Duration}more rounds");
    }
    public override CombatEffect CloneEffect()
    {
        return new Freeze(Duration,AreaEffect);
    }

}