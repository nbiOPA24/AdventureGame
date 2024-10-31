

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Character> enemyList,Character player,string message)
    {
        //Displays entering message and waits for keypress
        Console.Clear();
        Utilities.CharByCharLine(message,5,ConsoleColor.DarkGreen,true);
        Console.ReadKey(true);
        //Initializing CombatSession object carrying playerlist enemylist and such
        CombatSession currentSession = new(player,enemyList);

        //Round start
        while(player.CurrentHealth > 0 && enemyList.Count > 0 && currentSession.StillInCombat )
        {
            Console.WriteLine($"currentRound : {currentSession.CurrentRound}");
            Console.ReadKey(true);
            //player takes a Turn 
            //returns index choice if player wants to attack == 0, use item == 1, attempt to flee  == 2
            int choiceIndex = PickAction(enemyList);
            switch(choiceIndex)
            {
                case 0:          
                    //handles the using of an ability and the target of said ability
                    CharacterTurn(player,enemyList,ConsoleColor.DarkRed);
                    RemoveDeadEnemies(enemyList);
                    
                    break;
                case 1:
                    //TODO create items and inventory first
                    //Lists usable consumables such as healing potions or such
                    break;
                case 2:
                    //attempt to flee results in a loss
                    currentSession.StillInCombat = false;
                    break;
            }
            //ALL enemies take their turn
            EnemiesTurn(currentSession);

            //Resolve end of round stuff (statuseffectupdates etc.)
            EndOfRound(currentSession);
        }
        //Checks if player is alive after the Combat has finnished
        if(player.CurrentHealth > 0 )
        {
            if(enemyList.Count <= 0)
            {
                Console.WriteLine("You have slain all enemies. Collect your reward");
                return true; //player killed all monsters
            }
            else
            {
                Console.WriteLine("You have fled! shame on you");
                return false;
            }
        }
        else //current health is <= 0
        {
            Console.WriteLine("You have Died! Adjust your strategy and try again");
            return false; // player died
        }   
    }
    //Displays enemylist in a userfriendly format
    public static void DisplayEnemyList(List<Character> enemyList)
    {   
        Console.WriteLine($"{"Name",-15}{"Health",-15}");
        foreach(Character e in enemyList)
        {
            Console.Write($"{e.Name,-15}");
            Utilities.ConsoleWriteColor($"[{e.CurrentHealth}/{e.MaxHealth}]    ",ConsoleColor.DarkRed);
            PrintAllEffectIcons(e);
            Console.WriteLine();
        }
    }

    //basicly a copy of the utilities method for returning index frmo list but also displaying the enemies
    public static int PickAction(List<Character> enemyList)
    {
        List<string> combatOptions = new()
        {
            "Attack",
            "Use item",
            "Flee"
        };   
        int markedIndex = 0;
        bool stillChoosing = true;
        int returnValue = 0;
        while(stillChoosing)
        {
            Console.Clear();
            DisplayEnemyList(enemyList);
            for(int i = 0; i< combatOptions.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    Console.Write(combatOptions[i]);
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

                }
                else
                {
                    Console.WriteLine(combatOptions[i]);
                }
            }
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch(pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(markedIndex > 0 )
                    {
                        markedIndex--;    
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if(markedIndex < combatOptions.Count -1 )
                    {
                        markedIndex++;  
                    }
                    break;
                case ConsoleKey.Enter:
                    
                    returnValue = markedIndex;
                    stillChoosing = false;
                    break;
            }
            Console.Clear();
        }
        return returnValue;
    }

    public static void CharacterTurn(Character self,List<Character> targetList,ConsoleColor enemyColor)
    {  
        if(self.AbleToAct) //if not frozen or otherwise hindered
        {
            Ability chosenAbility = self.ICombatHandler.SelectAbility(self.ChosenAbilities);  //selects the ability to use
            ExecuteAbilityOnTarget(self,chosenAbility,targetList,enemyColor); //handles targeting. who will it affect
        }
        else
        {
            Utilities.ConsoleWriteColor(self.Name,self.NameColor);
            Console.WriteLine($" is hindered and not able to act");
        } 
        AfterTurn(self); 
    }
    public static void EnemiesTurn(CombatSession session)
    {
        foreach(Character e in session.EnemyList)
        {
            CharacterTurn(e,session.PlayerList,ConsoleColor.Cyan);
        }
    }
  
    public static void ExecuteAbilityOnTarget(Character self,Ability a,List<Character> characterList,ConsoleColor colorTarget)
    {
        switch(a.Target)
        {
            case TargetType.Self: //if a selfcast spell
                UseAbilityOn(self,a,self.NameColor);
                break;
            case TargetType.Friendly: //if a friendly target spell
                break;
                
            case TargetType.Enemy: //if an enemy target spell
                Character chosenEnemy = self.ICombatHandler.ChooseEnemyTarget(characterList);
                UseAbilityOn(self,chosenEnemy,a,self.NameColor,colorTarget); //Deals damage to the enemy object
                Console.ReadKey(true);
                break;
            default:
                Console.WriteLine("New TargetType noticed check your code");
                break;
        }
    }
    //uses ability on a target
    public static void UseAbilityOn(Character self,Character target ,Ability a,ConsoleColor colorSelf,ConsoleColor colorTarget)
    {
        //if target != self  displays the target else only writes that its being used
        if(a.Target != TargetType.Self)
        {
            Utilities.ConsoleWriteColor(self.Name,colorSelf);
            Utilities.CharByChar($" Uses {a.Name} ",8);
            Console.Write($"on ");
            Utilities.ConsoleWriteLineColor($"{target.Name}",colorTarget);
        }

        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(target);
        }   
    } 
    //Ability used on self
    public static void UseAbilityOn(Character self,Ability a,ConsoleColor color)
    {
        Utilities.ConsoleWriteColor(self.Name,color);
        Utilities.CharByCharLine($" Uses {a.Name} ",8);
        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(self);
        } 
    }
    public static void RemoveDeadEnemies(List<Character> enemyList)
    {
        for(int i = 0 ; i < enemyList.Count ; i++)
        {
            if(enemyList[i].CurrentHealth <= 0)
            {
                enemyList.Remove(enemyList[i]);
            }
        }
    }
    public static void AfterTurn(Character character)
    {
        if(character.CurrentStatusEffects.Count > 0)
        {
            for(int i = 0; i < character.CurrentStatusEffects.Count ; i++)
            {
                character.CurrentStatusEffects[i].AfterTurn(character);
                if(character.CurrentStatusEffects[i].Duration == 0)
                {
                    character.ClearEffect(character.CurrentStatusEffects[i]);
                    character.CurrentStatusEffects.RemoveAt(i);
                }
            }
            
            //Console.ReadKey(true);
        }
    }
    public static void AfterRound(Character character)
    {
        if(character.CurrentStatusEffects.Count > 0)
        {
            for(int i = 0; i < character.CurrentStatusEffects.Count ; i++)
            {
                character.CurrentStatusEffects[i].AfterRound(character);
                if(character.CurrentStatusEffects[i].Duration == 0)
                {
                    character.ClearEffect(character.CurrentStatusEffects[i]);
                    character.CurrentStatusEffects.RemoveAt(i);
                }
            }
            
            //Console.ReadKey(true);
        }
    }
    public static void EndOfRound(CombatSession session)
    {
        Utilities.ConsoleWriteLineColor("---------End of Round----------",ConsoleColor.DarkYellow);
        for(int i = 0 ; i < session.EnemyList.Count ; i++)
        {
            AfterRound(session.EnemyList[i]);
        }
        AfterRound(session.Player);
        Console.ReadKey(true);
    }
    public static void PrintAllEffectIcons(Character character)
    {
        foreach(CombatEffect effect in character.CurrentStatusEffects)
        {
            effect.PrintEffectIcon();
        }
    }
}