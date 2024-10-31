
public class PlayerCombatHandler : ICombatHandler
{
    public Ability SelectAbility(List<Ability> abilityList)
    {
        int chosenIndex = Utilities.PickIndexFromList(Utilities.ToStringList(abilityList),"What ability do you want to use?");
        return abilityList[chosenIndex];
    }
    public Character ChooseEnemyTarget(List<Character> potentialTargets)
    {
        int index = Utilities.PickIndexFromList(Utilities.ToStringList(potentialTargets),"Who do you want to attack");
        return potentialTargets[index];
    }


}