public class Healing : CombatEffect
{
    public Healing(int magnitude) : base(1, magnitude, eCombatEffect.Healing) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character character)
    {
        if (character.CurrentHealth < character.MaxHealth) // Only apply if healing is needed
        {
            Console.Write($"{character.Name} heals ");
            Utilities.ConsoleWriteColor($"{Magnitude} health", ConsoleColor.Green);
            Console.WriteLine(" from the healing effect.");

            // Apply healing directly to character's health, ensuring it doesn't exceed MaxHealth
            character.CurrentHealth = character.CurrentHealth + Magnitude > character.MaxHealth 
                                    ? character.MaxHealth 
                                    : character.CurrentHealth + Magnitude;

        }
        else
        {
            Console.WriteLine($"{character.Name} is already at maximum health and does not need healing.");
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Healing(Magnitude);
    }

    // No need for ResolveEffect since it's an instant effect
}
