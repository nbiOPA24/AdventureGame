public class CombatEffect
{
    
    public int Duration {get;set;}
    public int Magnitude {get;set;}
    public eCombatEffect Type {get;set;}
    public bool FirstRound {get;set;}

    public CombatEffect(int duration, int magnitude, eCombatEffect type)
    {
        Duration = duration;
        Magnitude = magnitude;
        Type = type;
        FirstRound = true;
    }
    //Checks if the effect already is contained in the list of current ailments if not applies it
    public virtual  void ApplyEffect(Character character)
    {
        if(character.IsImmune == true)
        {
        Console.WriteLine($"{character.Name} is Immune and unaffected by {Type}");
        }
        else
        {
            bool alreadyContains = false;
            bool foundStronger = false;
            int foundIndex = 0;
            for(int i = 0; i < character.CurrentStatusEffects.Count; i++)
            {
                if(character.CurrentStatusEffects[i].Type == Type) //checking if the type already is contained in the list.
                {
                    if(character.CurrentStatusEffects[i].Magnitude*character.CurrentStatusEffects[i].Duration >= Magnitude*Duration) //if the full damage over time is higher on already contained effect
                    {
                        alreadyContains = true; // says its already contained and wont be added
                    }
                    else    //if the damage over time effect of the new spell is higher they are swapped out
                    {
                        foundStronger = true;
                        foundIndex = i;
                    }
                }
                
            }
            if(!alreadyContains) //if none of the same kind was found add the new effect
            {
                if(foundStronger) //if effect*duration is stronger in the new effect the old version is overwritten
                {
                    character.CurrentStatusEffects[foundIndex] = CloneEffect();  
                    PrintApplication(character);  

                }
                else
                {
                    character.CurrentStatusEffects.Add(CloneEffect());
                    PrintApplication(character);
                }
            }

        }
    }
    public virtual void StartOfRound()
    {
        Console.WriteLine("this effect has no StartOfRound() Method fix asap");
    }
    public virtual void EndOfRound(Character character)
    {
        if(Duration > 0 )
        {
            Duration--;
        }   
        
    }
    public virtual void AfterRound(Character character)
    {

    }
    public virtual void AfterTurn(Character character)
    {

    }

    public virtual CombatEffect CloneEffect()
    {
        Console.WriteLine("this abilityeffect have no override for CloneEffect fix it");
        return new(Duration,Magnitude,Type);
    }
    public virtual void PrintApplication(Character character)
    {
        Console.WriteLine("this effect has no PrintApplication() method fix asap");
    }
    public virtual void PrintEffectIcon()
    {
        switch(Type)
        {
            case eCombatEffect.Freeze:
            Utilities.ConsoleWriteColor($"({Duration})",ConsoleColor.Blue);
                break;
            case eCombatEffect.Poison:
            Utilities.ConsoleWriteColor($"({Duration})",ConsoleColor.DarkGreen);
                break;
            case eCombatEffect.Immune:
            Utilities.ConsoleWriteColor($"({Duration})",ConsoleColor.DarkGreen);
                break;
            case eCombatEffect.HealingOverTime:
                break;
            case eCombatEffect.Shield:
                break;
        }
        //code for removing the effect
    }


}