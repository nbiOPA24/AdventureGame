public enum eCombatEffect 
{
    Freeze,
    Poison,
    Immune,
    Damage,
    Healing,
    HealingOverTime,
    Shield,
    Cleanse,

}
public enum AbilityType
{
    DefensiveSelf,
    DefensiveOther,
    HealingSelf,
    HealingOther,
    CleanseSelf,
    CleanseOther,
    Offensive
}
public enum CombatState
{
    Offensive,
    Defensive,
    Supportive,
}
public enum TargetType
{
    Self,
    Friendly,
    Enemy
}