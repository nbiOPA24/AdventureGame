
public class GoblinNecromancerBrain : ICombatBrain
{
    public Character Self {get;set;}
    public IAbilitySelectionStrategy AbilitySelectionStrategy { get; set; }
    public ITargetSelectionStrategy TargetSelectionStrategy { get; set; }
    public ICombatStateHandler CombatStateHandler { get; set; }

    public GoblinNecromancerBrain()
    {
        TargetSelectionStrategy = new GoblinNecromancerTargetingStrategy();
        AbilitySelectionStrategy = new GoblinNecromancerAbilitySelection();
        CombatStateHandler = new GoblinNecromancerStateHandler();

    }
}