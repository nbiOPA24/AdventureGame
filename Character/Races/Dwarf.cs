public class Dwarf : IRace
{
    public string RaceName{get;set;}
    public Dwarf()
    {
        RaceName = "Dwarf";
    }
    public int  AdjustHealth(int baseHealth)
    {
        return baseHealth +4;
    }
    public int AdjustDamage(int baseDamage)
    {
        return baseDamage -4;
    }
    
}