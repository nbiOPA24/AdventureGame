
public class NPCOffensiveAI : ICombatBrain
{
    


    public Character Self {get;set;}
    public IAbilitySelectionStrategy AbilitySelectionStrategy { get; set; }
    public ITargetSelectionStrategy TargetSelectionStrategy { get; set; }
    public ICombatStateHandler CombatStateHandler { get; set; }

    public NPCOffensiveAI()
    {
        TargetSelectionStrategy = new OffensiveTargetingStrategy();
        AbilitySelectionStrategy = new OffensiveAbilitySelection();
        CombatStateHandler = new OffensiveCombatStateHandler();

    }
    



}