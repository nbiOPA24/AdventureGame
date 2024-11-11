

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Character> enemyList,List<Character> playerList,string message) 
    {
        Console.Clear();
        Utilities.CharByCharLine(message,5,ConsoleColor.DarkGreen,true);    //Displays entering message and waits for keypress
        Console.ReadKey(true);
        
        CombatSession currentSession = new(playerList,enemyList); //Initializing CombatSession object carrying playerlist enemylist and such
        InitialiseCombatSelectors(playerList,enemyList);
        //Round start
        while(playerList.Count > 0 )
        {
            
            
            //returns index choice if player wants to attack == 0, use item == 1, attempt to flee  == 2
            int choiceIndex = PickAction(currentSession);
            switch(choiceIndex)
            {
                case 0:          
                    //Each Player takes a turn in order
                    foreach(Character c in playerList)
                    {
                        CharacterTurn(c,currentSession.EnemyList,currentSession.PlayerList); 
                        Console.ReadKey(true);
                        RemoveDeadCharacters(enemyList,playerList); //Removes all dead characters from respective list
                        if (enemyList.Count <= 0)return true;   
                    }
                    break;
                case 1:
                    //TODO create items and inventory first
                    //Lists usable consumables such as healing potions or such
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
            //ALL enemies take their turn
            EnemiesTurn(currentSession);
            RemoveDeadCharacters(enemyList,playerList);
            if(enemyList.Count == 0) return true;

            //Resolve end of round stuff (statuseffectupdates etc.)
            EndOfRound(currentSession);
            RemoveDeadCharacters(enemyList,playerList);
            if(enemyList.Count == 0) return true;
        }
        return false;
    }
    //Displays enemylist in a userfriendly format



    //basicly a copy of the utilities method for returning index frmo list but also displaying the enemies
    public static int PickAction(CombatSession session)
    {
        List<string> combatOptions = new()
        {
            "Act",
            "Use item",
            "Flee"
        };   
        int markedIndex = 0 ;
        bool stillChoosing = true;
        int returnValue = 0 ;
        while(stillChoosing)
        {
            Console.Clear();
            DisplayCharacterList(session.EnemyList);
            DisplayCharacterList(session.PlayerList);
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

    public static void CharacterTurn(Character self,List<Character> enemyList,List<Character> friendList)
    {  

        self.ICombatSelector.UpdateCombatState();

       
        if(self.AbleToAct) //if not frozen or otherwise hindered
        {  
            Ability chosenAbility = self.ICombatSelector.SelectAbility(self,enemyList,friendList);  //selects the ability to use

            if(chosenAbility == null) return;
            Utilities.ConsoleWriteColor("--------------",ConsoleColor.DarkYellow);
            Utilities.ConsoleWriteColor(self.Name,self.NameColor);
            Utilities.ConsoleWriteLineColor("--------------",ConsoleColor.DarkYellow);
            ExecuteAbilityOnTarget(self,chosenAbility,enemyList,friendList); //handles targeting. who will it affect
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

            CharacterTurn(e,session.PlayerList,session.EnemyList);
        }
    }
  
    public static void ExecuteAbilityOnTarget(Character self,Ability a,List<Character> EnemyTargetList,List<Character> FriendlyTargetList)
    {
        switch(a.Target)
        {
            case eTargetType.Self: //if a selfcast spell
                UseAbilityOn(self,a,self.NameColor,FriendlyTargetList);
                break;
            case eTargetType.Friendly: //if a friendly target spell
                bool foundOther = false;
                foreach(Character c in FriendlyTargetList)
                {
                    if(c != self)
                    {
                        foundOther = true;
                    }
                }
                if(foundOther)
                {
                    Character chosenFriend = self.ICombatSelector.ChooseTarget(a,self,eTargetType.Friendly,FriendlyTargetList,FriendlyTargetList,EnemyTargetList);
                    UseAbilityOn(self,chosenFriend,a,self.NameColor,self.NameColor,FriendlyTargetList); //Casts Ability on friend
                    Console.ReadKey(true);
                }
                else
                Console.WriteLine("There is no suitable target for that ability");
                break;
            case eTargetType.AnyFriend:
                    Character chosenAnyFriend = self.ICombatSelector.ChooseTarget(a,self,eTargetType.AnyFriend,FriendlyTargetList,FriendlyTargetList,EnemyTargetList);
                    UseAbilityOn(self,chosenAnyFriend,a,self.NameColor,self.NameColor,FriendlyTargetList); //Casts Ability on friend
                    Console.ReadKey(true);
                break;
            case eTargetType.Enemy: //if an enemy target spell
                Character chosenEnemy = self.ICombatSelector.ChooseTarget(a,self,eTargetType.Enemy,EnemyTargetList,FriendlyTargetList,EnemyTargetList);
                UseAbilityOn(self,chosenEnemy,a,self.NameColor,chosenEnemy.NameColor,EnemyTargetList); //Deals damage to the enemy object
                Console.ReadKey(true);
                break;
            default:
                Console.WriteLine("New TargetType noticed check your code");
                break;
        }
        //starts the cooldown roundtimer for the ability
        a.CurrentCooldown = 0;
    }
    //uses ability on a target
    public static void UseAbilityOn(Character self,Character target ,Ability a,ConsoleColor colorSelf,ConsoleColor colorTarget,List<Character> targetParty)
    {
        //if target != self  displays the target else only writes that its being used
        if(a.Target != eTargetType.Self)
        {
            Utilities.ConsoleWriteColor(self.Name,colorSelf);
            Utilities.CharByChar($" Uses {a.Name} ",8);
            Console.Write($"on ");
            Utilities.ConsoleWriteLineColor($"{target.Name}",colorTarget);
        }

        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(self,target,targetParty);
        }   
    } 
    //Ability used on self
    public static void UseAbilityOn(Character self,Ability a,ConsoleColor color,List<Character> targetParty)
    {
        Utilities.ConsoleWriteColor(self.Name,color);
        Utilities.CharByCharLine($" Uses {a.Name} ",8);
        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(self,self,targetParty);
        } 
    }
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
            Console.WriteLine(enemyList.Count); //REMOVE
            for(int i = 0 ; i < playerList.Count ; i++)
            {
                if(playerList[i].CurrentHealth <= 0)
                {
                    playerList.Remove(playerList[i]);
                }
            }
        }    
        Console.ReadKey();//Remove
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
            //Console.ReadKey(true);
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
    public static void EndOfRound(CombatSession session)
    {
        Utilities.ConsoleWriteLineColor("---------End of Round----------",ConsoleColor.DarkYellow);
        for(int i = 0 ; i < session.EnemyList.Count ; i++)
        {
            AfterRound(session.EnemyList[i]);
        }
        foreach(Character c in session.PlayerList)
        {
            AfterRound(c);
        }
        
        Console.ReadKey(true);
    }
    //The combat selectors are initialized for each character in 1 team
    public static void InitialiseCombatSelectors(List<Character> friendList,List<Character> enemyList)
    {
        //Sets up each characters IcommbatSelectors to know who is their friend and who is their foe and also who "Self" represents
        //Also updates the abilityList of each character in both list
        foreach(Character c in friendList)
        {
            c.ICombatSelector.FriendList = friendList;
            c.ICombatSelector.EnemyList = enemyList;
            c.ICombatSelector.AbilityList = c.Abilities;
            c.ICombatSelector.Self = c;
            
        }
        foreach(Character c in enemyList)
        {
            c.ICombatSelector.FriendList = enemyList;
            c.ICombatSelector.EnemyList = friendList;
            c.ICombatSelector.AbilityList = c.Abilities;
            c.ICombatSelector.Self = c;
            foreach(Character d in friendList)
            {
                c.ICombatSelector.AggroDictionary.Add(d,0);
            }
        }
    }
    
    #region Displaying things
    //Displays a list of characters in combat format with a gray box outside aswell as their health
    public static void DisplayCharacterList(List<Character> listToDisplay)
    {   
        Utilities.ConsoleWriteLineColor(",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,",ConsoleColor.DarkGray);
        foreach(Character c in listToDisplay)
        {
            Utilities.ConsoleWriteColor("|",ConsoleColor.DarkGray);
            Utilities.ConsoleWriteColor($"{c.Name,-25}",c.NameColor);
            Utilities.ConsoleWriteColor($"[{c.CurrentHealth,-3}/{c.MaxHealth,3}]",ConsoleColor.Red);
            Utilities.ConsoleWriteColor("|",ConsoleColor.DarkGray);
            PrintAllEffectIcons(c);
            Console.WriteLine();
        }
        Utilities.ConsoleWriteLineColor("************************************",ConsoleColor.DarkGray);
    }

    public static void PrintAllEffectIcons(Character character)
    {
        foreach(CombatEffect effect in character.CurrentStatusEffects)
        {
            effect.PrintEffectIcon();
        }
    }
    #endregion
}