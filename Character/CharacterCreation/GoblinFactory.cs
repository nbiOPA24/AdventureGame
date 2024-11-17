public static class GoblinFactory 
{
    static List<Func<ConsoleColor,int,NPC>> SupportiveNPCs = new List<Func<ConsoleColor, int, NPC>>();
    static List<Func<ConsoleColor,int,NPC>> OffensiveNPCs = new List<Func<ConsoleColor, int, NPC>>();
    static List<Func<ConsoleColor,int,NPC>> BossNPCs = new List<Func<ConsoleColor, int, NPC>>();
    static Random random = new Random();
    static GoblinFactory()
    {
        OffensiveNPCs.Add(GenerateGoblinPyromancer);
        OffensiveNPCs.Add(GenerateGoblinBerserker);
        SupportiveNPCs.Add(GenerateGoblinShaman);
        BossNPCs.Add(GenerateGoblinNecromancer);

    }
    //Returns a goblin from specified type
    public static NPC GenerateGoblin(ConsoleColor selfColor,int intelligence,eEnemyType type)
    {
        NPC character = null;
        switch(type)
        {   //Generates a Goblin Shaman
            case eEnemyType.Supportive:
                
                return SupportiveNPCs[random.Next(0,SupportiveNPCs.Count)](selfColor,intelligence);
            case eEnemyType.Offensive:
                return OffensiveNPCs[random.Next(0,SupportiveNPCs.Count)](selfColor,intelligence);
            case eEnemyType.Minion:   
                return GenerateGoblinMinion(selfColor,intelligence);
            case eEnemyType.Boss:
            return BossNPCs[random.Next(0,SupportiveNPCs.Count)](selfColor,intelligence);           
        }
        return character;
}


    #region Supportive Goblins
    private static NPC GenerateGoblinShaman(ConsoleColor selfColor,int intelligence)
    {
                NPC newGoblin;
                ICombatBrain brain = new NPCSupportiveAI();

                //Increases Armor
                Ability naturesArmor= new Ability("Nature's Armor",eTargetType.Self,0,eAbilityType.DefensiveSelf);
                naturesArmor.AddArmorBuffEffect(10,3,false);
                //Heals self
                Ability healSelf = new Ability("Heal Self",eTargetType.Self,3,eAbilityType.HealingSelf);
                healSelf.AddHealingEffect(10,false);
                //Heals partymember
                Ability healOther = new Ability("Heal Other",eTargetType.Friendly,3,eAbilityType.HealingOther);
                healOther.AddHealingEffect(15,false);
                //Basic attack
                Ability stickSlam = new Ability("Stick Slam",eTargetType.Enemy,0,eAbilityType.Offensive);
                stickSlam.AddDamageEffect(15,false);
                //Regular damage + poison application
                Ability poisonDart = new Ability("Poison Dart",eTargetType.Enemy,3,eAbilityType.Offensive);
                poisonDart.AddDamageEffect(10,false);
                poisonDart.AddPoisonEffect(5,5,false);
                //Removes Poison from friendly target
                Ability PurifyPoison = new Ability("Purify Poison",eTargetType.Friendly,5,eAbilityType.CleanseOther);
                PurifyPoison.AddCleanseEffect(eCombatEffect.Poison,false);
                //Removes Burn from friendly target


                //Creates the NPC
                newGoblin = new("Goblin Shaman",50,5,10,brain,intelligence,selfColor);
                //Adds each ability to the NPC
                newGoblin.Abilities.Add(naturesArmor);   //Defensive self increases Armor
                newGoblin.Abilities.Add(healOther);      //HealOther heals target 
                newGoblin.Abilities.Add(healSelf);       //HealSelf Heals self
                newGoblin.Abilities.Add(stickSlam);      //Offensive No cooldown basic attack
                newGoblin.Abilities.Add(poisonDart);     //Offensive lower damage but adds poison
                newGoblin.Abilities.Add(PurifyPoison);   //CleanseOther removes poison


                return newGoblin;
    }
    #endregion
    #region Offensive Goblins
    private static NPC GenerateGoblinPyromancer(ConsoleColor selfColor,int intelligence)
    {
                NPC newGoblin;
                ICombatBrain brain = new NPCOffensiveAI();
                
                // Defensive Ability: Fire Shield increases armor temporarily
                Ability fireShield = new Ability("Fire Shield", eTargetType.Self, 3, eAbilityType.DefensiveSelf);
                fireShield.AddArmorBuffEffect(8, 3,false);

                // Healing Ability: Minor self-healing ability
                Ability rejuvenate = new Ability("Rejuvenate", eTargetType.Self, 4, eAbilityType.HealingSelf);
                rejuvenate.AddHealingEffect(8,false);

                // Offensive Ability: Fireball with strong fire damage
                Ability fireball = new Ability("Fireball", eTargetType.Enemy, 0, eAbilityType.Offensive);
                fireball.AddDamageEffect(20,false);

                // Offensive Ability: Flame Burst adds fire-based DoT effect (burn)
                Ability flameBurst = new Ability("Flame Burst", eTargetType.Enemy, 3, eAbilityType.Offensive);
                flameBurst.AddDamageEffect(12,false);
                flameBurst.AddBurnEffect(4, 4,false); // Deals burn damage for 4 rounds
                //OffensiveStrong Ability 
                Ability pyroBlast = new Ability("PyroBlast",eTargetType.Enemy,5,eAbilityType.OffensiveStrong);

                // Cleansing Ability: Flame Purge cleanses burn on allies
                Ability flamePurge = new Ability("Flame Purge", eTargetType.Friendly, 5, eAbilityType.CleanseOther);
                flamePurge.AddCleanseEffect(eCombatEffect.Burn,false);

                // Creates the NPC
                newGoblin = new NPC("Goblin Pyromancer", 45, 4, 12, brain, intelligence,selfColor);
                
                // Adds each ability to the NPC
                newGoblin.Abilities.Add(fireShield);       // Defensive self ability that increases Armor
                newGoblin.Abilities.Add(rejuvenate);       // HealSelf provides minor self-healing
                newGoblin.Abilities.Add(fireball);         // Offensive fire-based attack with no cooldown
                newGoblin.Abilities.Add(flameBurst);       // Offensive ability that inflicts burn DoT
                newGoblin.Abilities.Add(flamePurge);       // CleanseOther removes burn effect on allies

                return newGoblin;
    }
    private static NPC GenerateGoblinBerserker(ConsoleColor selfColor,int intelligence)
    {
        NPC newGoblin;
        ICombatBrain brain = new NPCOffensiveAI();
                
        // Defensive Ability: Iron Skin increases armor temporarily
        Ability ironSkin = new Ability("Iron Skin", eTargetType.Self, 4, eAbilityType.DefensiveSelf);
        ironSkin.AddArmorBuffEffect(10, 3,false); // Provides a strong temporary armor boost for 3 rounds

        // Offensive Ability: Power Strike with high physical damage
        Ability powerStrike = new Ability("Strike", eTargetType.Enemy, 0, eAbilityType.Offensive);
        powerStrike.AddDamageEffect(18,false); // High-damage single-target attack

        // Offensive Ability: Cleave hits multiple enemies with moderate damage
        Ability brutalStrike = new Ability("Brutal Strike", eTargetType.Enemy, 3, eAbilityType.OffensiveStrong);
        brutalStrike.AddDamageEffect(25,false); // Moderate AoE damage for melee attacks

        // OffensiveStrong Ability: Berserk Rage with high damage but also self-damage
        Ability berserkRage = new Ability("Vampiric Strike", eTargetType.Enemy, 5, eAbilityType.OffensiveStrong);
        berserkRage.AddDamageEffect(12,false); // Very high single-target damage
        

        // Supportive Ability: Battle Cry boosts attack damage temporarily
        Ability battleCry = new Ability("Battle Cry", eTargetType.Friendly, 6, eAbilityType.DefensiveOther);
        battleCry.AddArmorBuffEffect(8, 3,false); // Increases damage for 3 rounds

        // Creates the NPC
        newGoblin = new NPC("Goblin Berserker", 60, 8, 10, brain,  intelligence,selfColor); // Adjusted stats for a tankier DPS

        // Adds each ability to the NPC
        newGoblin.Abilities.Add(ironSkin);       // Defensive self ability that increases Armor
        newGoblin.Abilities.Add(powerStrike);    // High single-target physical damage
        newGoblin.Abilities.Add(brutalStrike);         // AoE physical damage for multiple enemies
        newGoblin.Abilities.Add(berserkRage);    // High-risk, high-reward attack
        newGoblin.Abilities.Add(battleCry);      // Supportive ability that boosts attack power
        return newGoblin;
    }
    #endregion
    #region Minions
    //Returns a goblin minion from several options
    private static NPC GenerateGoblinMinion(ConsoleColor selfColor,int intelligence)
    {
        ICombatBrain brain = new NPCOffensiveAI();
        NPC newGoblin = new("Check GenerateGoblinMinion inside Goblin factory",1,1,1,brain,1,selfColor);
        switch(random.Next(0,3))
        {
            //Generates a Goblin Grunt
            case 0:
                
                Ability scratch = new("Scratch",eTargetType.Enemy,0,eAbilityType.Offensive);
                scratch.AddDamageEffect(5,false);
                newGoblin = new NPC("Goblin Grunt",25,5,1,brain,intelligence,selfColor); 
                newGoblin.Abilities.Add(scratch);
                break;
            //Generates a Goblin Archer
            case 1:
                Ability weakShot = new("Weak Shot",eTargetType.Enemy,0,eAbilityType.Offensive);
                weakShot.AddDamageEffect(5,false);
                newGoblin = new NPC("Goblin Archer",20,5,1,brain,intelligence,selfColor); 
                newGoblin.Abilities.Add(weakShot);
                break;
            //Generates a Goblin Apprentice
            case 2:
                Ability magicBolt = new("Magic Bolt",eTargetType.Enemy,0,eAbilityType.Offensive);
                magicBolt.AddDamageEffect(5,false);
                newGoblin = new NPC("Goblin Apprentice",15,7,1,brain,intelligence,selfColor); 
                newGoblin.Abilities.Add(magicBolt);
                break;
        }
        return newGoblin;
    }
    #endregion
    #region Bosses
    private static NPC GenerateGoblinNecromancer(ConsoleColor selfColor,int intelligence)
    {
        NPC newGoblin;
        ICombatBrain brain = new GoblinNecromancerBrain();
        newGoblin = new NPC("Goblin Necromancer",200,35,0,brain,0,selfColor);
        //Phase 1 spells
        Ability summonGoblin = new("Summon Goblins",eTargetType.AnyFriend,3,eAbilityType.DefensiveSelf,3); //Summon 3 goblin minions
        summonGoblin.AddSummonEffect(eEnemyFamily.Goblin,eEnemyType.Minion,0,3);

        Ability shadowyWard = new("Shadowy Ward",eTargetType.AnyFriend,2,eAbilityType.DefensiveSelf,2); //Increases the Armor of all friends
        shadowyWard.AddArmorBuffEffect(40,3,true);

        Ability shadowyAmplification = new("Shadowy Amplification",eTargetType.AnyFriend,4,eAbilityType.DefensiveSelf,2);
        shadowyAmplification.AddAttackBuffEffect(25,3,true);
        
        Ability shadowBolt = new("Shadow Bolt",eTargetType.Enemy,0,eAbilityType.Offensive,1); // singletarget no cd ability
        shadowBolt.AddDamageEffect(25,false);

        Ability shadowNova = new("Shadow Nova",eTargetType.Enemy,2,eAbilityType.Offensive,2); // aoe ability
        shadowNova.AddDamageEffect(15,true);
        newGoblin.AbilityListDictionary[0] = new List<Ability>()
        {
            summonGoblin,
            shadowyWard,
            shadowyAmplification,
            shadowBolt,
            shadowNova
        };
        newGoblin.Abilities = newGoblin.AbilityListDictionary[0];
        //Phase 2 spells
        //Phase 3 spells
                    
        newGoblin.TempArmor = 999;

        return newGoblin;
    }
    #endregion
}