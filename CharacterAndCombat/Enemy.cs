public class Enemy : Character
{
    public Enemy(string name,int startingHealth,IRace race,int baseDamage,int armor) :base(name,startingHealth,race,baseDamage,armor)
    {

    }
    public int DealDamage()
    {
        Ability a = DecideAbility();
        Console.WriteLine($"{Name} Uses {a.Name} ");
        return base.DealDamage(a);
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