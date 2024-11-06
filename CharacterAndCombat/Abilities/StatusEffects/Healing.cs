public class Healing : CombatEffect
{
    public Healing(int magnitude) : base(1, magnitude, eCombatEffect.Healing) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character self,Character target)
    {
        if (target.CurrentHealth < target.MaxHealth) // Only apply if healing is needed
        {
            Console.Write($"{target.Name} heals ");
            Utilities.ConsoleWriteColor($"{Magnitude} health", ConsoleColor.Green);
            Console.WriteLine(" from the healing effect.");

            // Apply healing directly to character's health, ensuring it doesn't exceed MaxHealth
            target.CurrentHealth = target.CurrentHealth + Magnitude > target.MaxHealth 
                                    ? target.MaxHealth 
                                    : target.CurrentHealth + Magnitude;

        }
        else
        {
            Console.WriteLine($"{target.Name} is already at maximum health and does not need healing.");
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Healing(Magnitude);
    }

    // No need for ResolveEffect since it's an instant effect
}
