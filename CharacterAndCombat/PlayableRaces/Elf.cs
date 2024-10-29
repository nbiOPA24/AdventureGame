public class Elf : IRace
{
    #region Elfspecific Abilities

    #endregion
    public string RaceName{get;set;}
    public List<Ability> Abilities {get;set;}
    public Elf()
    {
        Abilities = new();
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
    public List<Ability> GetAbilities()
    {
        return Abilities;
    }
    
}