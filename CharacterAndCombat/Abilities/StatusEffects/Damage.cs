public class Damage : CombatEffect
{
    public Damage(int magnitude) : base(1, magnitude, eCombatEffect.Damage) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character self,Character target)
    {
        if (!target.IsImmune) // Check if the character isn't immune
        {
            // Character resolves the damage taken
            target.TakeDamage(Magnitude);
        }
        else
        {
            Console.WriteLine($"{target.Name} is immune and takes no damage.");
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Damage(Magnitude);
    }

}
