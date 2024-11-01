public class CRH : Room
{
    public Tile[,] Room {get; set;}
    public CRH()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile x = new EnemyTile("Monsters", 10, 3, DifficultyLevel.Hard, "Goblin");


        Tile[,] crossRoomHard = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , o , e , e , e , o , e , e },
            { e, e , e , e , x , e , e , e , e },
            { o, e , o , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
        };
        return crossRoomHard;
    }
}