public class HealingOverTime : CombatEffect
{
    public HealingOverTime(int duration, int magnitude,bool areaEffect) : base(duration, magnitude, eCombatEffect.HealingOverTime,areaEffect)
    {
    
    }

    public override void ApplyEffect(Character caster, Character target,List<Character> targetTeam,List<Character> otherTeam)
    {
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = targetTeam;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            UpdateMagnitude(caster.Power,caster.TempPower);
            // Initial application message
            Utilities.ConsoleWriteColor(c.Name,c.NameColor);
            Console.WriteLine(" receives a healing over time effect.");
            c.CurrentStatusEffects.Add(CloneEffect()); // Adds the effect to the target's active status effects
        }
    }

    public override void AfterRound(Character character)
    {
        if (Duration > 0)
        {
            // Heal the character
            character.CurrentHealth = Math.Min(character.MaxHealth, character.CurrentHealth + Magnitude);
            Utilities.ConsoleWriteColor(character.Name, character.NameColor);
            Console.Write($" heals for ");
            Utilities.ConsoleWriteColor($"{Magnitude}",ConsoleColor.DarkGreen);
            Console.WriteLine($" HP due to Healing Over Time. {Duration} rounds remaining.");

            // Reduce duration
            Duration--;
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new HealingOverTime(Duration, Magnitude,AreaEffect); // Clones the effect with the same duration and magnitude
    }

    public override void PrintApplication(Character character)
    {
        Utilities.ConsoleWriteColor(character.Name, character.NameColor);
        Console.Write(" is now benefiting from a ");
        Utilities.ConsoleWriteColor("Healing over time",ConsoleColor.DarkGreen);
        Console.WriteLine(" Effect.");
    }
}


