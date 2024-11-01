using System.ComponentModel;

public class Ability
{
    public string Name {get;set;}
    public List<CombatEffect> CombatEffects {get;set;}
    public TargetType Target {get;set;}
    public int CoolDownTimer {get;set;}
    public int CurrentCooldown {get;set;}

    
    public Ability(string name,TargetType target,int cooldown)
    {
        Name = name;
        CombatEffects = new();
        Target = target;
        CoolDownTimer = cooldown+1;
        CurrentCooldown = cooldown+1;

    }

    public void AddFreezeEffect(int duration)
    {
        Freeze freeze = new(duration);
        CombatEffects.Add(freeze);
    }
    public void AddPoisonEffect(int duration,int magnitude)
    {
        Poison poison = new(duration,magnitude);
        CombatEffects.Add(poison);
    }
    public void AddImmuneEffect(int duration)
    {
        Immune immune = new(duration);
        CombatEffects.Add(immune);
    }
    public void AddDamageEffect(int magnitude)
    {
        Damage damage = new(magnitude);
        CombatEffects.Add(damage);
    }
    public void AddHealingEffect(int magnitude)
    {
        Healing healing = new(magnitude);
        CombatEffects.Add(healing);
    }


}


