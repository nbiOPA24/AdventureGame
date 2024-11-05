
using System.ComponentModel;

public class EnemySupportAI : ICombatSelection
{
    public CombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public List<Character> FriendList {get;set;}
    public List<Character> EnemyList {get;set;}
    public Character Self {get;set;}
    public EnemySupportAI()
    {
        CurrentCombatState = CombatState.Offensive;
        AbilityList = new();
        FriendList = new();
        EnemyList = new();
    }
    public Ability SelectAbility()
{
    switch (CurrentCombatState)
    {
        case CombatState.Offensive:
            Ability offensiveAbility = ChooseOffensiveAbility();
            if (offensiveAbility != null) return offensiveAbility;
            CurrentCombatState = CombatState.Defensive;
            return SelectAbility(); //Try again with new state

        case CombatState.Defensive:
            Ability defensiveAbility = ChooseDefensiveAbility();
            if (defensiveAbility != null) return defensiveAbility;
            CurrentCombatState = CombatState.Supportive;
            return SelectAbility(); //Try again with new state

        case CombatState.Supportive:
            Ability supportiveAbility = ChooseSupportiveAbility();
            if (supportiveAbility != null) return supportiveAbility;
            CurrentCombatState = CombatState.Default;
            return SelectAbility(); //Try again with new state

        case CombatState.Default:
            Ability smack = new("Smack", TargetType.Enemy, 0, AbilityType.Offensive);
            smack.AddDamageEffect(5);
            return smack;
    }
    
    return null; // Should never reach here because each case returns an ability
}

    
    public Character ChooseTarget(Character self,TargetType targetType,List<Character> potentialTargets)
    {   Random random = new Random();
        switch(targetType)
        {
            case TargetType.Self: return self;
            case TargetType.Enemy: return potentialTargets[random.Next(0,potentialTargets.Count)];             
            case TargetType.Friendly: return GetSupportiveTarget(FriendList);
        }
        return null; // should be unreachable
    }
    public Character GetSupportiveTarget(List<Character> potentialTargets)
    {
        Character lowestHealthCharacter = CombatUtil.ReturnLowestHealthFriendlyCharacter(Self,FriendList);
        double lowestHealthPercentage = (double)lowestHealthCharacter.CurrentHealth/lowestHealthCharacter.MaxHealth;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(Self,FriendList,AbilityList);
        List<Ability> relevantAbilities;
        //These switchstatements decide what will be done in order, top got priority
        //Checks if a friendly target is below 50% health. if so chooses a healother type ability
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //If a target can be dispelled and the lowest health percenrage is above 80%
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.CleanseOther);
            if(relevantAbilities.Count != 0)
            {
                return dispellTarget;
            }
        }
        //if someone is damaged at all
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        else
        {
            CurrentCombatState = CombatState.Offensive;
            
        } 
        return null; // should be unreachable
    }

    public Ability ChooseDefensiveAbility()
    {
        List<Ability> relevantAbilities;
        Random random = new Random();
        if((double)Self.CurrentHealth/Self.MaxHealth < 1)
        {
           relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingSelf);
           if(relevantAbilities.Count != 0)
           {
                return relevantAbilities[random.Next(0,relevantAbilities.Count)]; 
           }
           
        }
        else if(CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.DefensiveSelf).Count !=0)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.DefensiveSelf);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[random.Next(0,relevantAbilities.Count)]; 
            }

        } 
        return null;
    }
    public Ability ChooseOffensiveAbility()
    {
        List<Ability> relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.Offensive);
        Random random = new Random();
        if(relevantAbilities.Count > 0)
        {
           return relevantAbilities[random.Next(0,relevantAbilities.Count)]; 
        }
        else return null;
    }

    public Ability ChooseSupportiveAbility()
    {  
        Character lowestHealthCharacter = CombatUtil.ReturnLowestHealthFriendlyCharacter(Self,FriendList);
        if (lowestHealthCharacter == null)
        {
            Console.WriteLine("lowestHealthCharacter is null");
        }
        double lowestHealthPercentage = (double)lowestHealthCharacter.CurrentHealth/lowestHealthCharacter.MaxHealth;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(Self,FriendList,AbilityList);
        if (dispellTarget == null)
        {
            Console.WriteLine("dispellTarget is null");
        }

        List<Ability> relevantAbilities;
        Random random = new Random();
        //These switchstatements decide what will be done in order, top got priority
        //Checks if a friendly target is below 30% health. if so chooses a healother type ability
        Console.WriteLine(lowestHealthPercentage);//REMOVE
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[random.Next(0,relevantAbilities.Count)];
            }
        }
        //if anyone has an applicable debuff that can be cleansed 
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.CleanseOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                Console.WriteLine("returning dispell");//LOG
                return relevantAbilities[random.Next(0,relevantAbilities.Count)];
                
            }
        }
        //if someone else is below 60% health
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[random.Next(0,relevantAbilities.Count)];
            }
        }
        
        return null;
    }
    public void UpdateCombatState()
    {
        bool foundSupportive = false;
        bool foundDefensive = false;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(Self,FriendList,AbilityList);
        Console.WriteLine("UpdateCombatstate after returnbestdispelltarget:"+dispellTarget);//LOG
        foreach(Character c in FriendList)
        {
            if((c != Self && (double)c.CurrentHealth/c.MaxHealth < 0.3 )|| dispellTarget != null )
            {
                foundSupportive = true;
                break;
            }
        }
        if((double)Self.CurrentHealth / Self.MaxHealth < 0.5) foundDefensive = true;
        
        CurrentCombatState = foundSupportive ? CombatState.Supportive:
                             foundDefensive ? CombatState.Defensive:
                             CombatState.Offensive;                        
    }
}
