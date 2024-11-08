class EnemyTile : RewardTile
{
    public int NrOfEnemies {get;set;}
    public DifficultyLevel Difficulty {get; set;}
    public string Race {get; set;}
    
    public EnemyTile(string tileName, int reward, int nrOfEnemies) : base(tileName, " ☠ ", reward)
    {
        NrOfEnemies = nrOfEnemies;
        Solid = true;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        
        if (!IsVisited) // Never entered the tile before!
        {
            string encounterMessage = $"As you step into the enemy lair, {NrOfEnemies} fierce foes appear, blocking your path!";
            Success = CombatHandler.RunCombatScenario(NPCFactory.GenerateNPCs(ConsoleColor.DarkRed,50,eEnemyFamily.Goblin,NrOfEnemies),playerList,encounterMessage);
            IsVisited = true;
            if (Success)
                Solid = false;

        }
        else // Entered the tile before
        {
            if (!Success)
            {
                string encounterMessage = $"\"Ah, it's you again! You weak and pitiful human, fleeing from us last time. You should have stayed away, coward!\"";
                Success = CombatHandler.RunCombatScenario(NPCFactory.GenerateNPCs(ConsoleColor.DarkRed,50,eEnemyFamily.Goblin,NrOfEnemies),playerList,encounterMessage);
            }
            else
            {
                Utilities.CharByCharLine("The room is quiet now... The scent of past victory lingers in the air. You have been here before.", 8, ConsoleColor.DarkBlue);
            }                
        }
    }
}

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}