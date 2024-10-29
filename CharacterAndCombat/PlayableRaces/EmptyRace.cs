public class EmptyRace : IRace
{

    public string RaceName {get;}
    public  List<Ability> Abilities{get;}
    public EmptyRace()
    {
        Abilities = new List<Ability>();
        RaceName = "Test";
    }
    public int  AdjustHealth(int baseHealth)
    {
        return baseHealth +4;
    }
    public int AdjustDamage(int baseDamage)
    {
        return baseDamage -4;
    }
   

    public  List<Ability> GetAbilities()
    {
       return Abilities;
    }


    
}