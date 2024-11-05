public class Ability
{
    public string Name {get;set;}
    public List<CombatEffect> CombatEffects {get;set;}
    public eTargetType Target {get;set;}
    public eAbilityType Type {get;set;}
    public int CoolDownTimer {get;set;}
    public int CurrentCooldown {get;set;}

    
    public Ability(string name,eTargetType target,int cooldown,eAbilityType type)
    {
        Name = name;
        CombatEffects = new();
        Target = target;
        CoolDownTimer = cooldown+1;
        CurrentCooldown = cooldown+1;
        Type = type;

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
    public void AddBurnEffect(int duration, int magnitude)
    {
        Burn burn = new(duration, magnitude);
        CombatEffects.Add(burn);
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
    public void AddArmorBuffEffect(int magnitude,int duration)
    {
        ArmorBuff armorbuff = new(duration,magnitude);
        CombatEffects.Add(armorbuff);
    }
    public void AddCleanseEffect(List<eCombatEffect> effects)
    {
        Cleanse cleanse = new Cleanse(effects);
        CombatEffects.Add(cleanse);
    }
    public void AddCleanseEffect(eCombatEffect effect)
    {
        List<eCombatEffect> effects = new()
        {
            effect
        };
        Cleanse cleanse  = new(effects);
        
        
        CombatEffects.Add(cleanse);
    }

    public Ability Clone()
    {
        Ability clonedAbility = new Ability(Name,Target,CoolDownTimer-1,Type);
        return clonedAbility;
    }

}


