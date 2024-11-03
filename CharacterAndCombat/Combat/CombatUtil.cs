public static class CombatUtil
    {
        #region Ability list return
            public static List<Ability> ReturnUsableAbilitiesOfType(List<Ability> abilityList, AbilityType type)
        {
            List<Ability> returnList = new List<Ability>();
            for(int i = 0 ; i <abilityList.Count;i++)
            {
                if(abilityList[i].Type == type && abilityList[i].CurrentCooldown == abilityList[i].CoolDownTimer)
                {
                    returnList.Add(abilityList[i]);
                }
            }
            return returnList;
        }
        #endregion
        #region CharacterList sorting
    public static Character ReturnLowestHealthCharacter(List<Character> characterList)
    {
            if (characterList == null || characterList.Count == 0)
            return null; // Should not happen. combatloop only continues if the lists have characters in them

            Character returnCharacter = characterList[0];
            double lowestPercentage = 1;
            for(int i = 0 ; i< characterList.Count;i++)
            {
                double percentageHealth = (double)characterList[i].CurrentHealth/characterList[i].MaxHealth;
                if(percentageHealth < lowestPercentage)
                {
                    returnCharacter = characterList[i];
                    lowestPercentage = percentageHealth;
                }
            }
            return returnCharacter;
    }
        public static Character ReturnLowestHealthFriendlyCharacter(Character self,List<Character> friendList)
    {
            if (friendList == null || friendList.Count == 0)
            return null; // Should not happen. combatloop only continues if the lists have characters in them

            Character returnCharacter = friendList[0];
            double lowestPercentage = 1;
            for(int i = 0 ; i< friendList.Count;i++)
            {
                double percentageHealth = (double)friendList[i].CurrentHealth/friendList[i].MaxHealth;
                if(percentageHealth < lowestPercentage && friendList[i] != self)
                {
                    returnCharacter = friendList[i];
                    lowestPercentage = percentageHealth;
                }
            }
            return returnCharacter;
    }
    
    public static Character ReturnBestDispellTarget(List<Character> friendList, List<Ability> abilityList)
    {
        List<Ability> relevantAbilities = ReturnUsableAbilitiesOfType(abilityList,AbilityType.CleanseOther);
        List<eCombatEffect> dispellableTypes = new();
        Character topPriorityDebuffTarget = null;
        int previousSeverity = 0;

        foreach(Ability a in relevantAbilities)
        {
            foreach(CombatEffect e in a.CombatEffects)
            {
                dispellableTypes.Add(e.Type);
            }
        }
        
        if(relevantAbilities.Count != 0)
        {
            foreach(Character c in friendList)
            {
                foreach(CombatEffect effect in c.CurrentStatusEffects)
                {
                    if(effect.Type == eCombatEffect.Freeze)
                    {
                        foreach(eCombatEffect e in dispellableTypes)
                        {
                            if(e == eCombatEffect.Freeze)
                            {
                                if(topPriorityDebuffTarget !=null 
                                && topPriorityDebuffTarget.CurrentHealth/topPriorityDebuffTarget.MaxHealth > c.CurrentHealth/c.MaxHealth)
                                {
                                    topPriorityDebuffTarget = c;
                                    previousSeverity = 1;
                                }
                            }
                            
                        }
                    } 
                    else if(effect.Type == eCombatEffect.Poison && previousSeverity <1)
                    {
                        foreach(eCombatEffect e in dispellableTypes)
                        {
                            if(e == eCombatEffect.Poison)
                            {
                                if(topPriorityDebuffTarget !=null 
                                && topPriorityDebuffTarget.CurrentHealth/topPriorityDebuffTarget.MaxHealth > c.CurrentHealth/c.MaxHealth)
                                {
                                    topPriorityDebuffTarget = c;
                                }
                            }
                            
                        }
                    } 
                }
            }
        }
        return topPriorityDebuffTarget;
    }
    #endregion
}
    
    