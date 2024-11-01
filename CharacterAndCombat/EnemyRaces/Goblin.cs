public class Goblin : IRace
{
    #region Elfspecific Abilities
    Ability Bash = new Ability("Bash",TargetType.Enemy,1);
    Ability Growl = new Ability("Growl",TargetType.Enemy,1);
    #endregion
    public string RaceName{get;set;}
    public List<Ability> Abilities {get;set;}
    public Goblin()
    {
        RaceName = "Goblin";
        Abilities = new()
        {
            Bash,
            Growl
        };
    }
    public int  AdjustHealth(int baseHealth)
    {
        return baseHealth +0;
    }
    public int AdjustDamage(int baseDamage)
    {
        return baseDamage +0;
    }
    public List<Ability> GetAbilities()
    {
        return Abilities;
    }
    
}