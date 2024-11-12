public class Threat : CombatEffect
{
    public Threat(int magnitude,bool areaEffect) : base(1, magnitude, eCombatEffect.Threat,areaEffect) // Duration of 1 for instant application
    {
    }

    public override void ApplyEffect(Character caster,Character target,List<Character> targetTeam)
    {
        List<Character> affectedCharacters = new();
        if(AreaEffect)
        {
            affectedCharacters = targetTeam;
        }
        else affectedCharacters.Add(target);
        foreach(Character c in affectedCharacters)
        {
            if (!c.IsImmune) // Check if the character isn't immune
            {
                UpdateMagnitude(caster.Power);
                // Character resolves the damage taken
                c.ICombatSelector.AggroDictionary[caster] += Magnitude;
            }
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Threat(Magnitude,AreaEffect);
    }

}