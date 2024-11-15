public class Healing : CombatEffect
{
    public Healing(int magnitude,bool areaEffect) : base(1, magnitude, eCombatEffect.Healing,areaEffect) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character caster,Character target)
    {
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = target.FriendList;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            if (c.CurrentHealth < c.MaxHealth) // Only apply if healing is needed
            {
                UpdateMagnitude(caster.Power,caster.TempPower);
                Console.Write($"{c.Name} heals ");
                Utilities.ConsoleWriteColor($"{Magnitude} health", ConsoleColor.Green);
                Console.WriteLine(" from the healing effect.");

                // Apply healing directly to character's health, ensuring it doesn't exceed MaxHealth
                c.CurrentHealth = c.CurrentHealth + Magnitude > c.MaxHealth 
                                        ? c.MaxHealth 
                                        : c.CurrentHealth + Magnitude;

            }
            else
            {
                Console.WriteLine($"{c.Name} is already at maximum health and does not need healing.");
            }
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Healing(Magnitude,AreaEffect);
    }

    // No need for ResolveEffect since it's an instant effect
}
