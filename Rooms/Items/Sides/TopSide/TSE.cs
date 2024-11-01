public class TSE : Room
{
    public Tile[,] Room {get; set;}
    public TSE()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile x = new EnemyTile("Monsters", 10, 3, DifficultyLevel.Easy, "Goblin");


        Tile[,] topSideRoom = new Tile [,]
        {
            { o, o , o , o , o , o , o , o , o },
            { o, e , e , e , e , e , e , e , o },
            { o, e , o , e , e , e , o , e , o },
            { e, e , e , e , x , e , e , e , e },
            { o, e , o , e , e , e , o , e , o },
            { o, e , e , e , e , e , e , e , o },
            { o, o , o , o , e , o , o , o , o },
        };
        return topSideRoom;
    }
}