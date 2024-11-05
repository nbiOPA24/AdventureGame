public static class EnemyFactory
{


    /// <summary>
    /// Intelligence should be a number between 1-100
    /// </summary>
    /// <param name="inputName"></param>
    /// <param name="enemyType"></param>
    /// <param name="selfColor"></param>
    /// <param name="intelligence"></param>
    /// <returns></returns>
    public static Character GenerateEnemy(string inputName,EnemyType enemyType ,ConsoleColor selfColor,int intelligence)
    {
        string name = inputName;
        Character character = null;;
        switch(enemyType)
        {
            case EnemyType.Supportive:
                ICombatSelection ai = new NPCSupportAI();
                Ability guard = new Ability("Guard",TargetType.Self,0,AbilityType.DefensiveSelf);
                guard.AddArmorBuffEffect(5,3);
                Ability healSelf = new Ability("Heal Self",TargetType.Self,2,AbilityType.HealingSelf);
                healSelf.AddHealingEffect(10);
                Ability healOther = new Ability("Heal Other",TargetType.Friendly,2,AbilityType.HealingOther);
                healOther.AddHealingEffect(10);
                Ability stickSlam = new Ability("Stick Slam",TargetType.Enemy,0,AbilityType.Offensive);
                stickSlam.AddDamageEffect(5);
                Ability poisonDart = new Ability("Poison Dart",TargetType.Enemy,3,AbilityType.Offensive);
                poisonDart.AddDamageEffect(10);
                poisonDart.AddPoisonEffect(5,5);
                Ability cleansePoison = new Ability("Cleanse Poison",TargetType.Friendly,8,AbilityType.CleanseOther);
                cleansePoison.AddCleanseEffect(eCombatEffect.Poison);
                character = new(name,50,5,10,ai,selfColor,intelligence);
                character.Abilities.Add(guard);
                character.Abilities.Add(healOther);
                character.Abilities.Add(healSelf);
                character.Abilities.Add(stickSlam);
                character.Abilities.Add(poisonDart);
                character.Abilities.Add(cleansePoison);
                character.ICombatHandler.AbilityList = character.Abilities;
                character.ICombatHandler.Self = character;
                break;
        }
        return character;
    }
}