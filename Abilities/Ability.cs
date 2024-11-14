public class Ability
{
    public string Name {get;set;}
    public string Description {get;set;}
    public List<CombatEffect> CombatEffects {get;set;}
    public eTargetType Target {get;set;}
    public eAbilityType Type {get;set;}
    public int CoolDownTimer {get;set;}
    public int CurrentCooldown {get;set;}
    public int PriorityValue {get;set;}

    
    public Ability(string name,eTargetType target,int cooldown,eAbilityType type)
    {
        Name = name;
        CombatEffects = new();
        Target = target;
        CoolDownTimer = cooldown+1;
        CurrentCooldown = cooldown+1;
        Type = type;
        Description = "Add description";
        PriorityValue = 0;

    }
    //Constructor Mainly used for bosses! it adds customisable priority value to an ability. highest value == used first
    public Ability(string name,eTargetType target,int cooldown,eAbilityType type,int priorityValue)
    {
        Name = name;
        CombatEffects = new();
        Target = target;
        CoolDownTimer = cooldown+1;
        CurrentCooldown = cooldown+1;
        Type = type;
        Description = "Add description";
        PriorityValue = priorityValue;

    }
    #region  ADD COMBATEFFECTS
    public void AddFreezeEffect(int duration,bool areaEffect)
    {
        Freeze freeze = new(duration,areaEffect);
        CombatEffects.Add(freeze);
    }
    public void AddPoisonEffect(int duration,int magnitude,bool areaEffect)
    {
        Poison poison = new(duration,magnitude,areaEffect);
        CombatEffects.Add(poison);
    }
    public void AddBurnEffect(int duration, int magnitude,bool areaEffect)
    {
        Burn burn = new(duration, magnitude,areaEffect);
        CombatEffects.Add(burn);
    }

    public void AddImmuneEffect(int duration,bool areaEffect)
    {
        Immune immune = new(duration,areaEffect);
        CombatEffects.Add(immune);
    }
    public void AddDamageEffect(int magnitude,bool areaEffect)
    {
        Damage damage = new(magnitude,areaEffect);
        CombatEffects.Add(damage);
    }
    public void AddThreatEffect(int magnitude,bool areaEffect)
    {
        Threat threat = new(magnitude,areaEffect);
        CombatEffects.Add(threat);
    }
    
    public void AddHealingEffect(int magnitude,bool areaEffect)
    {
        Healing healing = new(magnitude,areaEffect);
        CombatEffects.Add(healing);
    }
    public void AddHealingOverTimeEffect(int magnitude, int duration,bool areaEffect)
    {
        HealingOverTime healOverTime = new HealingOverTime(duration, magnitude,areaEffect);
        CombatEffects.Add(healOverTime);
    }
    public void AddArmorBuffEffect(int magnitude,int duration,bool areaEffect)
    {
        ArmorBuff armorbuff = new(duration,magnitude,areaEffect);
        CombatEffects.Add(armorbuff);
    }
    public void AddAttackBuffEffect(int magnitude,int duration,bool areaEffect)
    {
        AttackBuff attackBuff = new(duration,magnitude,areaEffect);
        CombatEffects.Add(attackBuff);
    }
    public void AddCleanseEffect(List<eCombatEffect> effects,bool areaEffect)
    {
        Cleanse cleanse = new Cleanse(effects,areaEffect);
        CombatEffects.Add(cleanse);
    }
    public void AddCleanseEffect(eCombatEffect effect,bool areaEffect)
    {
        List<eCombatEffect> effects = new()
        {
            effect
        };
        Cleanse cleanse  = new(effects,areaEffect);
        
        
        CombatEffects.Add(cleanse);
    }
    public void AddSummonEffect(eEnemyFamily family,eEnemyType type,int intelligenceOfSummonedMonster,int amountOfNpcs)
    {
        Summon summon = new(family,type,intelligenceOfSummonedMonster,amountOfNpcs);
        CombatEffects.Add(summon);
    }
    #endregion

    public Ability Clone()
    {
        Ability clonedAbility = new Ability(Name,Target,CoolDownTimer-1,Type);
        return clonedAbility;
    }

}


