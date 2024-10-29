

public class Human : IRace
{
    #region Human Specific Abilities

    #endregion
    public string RaceName{get;set;}
    public List<Ability> Abilities{get;set;}
    public Human()
    {
        Abilities = new();
        RaceName = "Human";
    }
    public int  AdjustHealth(int baseHealth)
    {
        return baseHealth +2;
    }
    public int AdjustDamage(int baseDamage)
    {
        return baseDamage +2;
    }
    public List<Ability> GetAbilities()
    {
        return Abilities;
    }
    
}