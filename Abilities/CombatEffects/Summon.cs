public class Summon : CombatEffect
{
    public eEnemyFamily FamilyToGenerateFrom {get;set;}
    public eEnemyType TypeToSummon {get;set;}
    public int IntelligenceForSummon {get;set;}
    public int AmountOfNpcs {get;set;}
    public Summon(eEnemyFamily family,eEnemyType type,int intelligenceOfSummonedMonster,int amountOfNpcs) : base(1, 1, eCombatEffect.Summon,false) // Duration of 1 for instant application
    {
        FamilyToGenerateFrom = family;
        TypeToSummon = type;
        IntelligenceForSummon = intelligenceOfSummonedMonster;
        AmountOfNpcs = amountOfNpcs;
    }

    public override void ApplyEffect(Character caster,Character target,List<Character> targetTeam,List<Character> otherTeam)
    {
        for(int i = 0 ; i < AmountOfNpcs; i++)
            {
                Character newCharacter = NPCFactory.GenerateNPC(target.NameColor,IntelligenceForSummon,FamilyToGenerateFrom,TypeToSummon);
                Dictionary<Character ,int > newAggroDictionary = new();

                newCharacter.ICombatSelector.AggroDictionary = newAggroDictionary;
                newCharacter.ICombatSelector.AbilityList = newCharacter.Abilities;
                newCharacter.ICombatSelector.FriendList = caster.ICombatSelector.FriendList;
                newCharacter.ICombatSelector.EnemyList = caster.ICombatSelector.EnemyList;
                newCharacter.ICombatSelector.Self = newCharacter;
                targetTeam.Add(newCharacter);
                Console.WriteLine($"A {newCharacter.Name} has been summoned");
                foreach(Character c in otherTeam)
                {
                    c.ICombatSelector.AggroDictionary.Add(newCharacter,0);
                    newCharacter.ICombatSelector.AggroDictionary.Add(c,0);
                }
            }

    }

    public override CombatEffect CloneEffect()
    {
        return new Summon(FamilyToGenerateFrom, TypeToSummon, IntelligenceForSummon, AmountOfNpcs);
    }

}