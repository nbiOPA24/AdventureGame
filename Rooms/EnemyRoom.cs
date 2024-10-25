class EnemyRoom : RewardRoom
{
    public int NrOfEnemies {get;set;}
    public DifficultyLevel Difficulty {get; set;}
    public string Race {get; set;}
    
    public EnemyRoom(string roomName, int reward, int nrOfEnemies, DifficultyLevel difficulty, string race) : base(roomName, " ☠ ", reward)
    {
        NrOfEnemies = nrOfEnemies;
        Difficulty = difficulty;
        Race = race;
    }

    public override void RunRoom(Player player)
    {
        if (RoomState == false)
        {
            string encounterMessage = $"You enter the enemy lair, and {NrOfEnemies} foes immediately block your path!";
            CombatHandler.RunCombatScenario(CreateEnemies(),player,encounterMessage);

            
            RoomState = true;
        }
        else
        {
            Console.WriteLine("You've been here before. No battles yet, but stay tuned! Andreas is preparing something exciting!");
        }
    }

    public List<Enemy> CreateEnemies()
    {
        Random random = new Random();
        List<Enemy> returnList = new();
        for(int i = 0; i< NrOfEnemies ; i++)
        {
            string name="";
            IRace iRace;
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
                    iRace = new Goblin();
                    
                    Enemy e = new Enemy(name,15*(int)Difficulty,iRace,10+random.Next(1,4),5*random.Next(1,4));
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