public class CRE : Room
{
    public Tile[,] Room {get; set;}
    public CRE()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile x = new EnemyTile("Monsters", 10, 3, DifficultyLevel.Easy, "Goblin");


        Tile[,] crossRoomEasy = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , x , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { e, e , e , e , e , e , e , o , e },
            { o, e , o , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
        };
        return crossRoomEasy;
    }
}