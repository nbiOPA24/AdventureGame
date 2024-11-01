
public class GoblinShamanAI : ICombatHandler
{
    CombatState CurrentCombatState {get;set;}
    public GoblinShamanAI()
    {
        CurrentCombatState = CombatState.Offensive;
    }
    public Ability ChooseDefensiveAbility()
    {
        throw new NotImplementedException();
    }

    public Character ChooseEnemyTarget(List<Character> potentialTargets)
    {
        throw new NotImplementedException();
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

    public Ability SelectAbility(List<Ability> abilityList)
    {
        throw new NotImplementedException();
    }

    public void UpdateCombatState()
    {
        throw new NotImplementedException();
    }
}
