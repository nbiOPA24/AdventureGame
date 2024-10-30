public class Player : Character
{
    public Player(string name,int startingHealth,IRace race,int baseDamage,int armor,ICombatHandler icombatHandler) 
    :base(name,startingHealth,race,baseDamage,armor,icombatHandler)
    {
        //EDIT remove next two lines
        Ability AdminPower = new("AdminPower",TargetType.Enemy);
        AdminPower.AddDamageEffect(100000);
        ChosenAbilities.Add(AdminPower);
    }
    //

}