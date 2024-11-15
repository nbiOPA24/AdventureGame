
public class SupportiveTargetingStrategy : ITargetSelectionStrategy
{
    public Random RandomNumber { get; set; }
    public SupportiveTargetingStrategy()
    {
       RandomNumber = new Random(); 
    }
    

    public Character GetEnemyTarget(NPC self)
    {
  
        
            // Roll based on intelligence: high intelligence favors lowest health targets
            int roll = RandomNumber.Next(1, 101);
            
            if (roll <= self.Intelligence) // If the roll is lower than the casters intelligence it will target the lowest health enemy character
            {
                // Prioritize lowest health target
                return CombatUtil.ReturnLowestHealthCharacter(self.EnemyList);
            }
            else
            {
                List<Character> targetList = CombatUtil.ReturnHighestThreatList(self,self.EnemyList);
                return targetList[RandomNumber.Next(0,targetList.Count)];
            }
    }

    public Character GetFriendlyTarget(NPC self)
    {
        Character lowestHealthCharacter = CombatUtil.ReturnLowestHealthFriendlyCharacter(self,self.FriendList);
        double lowestHealthPercentage = (double)lowestHealthCharacter.CurrentHealth/lowestHealthCharacter.MaxHealth;
        Character dispellTarget = CombatUtil.ReturnBestDispellTarget(self,self.FriendList,self.Abilities);
        List<Ability> relevantAbilities;

        //Priority 1: Heal if a friends health is bellow 30%
        if(lowestHealthPercentage <0.3 && lowestHealthCharacter != self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        //Priority 2: Cleanse a friend if they have a debuff you can cleanse
        if(dispellTarget != null)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.CleanseOther);
            if(relevantAbilities.Count != 0)
            {
                return dispellTarget;
            }
        }
        //Priority 3: Heal someone below 60% 
        if(lowestHealthPercentage < 0.6 && lowestHealthCharacter != self)
        {
            relevantAbilities = CombatUtil.ReturnUsableAbilitiesOfType(self.Abilities,eAbilityType.HealingOther);
            if(relevantAbilities.Count != 0)
            {
                return lowestHealthCharacter;
            }
        }
        else
        {
            self.ICombatBrain.CombatStateHandler.TransitionToNextState(self);    //Transition to next combatstate to ensure we find a suitable target        
        } 
        return null; // should be unreachable
    }
    public Character GetTarget(NPC self,Ability ability)
    {
        switch(ability.Target)
        {
            case eTargetType.AnyFriend:
            case eTargetType.Self:
                return self;
            case eTargetType.Friendly:
                return GetFriendlyTarget(self);
            case eTargetType.Enemy:
            case eTargetType.AnyEnemy:
                return GetEnemyTarget(self);
            
        }
        Console.WriteLine("Unknown TargetType check OffensiveTargeting.cs");
        return null; //ALL ABILITIES ARE OF THESE TYPES AND THIS SHOULD BE UNREACHABLE CODE
    }

}