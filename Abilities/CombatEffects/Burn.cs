public class Burn : CombatEffect
{
    public Burn(int duration, int magnitude,bool areaEffect) : base(duration, magnitude, eCombatEffect.Burn,areaEffect)
    {
    }

    //Decided to leave this in case i want to alter it later
    public override void ApplyEffect(Character caster,Character target)
    {
        base.ApplyEffect(caster,target); // Reuse the base logic for applying effects
    }

    //Message that is displayed when the effect affects a character! 
    public override void PrintApplication(Character character)
    {
        Utilities.ConsoleWriteColor(character.Name, character.NameColor);
        Console.Write(" is now ");
        Utilities.ConsoleWriteColor("Burning", ConsoleColor.Red);
        Console.WriteLine($" for {Duration} rounds");
    }

    //Message displayed aftter each round 
    public override void AfterRound(Character character)
    {
        Console.Write($"{character.Name} takes {Magnitude} damage due to ");
        Utilities.ConsoleWriteLineColor("Burn", ConsoleColor.Red);
        character.TakeTrueDamage(Magnitude);

        // Decrease the duration after each round
        if (Duration > 0)
        {
            Duration--;
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Burn(Duration, Magnitude,AreaEffect);
    }
}
