

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Character> playerList,List<Character> enemyList,string message)  //This is the main combat Loop
    {
        Console.Clear();
        Utilities.CharByCharLine(message,5,ConsoleColor.DarkGreen,true);    //Displays entering message and waits for keypress
        Console.ReadKey(true);

        InitialiseFriendsAndFoes(playerList,enemyList);
        while(playerList.Count > 0 )                        //For as long as the playerlist has characters in it
        {
            //Player takes turn
            foreach(Player p in playerList.ToList())
            {   
                Character target = null;
                Ability chosenAbility = null;
                while(chosenAbility == null && target == null)
                {
                    if (enemyList.Count <= 0) return true;   //Returns true if all enemies are dead

                    int choiceIndex = PickAction(p); //Character gets a choice of what to do

                    switch(choiceIndex)
                    {
                        case 0:              
                            if(p.AbleToAct) //if not frozen or hindered
                            {   
                                while(target == null)
                                {
                                    chosenAbility = p.CombatBrain.SelectAbility(p);  //selects the ability to use
                                    if(chosenAbility != null)
                                        target = p.CombatBrain.ChooseTarget(p,chosenAbility);
                                    else break;
                                    if(target != null)
                                        p.CombatBrain.UseAbilityOnTarget(p,chosenAbility,target);
                                }
                                AfterTurn(p);  
                                RemoveDeadCharacters(enemyList,playerList); //Removes all dead characters from respective list
                            }
                            else
                            {
                                Utilities.ConsoleWriteColor(p.Name,p.NameColor);
                                Console.WriteLine($" is hindered and not able to act");
                            } 
                            Console.ReadKey(true);
                            break;
                        case 1:
                            //Skips that characters turn
                           
                            break;
                        case 2:
                            //attempt to flee results in a loss
                            if(TryToEscape()) 
                            {
                                Utilities.CharByCharLine("You manage to run away safely",4);
                                return false;
                            }
                            break;
                    }
                    
                }
            }
            
            //returns index choice if player wants to attack == 0, use item == 1, attempt to flee  == 2
            
            //ALL enemies take their turn
            EnemiesTurn(enemyList);
            RemoveDeadCharacters(enemyList,playerList);
            if(enemyList.Count == 0) return true;

            //Resolve end of round stuff (statuseffectupdates etc.)
            EndOfRound(playerList,enemyList);
            RemoveDeadCharacters(enemyList,playerList);
            if(enemyList.Count == 0) return true;
        }
        return false;
    }
    //basicly a copy of the utilities method for returning index frmo list but also displaying the enemies
    public static int PickAction(Player character)
    {
        List<string> combatOptions = new()
        {
            "Act",
            "Skip Turn",
            "Flee"
        };   
        int markedIndex = 0 ;
        bool stillChoosing = true;
        int returnValue = 0 ;
        while(stillChoosing)
        {
            Console.Clear();
            DisplayCharacterList(character.EnemyList);
            DisplayCharacterList(character.FriendList);
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
    public static bool TryToEscape()
    {
                    Random random  = new Random(); 
                    int canEscape = random.Next(0,2);
                    if(canEscape == 0) return false;
                    else return true;              
    }

    public static void EnemyTurn(NPC self)
    {  
        
        self.ICombatBrain.CombatStateHandler.UpdateCombatState(self);

       
        if(self.AbleToAct) //if not frozen or hindered
        {  
            Ability chosenAbility = self.ICombatBrain.AbilitySelectionStrategy.SelectAbility(self);  //selects the ability to use based on AI CombatStates
            Character target = self.ICombatBrain.TargetSelectionStrategy.GetTarget(self,chosenAbility); //Selects ability based on chosen ability targetType

            UseAbilityOnTarget(self,chosenAbility,target); //Uses ability on target
            DisplayTurnOutcome(chosenAbility,self,target); //Displays outcome of the ability
        }
        else
        {
            Utilities.ConsoleWriteColor(self.Name,self.NameColor);
            Console.WriteLine($" is hindered and not able to act");
        } 
        AfterTurn(self);  
        Console.ReadKey(true); //Maybe REMOVE
    }
    

    public static void EnemiesTurn(List<Character> enemyList)
    {
        foreach(NPC e in enemyList.ToList())
        {
            EnemyTurn(e);
        }
    }
  
        public static void UseAbilityOnTarget(Character self,Ability ability, Character target)
    {
        List<Character> charactersHitByAbility = new List<Character>();
        switch(ability.Target)
        {   
            case eTargetType.Enemy:
            case eTargetType.Friendly :
            case eTargetType.Self:
                charactersHitByAbility.Add(target);
                break;
            case eTargetType.AnyFriend:
            case eTargetType.AnyEnemy:
                charactersHitByAbility = target.FriendList;
                break;

        }
        foreach(Character c in charactersHitByAbility)
        {
            foreach(CombatEffect e in ability.CombatEffects)
            {
                e.ApplyEffect(self,c);
            }
        }

    }
    //Checks and removes dead characters from both player and enemyList
    public static void RemoveDeadCharacters(List<Character> enemyList,List<Character> playerList)
    {
        if(playerList.Count >= 0)
        {
            for(int i = 0 ; i < enemyList.Count ; i++)
            {
                if(enemyList[i].CurrentHealth <= 0)
                {
                    enemyList.Remove(enemyList[i]);
                    i--;
                } 
            }
            for(int i = 0 ; i < playerList.Count ; i++)
            {
                if(playerList[i].CurrentHealth <= 0)
                {
                    playerList.Remove(playerList[i]);
                }
            }
        }    
    }
    //Every character goes through this after their turn
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
            
        }
    }
    //Everycharacter goes through this after all characters have done their turn
    public static void AfterRound(Character character)
    {
        //loops through the afterRound methods of combateffects
        if(character.CurrentStatusEffects.Count > 0)
        {
            for(int i = 0; i < character.CurrentStatusEffects.Count ; i++)
            {
                character.CurrentStatusEffects[i].AfterRound(character);
                if(character.CurrentStatusEffects[i].Duration == 0)
                {
                    character.ClearEffect(character.CurrentStatusEffects[i]);
                }
            }
        }
        //Ability is 1 round closer to ready
        foreach(Ability a in character.Abilities)
        {
            if(a.CurrentCooldown < a.CoolDownTimer)
            {
                a.CurrentCooldown++;    
            }
        }
    }
    public static void EndOfRound(List<Character> playerList,List<Character> enemyList)
    {
        Utilities.ConsoleWriteLineColor("---------End of Round----------",ConsoleColor.DarkYellow);
        for(int i = 0 ; i < enemyList.Count ; i++)
        {
            AfterRound(enemyList[i]);
        }
        foreach(Character c in playerList)
        {
            AfterRound(c);
        }
        
        Console.ReadKey(true);
    }
    //Sets up friendlist and enemylist for both teams //Enemies also get agrolists initialized
    public static void InitialiseFriendsAndFoes(List<Character> playerList,List<Character> enemyList)
    {
        
        foreach(Character p in playerList)
        {
            Console.WriteLine(p.Name);
            p.FriendList = playerList;
            p.EnemyList = enemyList;

        }
        foreach(Character n in enemyList)
        {
            Console.WriteLine(n.Name);
            n.FriendList = enemyList;
            n.EnemyList = playerList;

            foreach(Character d in playerList)
            {
                Console.WriteLine(d.Name);
                n.AggroDictionary.Add(d,0);
            }
        }
    }

    
    #region Displaying things
    //Displays a list of characters in combat format with a gray box outside aswell as their health
    public static void DisplayCharacterList(List<Character> listToDisplay)
    {   
        Utilities.ConsoleWriteLineColor("┌──────────────────────────────────┐",ConsoleColor.DarkGray);
        foreach(Character c in listToDisplay)
        {
            Utilities.ConsoleWriteColor("│",ConsoleColor.DarkGray);
            Utilities.ConsoleWriteColor($"{c.Name,-25}",c.NameColor);
            Utilities.ConsoleWriteColor($"[{c.CurrentHealth,-3}/{c.MaxHealth,3}]",ConsoleColor.Red);
            Utilities.ConsoleWriteColor("│",ConsoleColor.DarkGray);
            PrintAllEffectIcons(c);
            Console.WriteLine();
        }
        Utilities.ConsoleWriteLineColor("└──────────────────────────────────┘",ConsoleColor.DarkGray);
    }
    //After a character uses an ability this is displayed
    public static void DisplayTurnOutcome(Ability ability,Character self,Character target)
    {
        Utilities.ConsoleWriteColor("--------------",ConsoleColor.DarkYellow);
        Utilities.ConsoleWriteColor(self.Name,self.NameColor);
        Utilities.ConsoleWriteLineColor("--------------",ConsoleColor.DarkYellow);
        switch(ability.Target)
        {
            case eTargetType.Enemy:
            case eTargetType.Friendly :
            case eTargetType.Self:
                Utilities.ConsoleWriteColor(self.Name,self.NameColor);
                Utilities.CharByChar($" Uses {ability.Name} ",8);
                Console.Write($"on ");
                Utilities.ConsoleWriteLineColor($"{target.Name}",target.NameColor);
                break;
            case eTargetType.AnyFriend:
            case eTargetType.AnyEnemy:
                Utilities.ConsoleWriteColor(self.Name,self.NameColor);
                Utilities.CharByCharLine($" Uses {ability.Name} ",8);

                break;
        }

    }
    //Displays all effecticons of the effects affecting 1 character
    public static void PrintAllEffectIcons(Character character)
    {
        foreach(CombatEffect effect in character.CurrentStatusEffects)
        {
            effect.PrintEffectIcon();
        }
    }
    #endregion
}