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
    Burn,

}
public enum eAbilityType
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
public enum eCombatState
{
    Offensive,
    Defensive,
    Supportive,
    Default,
}
public enum eTargetType
{
    Self,
    Friendly,
    Enemy
}
public enum eEnemyType
{
    Supportive,
    Bruiser,
    Tank,
    Caster,
    Rogue,
}
public enum eEnemyFamily
{
    Goblin,
}