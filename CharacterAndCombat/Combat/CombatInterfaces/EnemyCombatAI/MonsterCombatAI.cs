
public class MonsterCombatAI : ICombatHandler
{
    public Ability SelectAbility(List<Ability> abilityList)
    {
        Random random = new Random();
        int index;
        do 
        {
            index = random.Next(0,abilityList.Count);
        }
        while(abilityList[index] == null);

        return abilityList[index];
    }
    
    public Character ChooseEnemyTarget(List<Character> potentialTargets)
    {   
        return potentialTargets[0];
    }


}