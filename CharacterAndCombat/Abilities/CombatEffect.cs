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
    public virtual  void ApplyEffect(Character self,Character target)
    {
        if(target.IsImmune == true)
        {
        Console.WriteLine($"{target.Name} is Immune and unaffected by {Type}");
        }
        else
        {
            bool alreadyContains = false;
            bool foundStronger = false;
            int foundIndex = 0;
            for(int i = 0; i < target.CurrentStatusEffects.Count; i++)
            {
                if(target.CurrentStatusEffects[i].Type == Type) //checking if the type already is contained in the list.
                {
                    if(target.CurrentStatusEffects[i].Magnitude*target.CurrentStatusEffects[i].Duration >= Magnitude*Duration) //if the full damage over time is higher on already contained effect
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
                    target.CurrentStatusEffects[foundIndex] = CloneEffect();  
                    PrintApplication(target);  

                }
                else
                {
                    target.CurrentStatusEffects.Add(CloneEffect());
                    PrintApplication(target);
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
            case eCombatEffect.Burn:
                Utilities.ConsoleWriteColor($"({Duration})", ConsoleColor.Red); // Add this line for burn
                break;
            case eCombatEffect.Immune:
                Utilities.ConsoleWriteColor($"({Duration})",ConsoleColor.Magenta);
                break;
            case eCombatEffect.HealingOverTime:
                break;
            case eCombatEffect.Shield:
                break;
        }
        //code for removing the effect
    }
    public void PrintAllEffectIcons(List<CombatEffect> list)
    {
        foreach(CombatEffect ce in list)
        {
            PrintEffectIcon();
        }
    }


}