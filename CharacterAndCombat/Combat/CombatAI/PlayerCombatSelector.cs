
public class PlayerCombatSelector : ICombatSelection
{
    public eCombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public List<Character> FriendList { get;set; }
    public List<Character> EnemyList { get; set; }
    public Character Self {get;set;}
    public Random RandomNumber { get; set; }

    public PlayerCombatSelector()
    {
        CurrentCombatState = eCombatState.Offensive;
        AbilityList = new();
        RandomNumber = new Random();
    }
    public Ability SelectAbility()
    {
        //int chosenIndex = Utilities.PickIndexFromList(Utilities.ToStringList(abilityList),"What ability do you want to use?");
        //return abilityList[chosenIndex];
        int markedIndex = 0;
        bool stillChoosing = true;
        Ability returnAbility = null;
        
        
        while(stillChoosing)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("What ability do you want to use?");
            Console.ResetColor();
            for(int i = 0; i < AbilityList.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    if(AbilityList[i].CurrentCooldown < AbilityList[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(AbilityList[i].Name);
                        Console.Write($" ({AbilityList[i].CoolDownTimer - AbilityList[i].CurrentCooldown})");
                    }
                    else
                    {
                    Console.Write(AbilityList[i].Name);
                    }
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

                }
                else
                {
                    if(AbilityList[i].CurrentCooldown < AbilityList[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(AbilityList[i].Name);
                        Console.WriteLine($" ({AbilityList[i].CoolDownTimer - AbilityList[i].CurrentCooldown})");
                    }
                    else
                    {
                        Console.WriteLine(AbilityList[i].Name);
                    }
                    Console.ResetColor();
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
                    if(markedIndex < AbilityList.Count -1 )
                    {
                        markedIndex++;  
                    }
                    break;
                case ConsoleKey.Enter:
                    if(AbilityList[markedIndex].CurrentCooldown == AbilityList[markedIndex].CoolDownTimer)
                    {
                        returnAbility = AbilityList[markedIndex];
                        stillChoosing = false;
                    }
                    break;
            }
            Console.Clear();
        }
        return returnAbility;
    }
    public Character ChooseTarget(Character self,eTargetType targetType,List<Character> potentialTargets)
    {
        int markedIndex = 0;
        bool stillChoosing = true;
        while (stillChoosing)
        {
            Console.Clear();
            Console.WriteLine("Who do you want to target?");
            
            for (int i = 0; i < potentialTargets.Count; i++)
            {
                
                Character target = potentialTargets[i];
                if (i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*", ConsoleColor.Blue);
                    Utilities.ConsoleWriteColor(target.Name, ConsoleColor.DarkRed);
                    Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",ConsoleColor.Red);
                    CombatHandler.PrintAllEffectIcons(target);
                    Utilities.ConsoleWriteLineColor("*", ConsoleColor.Blue);
                }
                else
                {
                    Utilities.ConsoleWriteColor(target.Name, ConsoleColor.DarkRed);
                    Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",ConsoleColor.Red);
                    CombatHandler.PrintAllEffectIcons(target);
                    Console.WriteLine();
                }
            }

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (markedIndex > 0) markedIndex--;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (markedIndex < potentialTargets.Count - 1) markedIndex++;
                    break;
                case ConsoleKey.Enter:
                    if(potentialTargets[markedIndex] != self)
                    {
                        stillChoosing = false;
                    }
                    else
                    {
                        Utilities.ConsoleWriteLineColor("You cant target yourself",ConsoleColor.Red);
                    }
                    break;
            }
        }
        
        return potentialTargets[markedIndex];
    }
    public Character GetSupportiveTarget(List<Character> potentialTargets)
    {
       throw new NotImplementedException();
    }


    public void UpdateCombatState()
    {
        //player doesnt need this. also figure out a better way to not have all these stupid methods that does nothing for the player.
    }

    public Ability ChooseOffensiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Ability ChooseDefensiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public Ability ChooseSupportiveAbility()
    {
        //Dont implement for player
        throw new NotImplementedException();
    }

    public void TransitionToNextState()
    {
        throw new NotImplementedException();
    }
}