public class AttackBuff : CombatEffect
{
    public AttackBuff(int duration,int magnitude,bool areaEffect) : base(duration,magnitude,eCombatEffect.AttackBuff,areaEffect)
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
            c.TempPower = Magnitude;
        }
    }
    //Message that is displayed when the effect affects a character! 
    public override void PrintApplication(Character character)
    {

            Utilities.ConsoleWriteColor(character.Name+"s",character.NameColor);
            Utilities.ConsoleWriteColor(" Attack",ConsoleColor.DarkYellow);
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
        return new AttackBuff(Duration,Magnitude,AreaEffect);
    }
}