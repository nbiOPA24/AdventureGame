using System.Diagnostics.Contracts;

public class Human : IRace
{
    public string RaceName{get;set;}
    public Human()
    {
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
    
}