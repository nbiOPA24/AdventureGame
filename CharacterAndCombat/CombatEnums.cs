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
    ArmorBuff,

}
public enum AbilityType
{
    DefensiveSelf,
    DefensiveOther,
    HealingSelf,
    HealingOther,
    CleanseSelf,
    CleanseOther,
    Offensive,
    OffensiveStrong
}
public enum CombatState
{
    Offensive,
    Defensive,
    Supportive,
    Default,
}
public enum TargetType
{
    Self,
    Friendly,
    Enemy
}
public enum EnemyType
{
    Supportive,
    Bruiser,
    Tank,
    Caster,
    Rogue,
}