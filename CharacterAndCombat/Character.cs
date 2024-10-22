public class Character
{
    public string Name {get;set;}
    public int CurrentHealth {get;set;}
    public int MaxHealth {get;set;}
    public int BaseDamage {get;set;}
    public int Armor {get;set;}
    public IRace Race {get;set;}


    public Character(string name,int startingHealth,IRace race,int baseDamage,int armor)
    {
        Race = race;
        CurrentHealth = race.AdjustHealth(startingHealth);
        BaseDamage = race.AdjustDamage(baseDamage);
        Name = name;
        MaxHealth = CurrentHealth;
        Armor = armor;
        
    } 

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage-Armor;
        if(CurrentHealth < 0) CurrentHealth = 0;
    }
    public int DealDamage()
    {
        return BaseDamage;
    }
}