public class CRI : Room
{
    public Tile[,] Room {get; set;}
    public CRI()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile x = new EnemyTile("Monsters", 10, 3, DifficultyLevel.Medium, "Goblin");


        Tile[,] crossRoomIntermediate = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , o , e , e , e , o , e , e },
            { e, e , e , e , x , e , e , e , e },
            { o, e , o , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
        };
        return crossRoomIntermediate;
    }
}