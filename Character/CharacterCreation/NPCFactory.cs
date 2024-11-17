public static class NPCFactory
{
    static Random randomNumber = new Random();

    /// <summary>
    /// Intelligence should be a number between 1-100
    /// </summary>
    /// <param name="inputName"></param>
    /// <param name="enemyType"></param>
    /// <param name="selfColor"></param>
    /// <param name="intelligence"></param>
    /// <returns></returns>
    public static Character GenerateNPC(ConsoleColor selfColor,int intelligence,eEnemyFamily family,eEnemyType type) //Generates an npc from the specified family
    {
        Character character = null;
        switch(family)
        {
            case eEnemyFamily.Goblin:
            character = GoblinFactory.GenerateGoblin(selfColor,intelligence,type);       //Generates
                break;
        }
        return character;
    }
    //Generates a specified number of npcs from the specified family of npcs for example "Goblin"
    public static List<Character> GenerateNPCs(ConsoleColor selfColor,int intelligence,eEnemyFamily family,int amountOfNpcs,eEnemyType type,bool randomType) 
    {
        List<Character> returnListOfNPCs = new List<Character>();

        for(int i = 0 ; i < amountOfNpcs ; i++)
        {

            if(randomType)
            {
                switch(randomNumber.Next(0,2))
                {
                    case 0:
                        type = eEnemyType.Supportive;
                        break;
                    case 1:
                        type = eEnemyType.Offensive;
                        break; 
                } 
            }
            returnListOfNPCs.Add(GenerateNPC(selfColor,intelligence,family,type));
            
        }
        return returnListOfNPCs;
    }

}