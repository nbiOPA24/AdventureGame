public class ArmorBuff : CombatEffect
{
    public ArmorBuff(int duration,int magnitude) : base(duration,magnitude,eCombatEffect.ArmorBuff)
    {
        
    }

    public override void ApplyEffect(Character character)
    {
        base.ApplyEffect(character);
        character.TempArmor = Magnitude;
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
        return new ArmorBuff(Duration,Magnitude);
    }
}