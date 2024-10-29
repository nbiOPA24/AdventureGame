public class Player : Character
{
    public Player(string name,int startingHealth,IRace race,int baseDamage,int armor) :base(name,startingHealth,race,baseDamage,armor)
    {
        //EDIT remove next two lines
        Ability AdminPower = new("AdminPower",TargetType.Enemy);
        AdminPower.AddDamageEffect(100000);
        ChosenAbilities[0] = AdminPower;
    }
    //
    public Enemy ChooseTarget(List<Enemy> enemyList,string message)
    {
        List<string> enemyStringList = Utilities.ToStringList(enemyList);   //Turns enemies into list format        
        int pickedEnemyIndex = Utilities.PickIndexFromList(enemyStringList,message,ConsoleColor.DarkBlue);
        return enemyList[pickedEnemyIndex];  
    }
}