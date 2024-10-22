public class Ability
{
    public string Name{get;set;}
    public int BaseDamage{get;set;}
    
    public Ability(string name,int baseDamage)
    {
        Name = name;
        BaseDamage = baseDamage;
    }
}