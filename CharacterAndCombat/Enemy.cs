public class Enemy : Character
{
    public Enemy(string name,int startingHealth,IRace race,int baseDamage,int armor,ICombatHandler icombatHandler)
    :base(name,startingHealth,race,baseDamage,armor,icombatHandler)
    {

    }
}