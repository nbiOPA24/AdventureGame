
public class EnemySupportAI : ICombatHandler
{
    public CombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public EnemySupportAI()
    {
        CurrentCombatState = CombatState.Offensive;
        AbilityList = new();
    }
    public Ability SelectAbility()
    {
        Ability returnAbility = null;
        switch(CurrentCombatState)
        {
            case CombatState.Offensive:
                returnAbility = ChooseOffensiveAbility();
                break;
            case CombatState.Defensive:
                returnAbility = ChooseDefensiveAbility(); 
                break;
            case CombatState.Supportive:
                returnAbility = ChooseSupportiveAbility();   
                break;  
        }
        return returnAbility;
    }
    

    public Ability ChooseDefensiveAbility()
    {
        throw new NotImplementedException();
    }
    public Character ChooseEnemyTarget(List<Character> potentialTargets)
    {   
        return potentialTargets[0];
    }
    public Character ChooseFriendlyTarget(List<Character> potentialTargets)
    {
        throw new NotImplementedException();
    }

    public Ability ChooseOffensiveAbility()
    {
        throw new NotImplementedException();
    }

    public Ability ChooseSupportiveAbility()
    {
        throw new NotImplementedException();
    }
    public void UpdateCombatState()
    {
        throw new NotImplementedException();
    }
}
