public class Dwarf : IRace
{
    
    #region DwarfSpecific abilities

    #endregion
    public string RaceName {get;}
    public  List<Ability> Abilities{get;}
    public Dwarf()
    {
        Abilities = new();
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