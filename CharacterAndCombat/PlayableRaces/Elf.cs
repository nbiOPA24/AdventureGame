public class Elf : IRace
{
    #region Elfspecific Abilities
    Ability ElvenShot = new Ability("Elven Shot",10);
    Ability BowSmack = new Ability("Bow Smack",8);
    #endregion
    public string RaceName{get;set;}
    public List<Ability> Abilities {get;set;}
    public Elf()
    {
        RaceName = "Elf";
        Abilities = new()
        {
            ElvenShot,
            BowSmack
        };
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