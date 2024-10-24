

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Enemy> enemyList,Player player)
    {
        
        DisplayPlayerCombatOptions(enemyList,player);
        

        //Checks if player is alive after the Combat has finnished
        if(player.CurrentHealth > 0 )
        {
            return true; //player killed all monsters
        }
        else
        {
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
        switch(PickAction(combatOptions,enemyList))
        {
            case 0:
                int chosenAbilityIndex = player.PickFromChosenAbilities("What ability do you want to use");
                Ability usedAbility = player.ChosenAbilities[chosenAbilityIndex]; //chosen ability
                List<string> enemyStringList = new();
                foreach(Enemy e in enemyList)
                {
                    enemyStringList.Add(e.Name);
                } 
                int pickedEnemyIndex = Utilities.PickIndexFromList(enemyStringList,"Who do you want to attack?");
                enemyList[pickedEnemyIndex].TakeDamage(player.DealDamage(usedAbility));
                //attack code here choose ability  is probably the right name for it
                break;
            case 1:
                //TODO create items and inventory first
                //Lists usable consumables such as healing potions or such
                break;
            case 2:
                //attempt to flee results in a loss
                break;
        }
    }
    //Displays enemylist in a userfriendly format
    public static void DisplayEnemyList(List<Enemy> enemyList)
    {   
        Console.WriteLine("Name           Health      Race");
        foreach(Enemy e in enemyList)
        {
            Console.Write($"{e.Name,-15}");
            Utilities.ConsoleWriteColor($"[{e.CurrentHealth}/{e.MaxHealth}]",ConsoleColor.DarkRed);
            Console.WriteLine($"     {e.Race.RaceName}");
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