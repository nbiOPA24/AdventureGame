public class Damage : CombatEffect
{
    public Damage(int magnitude) : base(1, magnitude, eCombatEffect.Damage) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character character)
    {
        if (!character.IsImmune) // Check if the character isn't immune
        {
            // Character resolves the damage taken
            character.TakeDamage(Magnitude);
        }
        else
        {
            Console.WriteLine($"{character.Name} is immune and takes no damage.");
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Damage(Magnitude);
    }

}
