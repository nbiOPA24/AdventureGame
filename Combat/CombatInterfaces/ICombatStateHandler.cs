public interface ICombatStateHandler // Required for brains to update CombatStates properly
{
    Random RandomNumber {get;set;}
    public eCombatState CurrentCombatState {get;set;}
    void UpdateCombatState(Character self);
    void TransitionToNextState(Character self);
}