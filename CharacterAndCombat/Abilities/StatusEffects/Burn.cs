public class Burn : CombatEffect
{
    public Burn(int duration, int magnitude) : base(duration, magnitude, eCombatEffect.Burn)
    {
    }

    public override void ApplyEffect(Character character)
    {
        base.ApplyEffect(character); // Reuse the base logic for applying effects
    }

    public override void PrintApplication(Character character)
    {
        Utilities.ConsoleWriteColor(character.Name, character.NameColor);
        Console.Write(" is now ");
        Utilities.ConsoleWriteColor("Burning", ConsoleColor.Red);
        Console.WriteLine($" for {Duration} rounds");
    }

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
        return new Burn(Duration, Magnitude);
    }
}
