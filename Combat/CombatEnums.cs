public enum eCombatEffect  // CombatEffects are effects that an ability can cause to iteself an enemy or all enemies
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
    Threat,
    Summon,
    AttackBuff,

}
public enum eAbilityType // Ability type is a way to catagorise abilities to make a more intelligent AI possible.
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
public enum eCombatState //States of Combat for enemies also integral to making enemies simulate autonomy
{
    Offensive,
    Defensive,
    Supportive,
    Default,
}
public enum eTargetType  //TargetType decides who can be targeted by a certain ability
{
    Self,
    Friendly,
    AnyFriend,
    Enemy,
    None
}
public enum eEnemyType //EnemyType ensures a character gets the proper ICombatSelector // Enemies only
{
    Supportive,
    Bruiser,
    Tank,
    Caster,
    Rogue,
    Minion,
    Boss
}
public enum eEnemyFamily // Decides what subset of monsters that the enemy will be created from
{
    Goblin,
}