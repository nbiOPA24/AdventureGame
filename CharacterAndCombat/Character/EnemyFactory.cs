public static class EnemyFactory
{



    public static Character GenerateEnemy(string inputName,EnemyType enemyType )
    {
        string name = inputName;
        Character character = null;;
        switch(enemyType)
        {
            case EnemyType.Supportive:
                ICombatHandler ai = new EnemySupportAI();
                Ability guard = new Ability("Guard",TargetType.Self,0,AbilityType.DefensiveSelf);
                guard.AddArmorBuffEffect(5,3);
                Ability healSelf = new Ability("Heal Self",TargetType.Self,2,AbilityType.HealingSelf);
                healSelf.AddHealingEffect(10);
                Ability healOther = new Ability("Heal Other",TargetType.Friendly,2,AbilityType.HealingOther);
                healOther.AddHealingEffect(10);
                Ability stickSlam = new Ability("Stick Slam",TargetType.Enemy,0,AbilityType.Offensive);
                stickSlam.AddDamageEffect(5);
                Ability superStickSlam = new Ability("Super Stick Slam",TargetType.Enemy,0,AbilityType.Offensive);
                stickSlam.AddDamageEffect(10);
                character = new(name,25,5,10,ai,ConsoleColor.DarkRed);
                character.Abilities.Add(guard);
                character.Abilities.Add(healOther);
                character.Abilities.Add(healSelf);
                character.Abilities.Add(stickSlam);
                character.Abilities.Add(superStickSlam);
                character.ICombatHandler.AbilityList = character.Abilities;
                break;
        }
        return character;
    }
}