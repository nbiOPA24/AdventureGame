public interface ITargetSelectionStrategy
{
    Random RandomNumber {get;set;}
    Character GetTarget(NPC self,Ability ability);
    Character GetEnemyTarget(NPC self);
    Character GetFriendlyTarget(NPC self);
    
}





