public interface ITargetSelectionStrategy //Required for brains to select targets properly
{
    Random RandomNumber {get;set;}
    Character GetTarget(NPC self,Ability ability);
    Character GetEnemyTarget(NPC self);
    Character GetFriendlyTarget(NPC self);
    
}





