public class Damage : CombatEffect
{
    public Damage(int magnitude,bool areaEffect) : base(1, magnitude, eCombatEffect.Damage,areaEffect) // Duration of 1 for instant application
    {
    }
    //All characters affected by a damagetype combateffect take instant damage once
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
            if (!c.IsImmune) // Check if the character isn't immune
            {
                UpdateMagnitude(caster.Power,caster.TempPower);
                // Character resolves the damage taken
                c.TakeDamage(Magnitude);
            }
            else
            {
                Console.WriteLine($"{c.Name} is immune and takes no damage.");
            }
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Damage(Magnitude,AreaEffect);
    }

}
