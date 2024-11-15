public class Player : Character
{
    public PlayerCombatBrain  CombatBrain {get;set;}

    public Player(string name,int startingHealth,int power,int armor) 
    : base (name,startingHealth,power,armor,ConsoleColor.Cyan)
    {
        CombatBrain = new PlayerCombatBrain();
    }
}