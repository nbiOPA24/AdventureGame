public class NPC : Character
{
    public ICombatBrain ICombatBrain {get;set;}
    public int Intelligence {get;set;}
    public Dictionary<int, List<Ability>> AbilityListDictionary {get;set;}

    public NPC(string name,int startingHealth,int power,int armor,ICombatBrain brain,int intelligence,ConsoleColor selfColor) 
    : base (name,startingHealth,power,armor,selfColor)
    {
        ICombatBrain = brain;
        Intelligence = intelligence;
        AbilityListDictionary = new();
    }

}

