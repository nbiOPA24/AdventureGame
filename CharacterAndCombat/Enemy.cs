public class Enemy : Character
{
    public Enemy(string name,int startingHealth,IRace race,int baseDamage,int armor) :base(name,startingHealth,race,baseDamage,armor)
    {

    }
    public void DealDamage(Player player)
    {
        //decides ability to use
        Ability a = DecideAbility();
        Utilities.ConsoleWriteColor(Name,ConsoleColor.Cyan);
        Utilities.CharByCharLine($" Uses {a.Name} ",8);
        player.TakeDamage(a.BaseDamage);
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
}