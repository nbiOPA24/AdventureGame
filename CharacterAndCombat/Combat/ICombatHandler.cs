public interface ICombatHandler

{
    Ability SelectAbility(List<Ability> abilityList);
    Character ChooseEnemyTarget(List<Character> potentialTargets);
    Character ChooseFriendlyTarget(List<Character> potentialTargets);
    void UpdateCombatState();
    Ability ChooseOffensiveAbility();
    Ability ChooseDefensiveAbility();
    Ability ChooseSupportiveAbility();
    
    
    

}