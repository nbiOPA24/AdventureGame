
using System.ComponentModel;

public class NPCSupportAI : ICombatSelection
{
    public CombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public List<Character> FriendList {get;set;}
    public List<Character> EnemyList {get;set;}
    public Character Self {get;set;}
    public Random RandomNumber {get;set;}
    public NPCSupportAI()
    {
        CurrentCombatState = CombatState.Offensive;
        AbilityList = new List<Ability>();
        FriendList = new List<Character>();
        EnemyList = new List<Character>();
        RandomNumber = new Random();
    }
    public Ability SelectAbility()
    {
        Ability ability = null;

        switch (CurrentCombatState)
        {
            case CombatState.Offensive:
                ability = ChooseOffensiveAbility();
                break;
            case CombatState.Defensive:
                ability = ChooseDefensiveAbility();
                break;
            case CombatState.Supportive:
                ability = ChooseSupportiveAbility();
                break;
            case CombatState.Default:
                ability = new Ability("Smack", TargetType.Enemy, 0, AbilityType.Offensive);
                ability.AddDamageEffect(5);
                return ability;
        }

        if (ability != null)
            return ability;

        TransitionToNextState();
        return SelectAbility();
    }

        
    public Character ChooseTarget(Character self,TargetType targetType,List<Character> potentialTargets)
    {   
        switch(targetType)
        {
            case TargetType.Self: return self;
            case TargetType.Enemy: return potentialTargets[RandomNumber.Next(0,potentialTargets.Count)];             
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

        //Priority 1: Heal if a friends health is bellow 30%
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //Priority 2: Cleanse a friend if they have a debuff you can cleanse
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.CleanseOther);
            if(relevantAbilities.Count != 0)
            {
                return dispellTarget;
            }
        }
        //Priority 3: Heal someone below 60% 
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //Default sets combatstate to offensive
        else
        {
            CurrentCombatState = CombatState.Offensive;
            
        } 
        return null; // should be unreachable
    }

    public Ability ChooseDefensiveAbility()
    {
        List<Ability> relevantAbilities;
        if((double)Self.CurrentHealth/Self.MaxHealth < 1)
        {
           relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingSelf);
           if(relevantAbilities.Count != 0)
           {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
           }
           
        }
        else if(CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.DefensiveSelf).Count !=0)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.DefensiveSelf);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
            }

        } 
        return null;
    }
    public Ability ChooseOffensiveAbility()
    {
        List<Ability> relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.Offensive);
        if(relevantAbilities.Count > 0)
        {
           return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
        }
        else return null;
    }

    public Ability ChooseSupportiveAbility()
    {  
        Character lowestHealthCharacter = CombatUtil.ReturnLowestHealthFriendlyCharacter(Self,FriendList);
        double lowestHealthPercentage = (double)lowestHealthCharacter.CurrentHealth/lowestHealthCharacter.MaxHealth;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(Self,FriendList,AbilityList);

        List<Ability> relevantAbilities;
        //These switchstatements decide what will be done in order, top got priority
        //Checks if a friendly target is below 30% health. if so chooses a healother type ability
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
            }
        }
        //if anyone has an applicable debuff that can be cleansed 
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.CleanseOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
                
            }
        }
        //if someone else is below 60% health
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
            }
        }
        
        return null;
    }
    public void UpdateCombatState()
    {
        bool foundSupportive = false;
        bool foundDefensive = false;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(Self,FriendList,AbilityList);
        List<Ability> healingSpells = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,AbilityType.HealingOther);
        foreach(Character c in FriendList)
        {
            if((c != Self && (double)c.CurrentHealth/c.MaxHealth < 0.3  && healingSpells.Count != 0 )|| dispellTarget != null )
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
    public void TransitionToNextState()
    {
        if (CurrentCombatState == CombatState.Offensive)
            CurrentCombatState = CombatState.Defensive;
        else if (CurrentCombatState == CombatState.Defensive)
            CurrentCombatState = CombatState.Supportive;
        else if (CurrentCombatState == CombatState.Supportive)
            CurrentCombatState = CombatState.Default;
        else
            CurrentCombatState = CombatState.Offensive;
    }
}
