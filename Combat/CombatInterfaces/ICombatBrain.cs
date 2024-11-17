public interface ICombatBrain //The brain handles NPC behavior
{
    IAbilitySelectionStrategy AbilitySelectionStrategy{get;set;}
    ITargetSelectionStrategy TargetSelectionStrategy {get;set;}
    ICombatStateHandler CombatStateHandler {get;set;}
}