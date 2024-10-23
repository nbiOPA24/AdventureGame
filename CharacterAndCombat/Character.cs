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
    public Ability[]  ChosenAbilities {get;set;}
    public List<Ability> AllKnownAbilities {get;set;}


    public Character(string name,int startingHealth,IRace race,int baseDamage,int armor)
    {
        Race = race;
        CurrentHealth = race.AdjustHealth(startingHealth);
        BaseDamage = race.AdjustDamage(baseDamage);
        Name = name;
        MaxHealth = CurrentHealth;
        Armor = armor;
        //En lista på spelarens alla lärda abilities
        AllKnownAbilities = Race.GetAbilities();
        //En spelares användningsredo abilities. 4 stycken
        ChosenAbilities = new Ability[4];
        SetInitialAbilities();  
    } 

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage-Armor;
        if(CurrentHealth < 0) CurrentHealth = 0;
    }
    public int DealDamage()
    {
        return BaseDamage;
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
            int chosenIndex = Utilities.PickIndexFromList(Ability.ToStringList(ChosenAbilities),"What ability would you like to replace?");
            int newAbilityIndex =Utilities.PickIndexFromList(Ability.ToStringList(AllKnownAbilities),$"What ability would you like to use instead of {ChosenAbilities[chosenIndex].Name} ");
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
}