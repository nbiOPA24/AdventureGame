using System.Dynamic;

public class Character
{
    public string Name {get;set;}
    public int CurrentHealth {get;set;}
    public int MaxHealth {get;set;}
    public int BaseDamage {get;set;}
    public int Armor {get;set;}
    public int TempArmor {get;set;}
    public int Shield{get;set;}
    public IRace Race {get;set;}
    public int XPos {get;set;}
    public int YPos {get;set;}
    public ConsoleColor NameColor {get;set;}
    public bool IsImmune {get;set;}
    public List<Ability>  ChosenAbilities {get;set;}
    public List<Ability> AllKnownAbilities {get;set;}
    public List<CombatEffect> CurrentStatusEffects {get;set;}
    public Inventory Inventory {get;set;}
    public bool AbleToAct {get;set;}
    public ICombatHandler ICombatHandler {get;set;}
    public int StartingHealth { get; }

    public Character(string name,int startingHealth,IRace race,int baseDamage,int armor,ICombatHandler icombatHandler,ConsoleColor nameColor)
    {
        AbleToAct = true;
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
        ChosenAbilities = new();
        SetInitialAbilities();  
        CurrentStatusEffects = new List<CombatEffect>();
        IsImmune = false;
        Inventory = new();
        ICombatHandler = icombatHandler;
        Shield = 0;
        NameColor = nameColor;
        TempArmor = 0;

    }


    #region Taking damage
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
        
        Utilities.ConsoleWriteColor(Name,NameColor);
        Console.Write(" Takes ");
        string stringOfDamage = damage.ToString();
        string stringOfAbsorbed = absorbed.ToString();
        Utilities.ConsoleWriteColor(stringOfDamage,ConsoleColor.Red);
        Console.Write(", ");
        Utilities.ConsoleWriteColor(stringOfAbsorbed,ConsoleColor.DarkYellow);
        Console.WriteLine(" absorbed by armor");
    }
    public int ReduceDamageByArmor(int unmitigatedDamage)
    {
        int totalArmor = Armor+TempArmor;
        double percentageReduction =  (double)totalArmor/(totalArmor+50);
        int trueDamage = (int)(unmitigatedDamage*(1-percentageReduction));
        return trueDamage;
    }
    public int CalculateDamageTaken(int damage)
    {
        damage = ReduceDamageByArmor(damage);
        return damage < 0 ? 0 : damage; // make sure dmage isnt negative
    }
#endregion
#region Statuseffects


    public virtual void ClearEffect(CombatEffect effect)
    {
        switch(effect.Type)
        {
            case eCombatEffect.Freeze:
            AbleToAct = true;
            Utilities.ConsoleWriteColor(Name,NameColor);
            Console.Write($" is no longer ");
            Utilities.ConsoleWriteLineColor("Frozen",ConsoleColor.Blue);
                break;
            case eCombatEffect.Poison:
            Utilities.ConsoleWriteColor(Name,NameColor);
            Console.Write($" is no longer ");
            Utilities.ConsoleWriteLineColor("Poisoned",ConsoleColor.DarkGreen);
                break;
            case eCombatEffect.Immune:
            Utilities.ConsoleWriteColor(Name,NameColor);
            Console.Write($" is no longer ");
            Utilities.ConsoleWriteLineColor("Immune",ConsoleColor.DarkGreen);
            IsImmune = false;
                break;
            case eCombatEffect.HealingOverTime:
                break;
            case eCombatEffect.Shield:
            Shield = 0;
                break;
        }
        //code for removing the effect
    }
    public virtual void ClearAllEffects()
    {
        for(int i = 0; i< CurrentStatusEffects.Count ; i++)
        {
            ClearEffect(CurrentStatusEffects[i]);
            CurrentStatusEffects.Remove(CurrentStatusEffects[i]);
        }
    }

#endregion
#region setups
    //Fills chosen abilities with abilities from "AllKnownAbilities"
    public void SetInitialAbilities()
    {
        for (int i = 0; i < ChosenAbilities.Count ; i++)
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
#endregion
#region Abilityrelated methods
    //Allows the used to pick an ability and replace it with another from AllKnownAbilities
    public void ReplaceChosenAbilities()
    {
        bool keepGoing = true;
        while(keepGoing)
        {
            int chosenIndex = SelectAbilityIndex("What ability do you want to replace?");
            int newAbilityIndex =PickFromAllKnownAbilities($"What ability would you like to use instead of {ChosenAbilities[chosenIndex].Name} ");
            bool isKnown = false;
            for(int i = 0; i< ChosenAbilities.Count ; i++)
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

    public int SelectAbilityIndex(string message)
    {
        return  Utilities.PickIndexFromList(Utilities.ToStringList(ChosenAbilities),message);
    }
    public int PickFromAllKnownAbilities(string message)
    {
        return Utilities.PickIndexFromList(Utilities.ToStringList(AllKnownAbilities),message);
    }
#endregion
    //handles the statuseffects currently affecting the player removes them once they reach 0 rounds remaining



}
