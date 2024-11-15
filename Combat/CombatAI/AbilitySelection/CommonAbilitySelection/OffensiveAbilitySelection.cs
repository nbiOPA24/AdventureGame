
public class OffensiveAbilitySelection : IAbilitySelectionStrategy
{
    public Random RandomNumber { get; set; }
    public OffensiveAbilitySelection()
    {
        RandomNumber = new Random();
    }

    public Ability ChooseDefensiveAbility(Character self)
    {
        List<Ability> relevantAbilities;
        if((double)self.CurrentHealth/self.MaxHealth < 1)
        {
           relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingSelf);
           if(relevantAbilities.Count != 0)
           {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
           }
           
        }
        else if(CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.DefensiveSelf).Count !=0)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.DefensiveSelf);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)]; 
            }

        } 
        return null;
    }


    public Ability ChooseOffensiveAbility(Character self)
    {
        List<Ability> relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.OffensiveStrong);

        if(relevantAbilities.Count > 0)
        {
            return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
        }
        relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.Offensive);
        if(relevantAbilities.Count > 0)
        {
            return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
        }
        else return null;
    }



    public Ability ChooseSupportiveAbility(Character self)
    {  
        Character lowestHealthCharacter = CombatUtil.ReturnLowestHealthFriendlyCharacter(self,self.FriendList);
        double lowestHealthPercentage = (double)lowestHealthCharacter.CurrentHealth/lowestHealthCharacter.MaxHealth;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(self,self.FriendList,self.Abilities);

        List<Ability> relevantAbilities;
        //These switchstatements decide what will be done in order, top got priority
        //Checks if a friendly target is below 30% health. if so chooses a healother type ability
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
            }
        }
        //if anyone has an applicable debuff that can be cleansed 
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.CleanseOther);
            if(relevantAbilities != null && relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
                
            }
        }
        //if someone else is below 60% health
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return relevantAbilities[RandomNumber.Next(0,relevantAbilities.Count)];
            }
        }
        
        return null;
    }

    public Ability SelectAbility(NPC self)
    {
        Ability ability = null;

        switch (self.ICombatBrain.CombatStateHandler.CurrentCombatState)
        {
            case eCombatState.Offensive:
                ability = ChooseOffensiveAbility(self);
                break;
            case eCombatState.Defensive:
                ability = ChooseDefensiveAbility(self);
                break;
            case eCombatState.Supportive:
                ability = ChooseSupportiveAbility(self);
                break;
            case eCombatState.Default:
                ability = new Ability("PlaceHolderSpell! you dont have enough fillers in this character", eTargetType.Enemy, 0, eAbilityType.Offensive);
                ability.AddDamageEffect(5,false);
                return ability;
        }

        if (ability != null)
            return ability;

        self.ICombatBrain.CombatStateHandler.TransitionToNextState(self);
        return SelectAbility(self);
    }
}