public static class NPCFactory
{
    static Random randomNumber = new Random();

    /// <summary>
    /// Intelligence should be a number between 1-100
    /// </summary>
    /// <param name="inputName"></param>
    /// <param name="enemyType"></param>
    /// <param name="selfColor"></param>
    /// <param name="intelligence"></param>
    /// <returns></returns>
    public static Character GenerateNPC(ConsoleColor selfColor,int intelligence,eEnemyFamily family,eEnemyType type)
    {
        //WIP just thrown together for codereview
        Character character = null;
        switch(family)
        {
            case eEnemyFamily.Goblin:
            character = GenerateGoblin(type,selfColor,intelligence);
                break;

        }
        return character;
    }
    public static List<Character> GenerateNPCs(ConsoleColor selfColor,int intelligence,eEnemyFamily family,int amountOfNpcs,eEnemyType type,bool randomType)
    {
        List<Character> returnListOfNPCs = new List<Character>();

        for(int i = 0 ; i < amountOfNpcs ; i++)
        {

            if(randomType)
            {
                switch(randomNumber.Next(0,3))
                {
                    case 0:
                        type = eEnemyType.Supportive;
                        break;
                    case 1:
                        type = eEnemyType.Bruiser;
                        break; 
                    case 2:
                        type = eEnemyType.Caster;
                        break;
                }
                
            }
            returnListOfNPCs.Add(GenerateNPC(selfColor,intelligence,family,type));
            
        }
        return returnListOfNPCs;
    }
    public static Character GenerateGoblin(eEnemyType enemyType ,ConsoleColor selfColor,int intelligence)
    {
        Character character = null;
        switch(enemyType)
        {   //Generates a Goblin Shaman
            case eEnemyType.Supportive:
                
                ICombatSelection ai = new NPCSupportAI();

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
                Ability waterBucket = new Ability("Water Bucket",eTargetType.Friendly,5,eAbilityType.CleanseOther);
                waterBucket.AddCleanseEffect(eCombatEffect.Burn,false);

                //Creates the NPC
                character = new("Goblin Shaman",50,5,10,ai,intelligence,selfColor);
                //Adds each ability to the NPC
                character.Abilities.Add(naturesArmor);   //Defensive self increases Armor
                character.Abilities.Add(healOther);      //HealOther heals target 
                character.Abilities.Add(healSelf);       //HealSelf Heals self
                character.Abilities.Add(stickSlam);      //Offensive No cooldown basic attack
                character.Abilities.Add(poisonDart);     //Offensive lower damage but adds poison
                character.Abilities.Add(PurifyPoison);   //CleanseOther removes poison
                character.Abilities.Add(waterBucket);    //CleanseOther removes burn effect
                break;
            case eEnemyType.Caster:
                ai = new NPCOffensiveAI();
                
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
                character = new Character("Goblin Pyromancer", 45, 4, 12, ai, intelligence,selfColor);
                
                // Adds each ability to the NPC
                character.Abilities.Add(fireShield);       // Defensive self ability that increases Armor
                character.Abilities.Add(rejuvenate);       // HealSelf provides minor self-healing
                character.Abilities.Add(fireball);         // Offensive fire-based attack with no cooldown
                character.Abilities.Add(flameBurst);       // Offensive ability that inflicts burn DoT
                character.Abilities.Add(flamePurge);       // CleanseOther removes burn effect on allies
                break;
            case eEnemyType.Bruiser:
                ai = new NPCOffensiveAI();
                
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
                character = new Character("Goblin Berserker", 60, 8, 10, ai,  intelligence,selfColor); // Adjusted stats for a tankier DPS

                // Adds each ability to the NPC
                character.Abilities.Add(ironSkin);       // Defensive self ability that increases Armor
                character.Abilities.Add(powerStrike);    // High single-target physical damage
                character.Abilities.Add(brutalStrike);         // AoE physical damage for multiple enemies
                character.Abilities.Add(berserkRage);    // High-risk, high-reward attack
                character.Abilities.Add(battleCry);      // Supportive ability that boosts attack power
                    break;  
                case eEnemyType.Minion:
                    
                    ai = new NPCOffensiveAI();
                    switch(randomNumber.Next(0,3))
                    {
                        case 0:
                            //offensive
                            Ability scratch = new("Scratch",eTargetType.Enemy,0,eAbilityType.Offensive);
                            scratch.AddDamageEffect(5,false);
                            character = new Character("Goblin Grunt",25,5,1,ai,0,selfColor); // Adjusted stats for a tankier DPS
                            character.Abilities.Add(scratch);
                            break;
                        case 1:
                            Ability weakShot = new("Weak Shot",eTargetType.Enemy,0,eAbilityType.Offensive);
                            weakShot.AddDamageEffect(5,false);
                            character = new Character("Goblin Archer",20,5,1,ai,0,selfColor); // Adjusted stats for a tankier DPS
                            character.Abilities.Add(weakShot);
                            break;
                        case 2:
                            
                            Ability magicBolt = new("Magic Bolt",eTargetType.Enemy,0,eAbilityType.Offensive);
                            magicBolt.AddDamageEffect(5,false);
                            character = new Character("Goblin Archer",15,7,1,ai,0,selfColor); // Adjusted stats for a tankier DPS
                            character.Abilities.Add(magicBolt);
                            break;
                    }
                    break;
                case eEnemyType.Boss:
                    ai = new GoblinNecromancerAI();
                    character = new Character("Goblin Necromancer",200,35,0,ai,0,selfColor);
                    character.ICombatSelector.Self = character;
                    character.Abilities = ai.AbilityList;
                    
                    character.TempArmor = 999;

                    return character;
        }
        return character;
    }
}