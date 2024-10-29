

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
        Utilities.CharByCharLine(message,5,ConsoleColor.DarkGreen,true);
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
                    //Players turn
                    int chosenAbilityIndex = player.PickFromChosenAbilities("What ability do you want to use");
                    Ability chosenAbility = player.ChosenAbilities[chosenAbilityIndex]; //chosen ability
                    switch(chosenAbility.Target)
                    {
                        case TargetType.Self:
                            player.UseAbilityOn(player,chosenAbility);
                            break;
                        case TargetType.Friendly:
                            break;
                            //if its an ability that targets enemies promts player to choose an enemy to attack
                        case TargetType.Enemy:
                            Enemy chosenEnemy = player.ChooseTarget(enemyList,"Who do you want to attack?");
                            player.UseAbilityOn(chosenEnemy,chosenAbility); //Deals damage to the enemy object
                            Console.ReadKey(true);
                            if(chosenEnemy.CurrentHealth <= 0) //removes enemies from list if they die
                            {
                                enemyList.Remove(chosenEnemy);
                            }
                            break;
                        default:
                            Console.WriteLine("New TargetType noticed check your code");
                            break;
                    }

                    //Enemies turn
                    foreach(Enemy e in enemyList)
                    {
                        Ability monsterChosenAbility = e.DecideAbility();
                        e.ResolveStatusEffects();
                        //Console.ReadKey(true);
                        switch(monsterChosenAbility.Target)
                        {
                            case TargetType.Self:
                                e.UseAbilityOn(e,monsterChosenAbility);
                                break;
                            case TargetType.Friendly:
                                break;
                            case TargetType.Enemy:
                                e.UseAbilityOn(player,monsterChosenAbility);
                                break;
                        }
                    }
                    player.ResolveStatusEffects();
                    Console.ReadKey(true);
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
            Console.Clear();
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