public interface ICombatBrain
{
    IAbilitySelectionStrategy AbilitySelectionStrategy{get;set;}
    ITargetSelectionStrategy TargetSelectionStrategy {get;set;}
    ICombatStateHandler CombatStateHandler {get;set;}
}