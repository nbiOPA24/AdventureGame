

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Enemy> enemyList,Player player,string message)
    {
        Console.Clear();
        Utilities.CharByChar(message,5,ConsoleColor.DarkGreen,true);
        DisplayPlayerCombatOptions(enemyList,player);
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
    public static void DisplayPlayerCombatOptions(List<Enemy> enemyList,Player player)
    {
        List<string> combatOptions = new()
        {
            "Attack",
            "Use item",
            "Flee"
        };     
        bool stillInCombat = true;   
        while(player.CurrentHealth > 0 && enemyList.Count > 0 && stillInCombat )
        {
            switch(PickAction(combatOptions,enemyList))
            {
                case 0:
                    int chosenAbilityIndex = player.PickFromChosenAbilities("What ability do you want to use");
                    Ability usedAbility = player.ChosenAbilities[chosenAbilityIndex]; //chosen ability
                    List<string> enemyStringList = Utilities.ToStringList(enemyList);   //Turns enemies into list format        
                    int pickedEnemyIndex = Utilities.PickIndexFromList(enemyStringList,"Who do you want to attack?",ConsoleColor.DarkBlue);
                    Enemy chosenEnemy = enemyList[pickedEnemyIndex];        //this is the chosen enemy for the attack
                    player.DealDamage(chosenEnemy,usedAbility); //Deals damage to the enemy object
                    Console.ReadKey(true);
                    if(chosenEnemy.CurrentHealth <= 0) //removes enemies from list if they die
                    {
                        enemyList.Remove(chosenEnemy);
                    }
                    //Enemies attack player.
                    foreach(Enemy e in enemyList)
                    {
                        e.DealDamage(player);
                        
                        Console.ReadKey(true);
                    }
                    
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
        }
    }
    //Displays enemylist in a userfriendly format
    public static void DisplayEnemyList(List<Enemy> enemyList)
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
    public static int PickAction(List<string> list,List<Enemy> enemyList)
    {
        int markedIndex = 0;
        bool stillChoosing = true;
        int returnValue = 0;
        while(stillChoosing)
        {
            DisplayEnemyList(enemyList);
            for(int i = 0; i< list.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    Console.Write(list[i]);
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

                }
                else
                {
                    Console.WriteLine(list[i]);
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
                    if(markedIndex < list.Count -1 )
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
}