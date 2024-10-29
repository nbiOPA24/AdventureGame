using System.Dynamic;

public class Character
{
    public string Name {get;set;}
    public int CurrentHealth {get;set;}
    public int MaxHealth {get;set;}
    public int BaseDamage {get;set;}
    public int Armor {get;set;}
    public IRace Race {get;set;}
    public int XPos {get;set;}
    public int YPos {get;set;}
    public bool IsImmune {get;set;}
    public Ability[]  ChosenAbilities {get;set;}
    public List<Ability> AllKnownAbilities {get;set;}
    public List<CombatEffect> CurrentStatusEffects {get;set;}
    public Inventory Inventory {get;set;}


    public Character(string name,int startingHealth,IRace race,int baseDamage,int armor)
    {
        Race = race;
        CurrentHealth = race.AdjustHealth(startingHealth);
        BaseDamage = race.AdjustDamage(baseDamage);
        Name = name;
        MaxHealth = CurrentHealth;
        Armor = armor;
        //En lista på spelarens alla lärda abilities
        AllKnownAbilities = new();
        AllKnownAbilities = Race.GetAbilities();
        //En spelares användningsredo abilities. 4 stycken
        ChosenAbilities = new Ability[4];
        SetInitialAbilities();  
        CurrentStatusEffects = new List<CombatEffect>();
        IsImmune = false;
        Inventory = new();

    } 

    public void TakeDamage(int damage)
    {
        if(IsImmune == true)
        {
            Console.WriteLine($"{Name} is Immune to damage this round");
        }
        else if(damage >0 )
        {
            int trueDamage = CalculateDamageTaken(damage);
            int absorbed = damage - trueDamage;
            DisplayDamageTaken(trueDamage,absorbed);
            CurrentHealth -= trueDamage;

            if(CurrentHealth < 0) 
            {
                CurrentHealth = 0;//make sure no negative health
                Utilities.ConsoleWriteLineColor($"{Name} has died",ConsoleColor.DarkRed);
            }
        }
    }
    public virtual void DisplayDamageTaken(int damage,int absorbed)
    {
        
        Utilities.ConsoleWriteColor(Name,ConsoleColor.Cyan);
        Console.Write(" Takes ");
        string stringOfDamage = damage.ToString();
        string stringOfAbsorbed = absorbed.ToString();
        Utilities.ConsoleWriteColor(stringOfDamage,ConsoleColor.Red);
        Console.Write(", ");
        Utilities.ConsoleWriteColor(stringOfAbsorbed,ConsoleColor.DarkYellow);
        Console.WriteLine(" absorbed by armor");
    }
    public int ResolveArmor(int unmitigatedDamage)
    {
        double percentageReduction =  (double)Armor/(Armor+25);
        int trueDamage = (int)percentageReduction*(1-unmitigatedDamage);
        return trueDamage;
    }
    public int CalculateDamageTaken(int damage)
    {
        damage -= Armor;
        return damage < 0 ? 0 : damage; // make sure dmage isnt negative
    }
    public virtual void UseAbilityOn(Character character,Ability a)
    {
        Utilities.ConsoleWriteColor(Name,ConsoleColor.Cyan);
        Utilities.CharByChar($" Uses {a.Name} ",8);
        Console.Write($"on ");
        Utilities.ConsoleWriteLineColor($"{character.Name}",ConsoleColor.DarkGray);
        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(character);
        }
        
    }
    //Fills chosen abilities with abilities from "AllKnownAbilities"
    public void SetInitialAbilities()
    {
        for (int i = 0; i < ChosenAbilities.Length ; i++)
        {
            if(i < AllKnownAbilities.Count)
            {
                ChosenAbilities[i] = AllKnownAbilities[i];
            }
             else
            {
                ChosenAbilities[i] = null; // Ensure any remaining slots are null
            }      
        }
    }
    //Allows the used to pick an ability and replace it with another from AllKnownAbilities
    public void ReplaceChosenAbilities()
    {
        bool keepGoing = true;
        while(keepGoing)
        {
            int chosenIndex = PickFromChosenAbilities("What ability would you like to replace?");
            int newAbilityIndex =PickFromAllKnownAbilities($"What ability would you like to use instead of {ChosenAbilities[chosenIndex].Name} ");
            bool isKnown = false;
            for(int i = 0; i< ChosenAbilities.Length ; i++)
            {
                if(ChosenAbilities[i] == AllKnownAbilities[newAbilityIndex])
                {
                    isKnown = true;
                }   
            }
            if(!isKnown)
            {
                ChosenAbilities[chosenIndex] = AllKnownAbilities[newAbilityIndex];
                keepGoing = false;
            }else
            {
                Console.WriteLine("Already known. select something else");
                Console.ReadKey(true);
            }
        }
    }

    public void DisplayChosenAbilities()
    {
        for(int i = 0 ; i < 4; i++)
        {
                if(ChosenAbilities[i] != null )
                {
                    Console.WriteLine($"[ {ChosenAbilities[i].Name ,-15} ]");
                }
        }
    }   
    public int PickFromChosenAbilities(string message)
    {
        int chosenIndex = Utilities.PickIndexFromList(Utilities.ToStringList(ChosenAbilities),message);
        return chosenIndex;
    }
    public int PickFromAllKnownAbilities(string message)
    {
        return Utilities.PickIndexFromList(Utilities.ToStringList(AllKnownAbilities),message);
    }
    //handles the statuseffects currently affecting the player removes them once they reach 0 rounds remaining
    public void ResolveStatusEffects()
    {
        if(CurrentStatusEffects.Count > 0)
        {
            for(int i = 0; i < CurrentStatusEffects.Count ; i++)
            {
                CurrentStatusEffects[i].ResolveEffect(this);
                if(CurrentStatusEffects[i].Duration == 0)
                {
                    CurrentStatusEffects.RemoveAt(i);
                }
            }
            
            //Console.ReadKey(true);
        }
    }
}