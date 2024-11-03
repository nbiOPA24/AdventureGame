public class CR : Room
{
    public Tile [,] Room {get;set;}

    public CR()
    {
        Room = GeneratRandomRoom();
    }
    public Tile[,] GeneratRandomRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile d = new DoorTile();
        Tile k = new TileKey();

        Tile[,] crossRoom = new Tile[,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { e, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomTwo = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { e, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }            
        };

        Tile[,] crossRoomThree = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, e , e , e , o , e , e , e , e },
            { o, e , e , e , o , e , e , e , e },
            { o, e , e , e , o , e , e , e , e },
            { o, o , d , o , o , e , e , e , e },
            { e, e , e , e , e , e , o , k , e },
            { o, e , e , e , e , e , e , e , e }
        };

        
        List<Tile[,]> rooms = [crossRoom, crossRoomTwo, crossRoomThree];

        Random random = new Random();
        
        int rndResult = random.Next(0,rooms.Count);
        
        return rooms[rndResult];
    }
}