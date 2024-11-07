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
    HealingAny,
    CleanseSelf,
    CleanseOther,
    Offensive,
    OffensiveStrong,
    OffensiveAOE
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
    AnyFriend,
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