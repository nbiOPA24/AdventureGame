
public class MonsterCombatAI : ICombatHandler
{
    public Ability SelectAbility(List<Ability> abilityList)
    {
        bool noSpellOffCooldown = true;
        Ability returnAbility;
        foreach(Ability a in abilityList)
        {
            if(a.CurrentCooldown == a.CoolDownTimer)
            {
                noSpellOffCooldown = false;
            }
        }
        if(noSpellOffCooldown)
        {
            Ability defaultAbility = new("HasNoReadyAbility",TargetType.Self,0);
            defaultAbility.AddDamageEffect(5);
            returnAbility = defaultAbility;
        }
        else
        {
            Random random = new Random();
            int index;
            do 
            {
                index = random.Next(0,abilityList.Count);
            }
            while(abilityList[index] == null && abilityList[index].CurrentCooldown < abilityList[index].CoolDownTimer);
            returnAbility = abilityList[index];
        }

        return returnAbility;
    }
    
    public Character ChooseEnemyTarget(List<Character> potentialTargets)
    {   
        return potentialTargets[0];
    }
}