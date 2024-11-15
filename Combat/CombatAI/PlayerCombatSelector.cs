
using System.Runtime;

public class PlayerCombatBrain 
{
    public Character Self {get;set;}
    public Random RandomNumber { get; set; }

    public PlayerCombatBrain()
    {
        RandomNumber = new Random();
    }
    public Ability SelectAbility(Character self)
    {
        //int chosenIndex = Utilities.PickIndexFromList(Utilities.ToStringList(abilityList),"What ability do you want to use?");
        //return abilityList[chosenIndex];
        int markedIndex = 0;
        bool stillChoosing = true;
        Ability returnAbility = null;
        
        
        while(stillChoosing)
        {
            Console.Clear();
            CombatHandler.DisplayCharacterList(self.EnemyList);
            CombatHandler.DisplayCharacterList(self.FriendList);
            Utilities.ConsoleWriteLineColor("-------"+self.Name+" Turn-------",ConsoleColor.DarkYellow);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("What ability do you want to use?");
            Console.ResetColor();
            for(int i = 0; i < self.Abilities.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    if(self.Abilities[i].CurrentCooldown < self.Abilities[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[{self.Abilities[i].Name+"]",-20}");
                        Console.Write($" ({self.Abilities[i].CoolDownTimer - self.Abilities[i].CurrentCooldown})");
                        Console.Write(self.Abilities[i].Description);
                    }
                    else
                    {
                    Console.Write($"[{self.Abilities[i].Name+"]",-20}");
                    Console.Write(self.Abilities[i].Description);
                    }
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

                }
                else
                {
                    if(self.Abilities[i].CurrentCooldown < self.Abilities[i].CoolDownTimer)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[{self.Abilities[i].Name+"]",-20}");
                        Console.Write($" ({self.Abilities[i].CoolDownTimer - self.Abilities[i].CurrentCooldown})");
                        Console.WriteLine(self.Abilities[i].Description);
                    }
                    else
                    {
                        Console.Write($"[{self.Abilities[i].Name+"]",-20}");
                        Console.WriteLine(self.Abilities[i].Description);
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
                    if(markedIndex < self.Abilities.Count -1 )
                    {
                        markedIndex++;  
                    }
                    break;
                case ConsoleKey.Enter:
                    if(self.Abilities[markedIndex].CurrentCooldown == self.Abilities[markedIndex].CoolDownTimer)
                    {
                        returnAbility = self.Abilities[markedIndex];
                        stillChoosing = false;
                    }
                    break;
            }
            Console.Clear();
        }
        return returnAbility;
    }

    public Character ChooseTarget(Character self,Ability a)
    {
        List<Character> potentialTargets = new List<Character>();
        switch(a.Target)
        {
            case eTargetType.Self:
            case eTargetType.AnyFriend:
                return self;
            case eTargetType.Friendly:
                potentialTargets = self.FriendList;
                break;
            case eTargetType.Enemy:
                potentialTargets = self.EnemyList;
                break;
            case eTargetType.AnyEnemy:
                return self.EnemyList[0];
        }

        int markedIndex = 0;
        bool stillChoosing = true;
        while (stillChoosing)
        {
            Console.Clear();
            CombatHandler.DisplayCharacterList(self.EnemyList);
            CombatHandler.DisplayCharacterList(self.FriendList);
            Console.WriteLine("Who do you want to target?");
            
            for (int i = 0; i < potentialTargets.Count; i++)
            {
                
                Character target = potentialTargets[i];
                if (i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*", ConsoleColor.Blue);
                    Utilities.ConsoleWriteColor(target.Name, target.NameColor);
                    Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",potentialTargets[i].NameColor);
                    CombatHandler.PrintAllEffectIcons(target);
                    Utilities.ConsoleWriteLineColor("*", ConsoleColor.Blue);
                }
                else
                {
                    Utilities.ConsoleWriteColor(target.Name, target.NameColor);
                    Utilities.ConsoleWriteColor($"   [{target.CurrentHealth}/{target.MaxHealth}]",potentialTargets[i].NameColor);
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
                        stillChoosing = false;
                   
                    break;
            }
        }
        
        return potentialTargets[markedIndex];
    }
    public void UseAbilityOnTarget(Character self,Ability ability, Character target)
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
            Console.Clear();
            CombatHandler.DisplayCharacterList(self.EnemyList);
            CombatHandler.DisplayCharacterList(self.FriendList);
            CombatHandler.DisplayTurnOutcome(ability,self,target);
            foreach(Character c in charactersHitByAbility)
            {
                foreach(CombatEffect e in ability.CombatEffects)
                {
                    e.ApplyEffect(self,c);
                }
            }
        

    }
}