public class Enemy : Character
{
    public Enemy(string name,int startingHealth,IRace race,int baseDamage,int armor) :base(name,startingHealth,race,baseDamage,armor)
    {

    }
    public Ability DecideAbility()
    {
        Random random = new Random();
        int index;
        do 
        {
            index = random.Next(0,4);
        }
        while(ChosenAbilities[index] == null);

        return ChosenAbilities[index];
    }
    public override void UseAbilityOn(Character character,Ability a)
    {
        Utilities.ConsoleWriteColor(Name,ConsoleColor.DarkGray);
        Utilities.CharByChar($" Uses {a.Name} ",8);
        Console.Write($"on ");
        Utilities.ConsoleWriteLineColor($"{character.Name}",ConsoleColor.Cyan);
        foreach(CombatEffect s in a.CombatEffects)
        {
            s.ApplyEffect(character);
        }
        
    }
        public override void DisplayDamageTaken(int damage,int absorbed)
    {
        
        Utilities.ConsoleWriteColor(Name,ConsoleColor.DarkGray);
        Console.Write(" Takes ");
        string stringOfDamage = damage.ToString();
        string stringOfAbsorbed = absorbed.ToString();
        Utilities.ConsoleWriteColor(stringOfDamage,ConsoleColor.Red);
        Console.Write(", ");
        Utilities.ConsoleWriteColor(stringOfAbsorbed,ConsoleColor.DarkYellow);
        Console.WriteLine(" absorbed by armor");
    }
}