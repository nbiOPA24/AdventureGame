public class Poison : CombatEffect
{
    public Poison(int duration,int magnitude) : base(duration,magnitude,eCombatEffect.Poison)
    {

    }

    public override void ApplyEffect(Character self,Character target)
    {
        base.ApplyEffect(self,target);

    }
    public override void PrintApplication(Character character)
    {

            Utilities.ConsoleWriteColor(character.Name,character.NameColor);
            Console.Write($" Has been ");
            Utilities.ConsoleWriteColor("Poisoned",ConsoleColor.DarkGreen);
            Console.WriteLine($" for {Duration} rounds");
    }
        public override void AfterRound(Character character)
    {
        Console.Write($"{character.Name} suffers {Magnitude} damage due to  ");
        Utilities.ConsoleWriteColor("Poison ",ConsoleColor.DarkGreen);
        //player takes poison damage
        character.CurrentHealth = character.CurrentHealth <= 0 ? 0 : character.CurrentHealth-Magnitude;
        
        Console.WriteLine($" {Duration} rounds remaining");
        //reduces duration by 1round
        if(Duration > 0 )
        {
            Duration--;
        }  
    }
    public override CombatEffect CloneEffect()
    {
        return new Poison(Duration,Magnitude);
    }
}