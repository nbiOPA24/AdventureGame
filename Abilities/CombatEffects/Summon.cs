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

    public override void ApplyEffect(Character caster,Character target)
    {
        for(int i = 0 ; i < AmountOfNpcs; i++)
            {
                ConsoleColor nameColor = target.NameColor == ConsoleColor.Magenta ? ConsoleColor.DarkRed : target.NameColor;
                Character newCharacter = NPCFactory.GenerateNPC(nameColor,IntelligenceForSummon,FamilyToGenerateFrom,TypeToSummon);
                Dictionary<Character ,int > newAggroDictionary = new();
                newCharacter.AggroDictionary = newAggroDictionary;
                
                newCharacter.FriendList = caster.FriendList;
                newCharacter.EnemyList = caster.EnemyList;
                target.FriendList.Add(newCharacter);

              

                Console.WriteLine($"A {newCharacter.Name} has been summoned");
                foreach(Character c in target.EnemyList)
                {
                    c.AggroDictionary.Add(newCharacter,0);
                    newCharacter.AggroDictionary.Add(c,0);
                }
            }

    }

    public override CombatEffect CloneEffect()
    {
        return new Summon(FamilyToGenerateFrom, TypeToSummon, IntelligenceForSummon, AmountOfNpcs);
    }

}