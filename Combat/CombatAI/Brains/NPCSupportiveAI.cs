public class NPCSupportiveAI : ICombatBrain
{
    


    public Character Self {get;set;}
    public IAbilitySelectionStrategy AbilitySelectionStrategy { get; set; }
    public ITargetSelectionStrategy TargetSelectionStrategy { get; set; }
    public ICombatStateHandler CombatStateHandler { get; set; }

    public NPCSupportiveAI()
    {
        TargetSelectionStrategy = new SupportiveTargetingStrategy();
        AbilitySelectionStrategy = new SupportiveAbilitySelection();
        CombatStateHandler = new SupportiveCombatStateHandler();

    }
    



}