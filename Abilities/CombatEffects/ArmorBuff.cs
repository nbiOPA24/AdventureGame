public class ArmorBuff : CombatEffect
{
    public ArmorBuff(int duration,int magnitude,bool areaEffect) : base(duration,magnitude,eCombatEffect.ArmorBuff,areaEffect)
    {
        
    }

    public override void ApplyEffect(Character caster,Character target,List<Character> targetTeam,List<Character> otherTeam)
    {
        base.ApplyEffect(caster,target,targetTeam,otherTeam);
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = targetTeam;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            c.TempArmor = Magnitude;
        }
    }
    public override void PrintApplication(Character character)
    {

            Utilities.ConsoleWriteColor(character.Name,character.NameColor);
            Utilities.ConsoleWriteColor("s Armor",ConsoleColor.DarkYellow);
            Console.Write($" Has Increased ");
            Console.WriteLine($" for {Duration} rounds");
    }
        public override void AfterRound(Character character)
    {

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
        return new ArmorBuff(Duration,Magnitude,AreaEffect);
    }
}