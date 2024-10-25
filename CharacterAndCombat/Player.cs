public class Player : Character
{
    public Player(string name,int startingHealth,IRace race,int baseDamage,int armor) :base(name,startingHealth,race,baseDamage,armor)
    {
        //EDIT remove next two lines
        Ability AdminPower = new("AdminPower",9001);
        ChosenAbilities[2] = AdminPower;
    }
}