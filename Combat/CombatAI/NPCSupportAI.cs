
using System.ComponentModel;

public class NPCSupportAI : ICombatSelection
{
    public eCombatState CurrentCombatState {get;set;}
    public List<Ability> AbilityList {get;set;}
    public List<Character> FriendList {get;set;}
    public List<Character> EnemyList {get;set;}
    public Dictionary<Character,int> AggroDictionary {get;set;}
    public Character Self {get;set;}
    public Random RandomNumber {get;set;}
    public NPCSupportAI()
    {
        CurrentCombatState = eCombatState.Offensive;
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
            case eCombatState.Offensive:
                ability = ChooseOffensiveAbility();
                break;
            case eCombatState.Defensive:
                ability = ChooseDefensiveAbility();
                break;
            case eCombatState.Supportive:
                ability = ChooseSupportiveAbility();
                break;
            case eCombatState.Default:
                ability = new Ability("Smack", eTargetType.Enemy, 0, eAbilityType.Offensive);
                ability.AddDamageEffect(5,false);
                return ability;
        }

        if (ability != null)
            return ability;

        TransitionToNextState();
        return SelectAbility();
    }

        
    public Character ChooseTarget(Ability a,Character self,eTargetType targetType,List<Character> potentialTargets,List<Character> playerList,List<Character> enemyList)
    {   
        switch(targetType)
        {
            case eTargetType.Self: return self;
            case eTargetType.Enemy: return potentialTargets[RandomNumber.Next(0,potentialTargets.Count)];             
            case eTargetType.Friendly: return GetSupportiveTarget(FriendList);
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
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //Priority 2: Cleanse a friend if they have a debuff you can cleanse
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.CleanseOther);
            if(relevantAbilities.Count != 0)
            {
                return dispellTarget;
            }
        }
        //Priority 3: Heal someone below 60% 
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //Default sets combatstate to offensive
        else
        {
            CurrentCombatState = eCombatState.Offensive;
            
        } 
        return null; // should be unreachable
    }

    public Ability ChooseDefensiveAbility()
    {
        List<Ability> relevantAbilities;
        if((double)Self.CurrentHealth/Self.MaxHealth < 1)
        {
           relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingSelf);
           if(relevantAbilities.Count != 0)
           {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
           }
           
        }
        else if(CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.DefensiveSelf).Count !=0)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.DefensiveSelf);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
            }

        } 
        return null;
    }
    public Ability ChooseOffensiveAbility()
    {
        List<Ability> relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.Offensive);
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
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
            }
        }
        //if anyone has an applicable debuff that can be cleansed 
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.CleanseOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
                
            }
        }
        //if someone else is below 60% health
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != Self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingOther);
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
        List<Ability> healingSpells = CombatUtil.ReturnUsableAbilitiesOfType(AbilityList,eAbilityType.HealingOther);
        foreach(Character c in FriendList)
        {
            if((c != Self && (double)c.CurrentHealth/c.MaxHealth < 0.3  && healingSpells.Count != 0 )|| dispellTarget != null )
            {
                foundSupportive = true;
                break;
            }
        }
        if((double)Self.CurrentHealth / Self.MaxHealth < 0.5) foundDefensive = true;
        
        CurrentCombatState = foundSupportive ? eCombatState.Supportive:
                             foundDefensive ? eCombatState.Defensive:
                             eCombatState.Offensive;                        
    }
    public void TransitionToNextState()
    {
        if (CurrentCombatState == eCombatState.Offensive)
            CurrentCombatState = eCombatState.Defensive;
        else if (CurrentCombatState == eCombatState.Defensive)
            CurrentCombatState = eCombatState.Supportive;
        else if (CurrentCombatState == eCombatState.Supportive)
            CurrentCombatState = eCombatState.Default;
        else
            CurrentCombatState = eCombatState.Offensive;
    }

    public Ability SelectAbility(Character self,List<Character> playerList, List<Character> enemyList)
    {
        Ability ability = null;

        switch (CurrentCombatState)
        {
            case eCombatState.Offensive:
                ability = ChooseOffensiveAbility();
                break;
            case eCombatState.Defensive:
                ability = ChooseDefensiveAbility();
                break;
            case eCombatState.Supportive:
                ability = ChooseSupportiveAbility();
                break;
            case eCombatState.Default:
                ability = new Ability("Smack", eTargetType.Enemy, 0, eAbilityType.Offensive);
                ability.AddDamageEffect(5,false);
                return ability;
        }

        if (ability != null)
            return ability;

        TransitionToNextState();
        return SelectAbility();
    }
}
