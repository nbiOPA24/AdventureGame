public class Threat : CombatEffect
{
    public Threat(int magnitude,bool areaEffect) : base(1, magnitude, eCombatEffect.Threat,areaEffect) // Duration of 1 for instant application
    {
    }
    //Adds a value to the AggroDictionary of the target ensuring  NPCs attack the proper target
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
                c.AggroDictionary[caster] += Magnitude;
            }
        }
    }

    public override CombatEffect CloneEffect()
    {
        return new Threat(Magnitude,AreaEffect);
    }

}