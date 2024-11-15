public interface ICombatStateHandler
{
    Random RandomNumber {get;set;}
    public eCombatState CurrentCombatState {get;set;}
    void UpdateCombatState(Character self);
    void TransitionToNextState(Character self);
}