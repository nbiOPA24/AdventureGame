public interface ICombatHandler

{
    public CombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    Ability SelectAbility();
    Character ChooseEnemyTarget(List<Character> potentialTargets);
    Character ChooseFriendlyTarget(List<Character> potentialTargets);
    void UpdateCombatState();
    Ability ChooseOffensiveAbility();
    Ability ChooseDefensiveAbility();
    Ability ChooseSupportiveAbility();
    
    
    

}