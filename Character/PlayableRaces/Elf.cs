public class Elf : IRace
{
    public string RaceName{get;set;}
    public Elf()
    {
        RaceName = "Elf";
    }
    public int  AdjustHealth(int baseHealth)
    {
        return baseHealth -4;
    }
    public int AdjustDamage(int baseDamage)
    {
        return baseDamage +4;
    }
    
}