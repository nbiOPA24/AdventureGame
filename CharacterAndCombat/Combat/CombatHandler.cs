

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Character> enemyList,Player player,string message)
    {
        //Displays entering message and waits for keypress
        Console.Clear();
        Utilities.CharByCharLine(message,5,ConsoleColor.DarkGreen,true);
        Console.ReadKey(true);

        List<Character> playerList = new() // this is stupid and needs to be refactored later
        {
            player
        };
        bool stillInCombat = true;   //Extra bool to check if player has not fled
        //Round start
        while(player.CurrentHealth > 0 && enemyList.Count > 0 && stillInCombat )
        {
            //player takes a Turn 
            //returns index choice if player wants to attack == 0, use item == 1, attempt to flee  == 2
            int choiceIndex = PickAction(enemyList);
            switch(choiceIndex)
            {
                case 0:          
                    CharacterTurn(player.ICombatHandler,player,enemyList,ConsoleColor.Cyan,ConsoleColor.DarkRed);
                    break;
                case 1:
                    //TODO create items and inventory first
                    //Lists usable consumables such as healing potions or such
                    break;
                case 2:
                    //attempt to flee results in a loss
                    stillInCombat = false;
                    break;
            }
            //ALL enemies take their turn
            EnemiesTurn(playerList,enemyList);

            //Resikve end of round stuff (statuseffectupdates etc.)
            foreach(Enemy e in enemyList)
            {
                e.EndOfRound();
            }
            player.EndOfRound();
            Console.ReadKey(true);
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
        
        else
        {
            Console.WriteLine("You have Died! Adjust your strategy and try again");
            return false; // player died
        }   
    }
    //Displays enemylist in a userfriendly format
    public static void DisplayEnemyList(List<Character> enemyList)
    {   
        Console.WriteLine($"{"Name",-15}{"Health",-11}Race");
        foreach(Enemy e in enemyList)
        {
            Console.Write($"{e.Name,-15}");
            Utilities.ConsoleWriteColor($"[{e.CurrentHealth}/{e.MaxHealth}]    ",ConsoleColor.DarkRed);
            Console.WriteLine($"{e.Race.RaceName}");
        }
    }

    //basicly a copy of the utilities method for returning index frmo list but also displaying the enemies
    public static int PickAction(List<Character> targetList)
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
            DisplayEnemyList(targetList);
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

    public static void CharacterTurn(ICombatHandler icombatHandler,Character self,List<Character> targetList,ConsoleColor selfColor,ConsoleColor enemyColor)
    {  
        if(self.AbleToAct) //if not frozen or otherwise hindered
        {
            Ability chosenAbility = icombatHandler.SelectAbility(self.ChosenAbilities);  //selects the ability to use
            ExecuteAbilityOnTarget(icombatHandler,self,chosenAbility,targetList,selfColor,enemyColor); //handles targeting. who will it affect
        }
        else Console.WriteLine($"{self.Name} is hindered and not able to act");
       
    }
    public static void EnemiesTurn(List<Character> targetList,List<Character> enemyList)
    {
        foreach(Enemy e in enemyList)
        {
            CharacterTurn(e.ICombatHandler,e,targetList,ConsoleColor.DarkRed,ConsoleColor.Cyan);
        }
    }
  
    public static void ExecuteAbilityOnTarget(ICombatHandler iCombatHandler,Character self,Ability a,List<Character> characterList,ConsoleColor colorSelf,ConsoleColor colorTarget)
    {
        switch(a.Target)
        {
            case TargetType.Self: //if a selfcast spell
                UseAbilityOn(self,a,colorSelf);
                break;
            case TargetType.Friendly: //if a friendly target spell
                break;
                
            case TargetType.Enemy: //if an enemy target spell
                Character chosenEnemy = iCombatHandler.ChooseEnemyTarget(characterList);
                UseAbilityOn(self,chosenEnemy,a,colorSelf,colorTarget); //Deals damage to the enemy object
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
}