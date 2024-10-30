public interface ICombatHandler

{
    Ability SelectAbility(List<Ability> abilityList);
    Character ChooseEnemyTarget(List<Character> potentialTargets);

}