
public class SupportiveCombatStateHandler : ICombatStateHandler
{
    public eCombatState CurrentCombatState { get; set; }
    public Random RandomNumber {get;set;}
    public SupportiveCombatStateHandler()
    {
        RandomNumber = new Random();
    }
    public void TransitionToNextState(Character self)
    {
        CurrentCombatState = CurrentCombatState == eCombatState.Offensive ? eCombatState.Defensive    
                           : CurrentCombatState == eCombatState.Defensive ? eCombatState.Supportive
                           : CurrentCombatState == eCombatState.Supportive ? eCombatState.Default  
                           : eCombatState.Offensive;
            
    }
    public void UpdateCombatState(Character self)
    {
        bool foundSupportive = false;
        bool foundDefensive = false;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(self,self.FriendList,self.Abilities);
        List<Ability> healingSpells = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingOther);
        foreach(NPC e in self.FriendList)
        {
            if((e != self && (double)e.CurrentHealth/e.MaxHealth < 0.3  && healingSpells.Count != 0 )|| dispellTarget != null )
            {
                foundSupportive = true;
                break;
            }
        }
        if((double)self.CurrentHealth / self.MaxHealth < 0.5) foundDefensive = true;
        
        CurrentCombatState = foundSupportive ? eCombatState.Supportive:
                             foundDefensive ? eCombatState.Defensive:
                             eCombatState.Offensive;                        
    }
}