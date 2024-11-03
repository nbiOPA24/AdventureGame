public interface ICombatSelection
{
    public CombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public List<Character> FriendList {get;set;}
    public List<Character> EnemyList {get;set;}
    public Character Self{get;set;}
    Ability SelectAbility();
    Character ChooseTarget(Character self,TargetType targetType,List<Character> potentialTargets);
    Character GetSupportiveTarget(List<Character> potentialTargets);
    void UpdateCombatState();
    Ability ChooseOffensiveAbility();
    Ability ChooseDefensiveAbility();
    Ability ChooseSupportiveAbility();
    
    
    

}