public class Dwarf : IRace
{
    
    #region DwarfSpecific abilities
    public static Ability DwarvenCharge = new Ability("Dwarven smash",10);
    public static Ability TossMe = new Ability("Toss Me",15);
    #endregion
    public string RaceName {get;}
    public  List<Ability> Abilities{get;}
    public Dwarf()
    {
        Abilities = new List<Ability>()
        {
            DwarvenCharge,
            TossMe
        };
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
   

    public  List<Ability> GetAbilities()
    {
       return Abilities;
    }


    
}