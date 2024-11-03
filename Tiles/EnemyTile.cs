class EnemyTile : RewardTile
{
    public int NrOfEnemies {get;set;}
    public DifficultyLevel Difficulty {get; set;}
    public string Race {get; set;}
    
    public EnemyTile(string tileName, int reward, int nrOfEnemies, DifficultyLevel difficulty, string race) : base(tileName, " ☠ ", reward)
    {
        NrOfEnemies = nrOfEnemies;
        Difficulty = difficulty;
        Race = race;
    }

    public override void RunTile(Character player)
    {
        if (TileState == false) // Never entered the tile before!
        {
            string encounterMessage = $"As you step into the enemy lair, {NrOfEnemies} fierce foes appear, blocking your path!";
            Success = CombatHandler.RunCombatScenario(CreateEnemies(),player,encounterMessage);
            TileState = true;
        }
        else // Entered the tile before
        {
            if (!Success)
            {
                string encounterMessage = $"\"Ah, it's you again! You weak and pitiful human, fleeing from us last time. You should have stayed away, coward!\"";
                Success = CombatHandler.RunCombatScenario(CreateEnemies(),player,encounterMessage);
            }
            else
            {
                Utilities.CharByCharLine("The room is quiet now... The scent of past victory lingers in the air. You have been here before.", 8, ConsoleColor.DarkBlue);
            }                
        }
    }

    public List<Character> CreateEnemies()
    {
        Random random = new Random();
        List<Character> returnList = new();
        for(int i = 0; i< NrOfEnemies ; i++)
        {
            string name="";
            switch(Race.ToUpper())
            {
                case "GOBLIN":
                    
                    switch(random.Next(1,5))
                    {
                        case 1:
                        name = "Goblin fighter";
                            break;
                        case 2:
                        name = "Goblin warlord";
                            break;
                        case 3: 
                        name = "Goblin shaman";
                            break;
                        case 4:
                        name = "Goblin scout";
                            break;
                        case 5:
                        name = "Goblin tamer";
                            break;
                    }
                    ICombatHandler combatHandler = new EnemySupportAI();
                    Character e = new Character(name,15*(int)Difficulty,10+random.Next(1,4),5*random.Next(1,4),combatHandler,ConsoleColor.DarkGray);
                    returnList.Add(e);
                    break;
            }
        }
        return returnList;
    }

}

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}