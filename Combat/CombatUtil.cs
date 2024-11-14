public static class CombatUtil
    {
        #region Ability list return
        /// <summary>
        /// Returns the ability with the highest priority
        /// </summary>
        /// <param name="relevantAbilities"></param>
        /// <returns></returns>
        public static Ability ReturnPriorityAbility(List<Ability> relevantAbilities)
        {
            Random random = new Random();
            

                relevantAbilities.OrderByDescending(x => x.PriorityValue);   
                List<Ability> topPriorityAbilities = new();
                int topPriorityNumber = relevantAbilities[0].PriorityValue;
                for(int i = 0 ; i< relevantAbilities.Count; i++)
                {
                    if(relevantAbilities[i].PriorityValue == topPriorityNumber)
                    {
                        topPriorityAbilities.Add(relevantAbilities[i]);
                    }
                    else
                        break;
                }
                
                return topPriorityAbilities[random.Next(0,topPriorityAbilities.Count)]; 
            

        }
        public static List<Ability> ReturnUsableAbilitiesOfType(List<Ability> abilityList, eAbilityType type)
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
    public static List<Character> ReturnHighestThreatList(Character self,List<Character> potentialTargets)
    {    
                List<Character> returnList = new();
                int highestThreat = 0;
                foreach(Character c in potentialTargets)
                {
                    if(self.ICombatSelector.AggroDictionary[c] > highestThreat) 
                    {
                        returnList.Clear();
                        returnList.Add(c);
                    }
                    else if(self.ICombatSelector.AggroDictionary[c] == highestThreat) returnList.Add(c);
                }
                return returnList;
    }
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
            return self; // Should not happen. combatloop only continues if the lists have characters in them

            Character returnCharacter = self;
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
    
    public static Character ReturnBestDispellTarget(Character self,List<Character> friendList, List<Ability> abilityList)
    {
        List<Ability> relevantAbilities = ReturnUsableAbilitiesOfType(abilityList,eAbilityType.CleanseOther);
        List<eCombatEffect> cleanseTypes = new();
        Character topPriorityCleanseTarget = null;
        bool foundFrost = false;
        bool foundPoison = false;

        foreach(Ability a in relevantAbilities)
        {
            foreach(Cleanse c in a.CombatEffects)
            {
                foreach(eCombatEffect e in c.TypesToCleanse)
                {
                    cleanseTypes.Add(e);
                }
            }
        }
        if(!relevantAbilities.Any())
        {
            foreach(Character c in friendList)
            {
                if(c.CharacterHasEffect(eCombatEffect.Freeze) && cleanseTypes.Contains(eCombatEffect.Freeze)&& c!= self)
                {   
                    topPriorityCleanseTarget = c;
                    foundFrost = true;

                } 
                else if(c.CharacterHasEffect(eCombatEffect.Poison) && cleanseTypes.Contains(eCombatEffect.Poison)&& c!= self && !foundFrost)
                {     
                    topPriorityCleanseTarget = c;
                    foundPoison = true;
                }
                else if(c.CharacterHasEffect(eCombatEffect.Burn) && cleanseTypes.Contains(eCombatEffect.Burn)&& c!= self && !foundPoison && !foundFrost)
                {     
                    topPriorityCleanseTarget = c;
                }
            }
        }
        return topPriorityCleanseTarget;
    }
    #endregion
}
    
    