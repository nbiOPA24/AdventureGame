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
        Tile x = new EnemyTile("Dragon Lair", 10, 3);
        Tile m = new MysteryTile(20);
        Tile r = new ItemRewardTile();


        Tile[,] crossRoom = new Tile[,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { e, e , e , x , e , e , e , e , e },
            { o, e , o , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomTwo = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, m , e , e , e , e , e , e , e },
            { o, o , o , o , e , e , e , e , e },
            { e, e , e , e , e , e , e , e , e },
            { o, e , x , e , e , e , e , e , e },
            { o, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }            
        };

        Tile[,] crossRoomThree = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, m , e , e , o , e , e , e , e },
            { o, e , e , e , o , e , e , e , e },
            { o, e , e , e , o , e , e , e , e },
            { o, o , d , o , o , e , e , e , e },
            { e, e , e , e , e , e , o , k , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomFour = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, e , e , d , e , e , e , e , e },
            { o, e , e , o , e , e , e , e , e },
            { o, x , e , o , e , e , e , e , e },
            { o, o , o , o , e , e , e , e , e },
            { e, e , e , e , e , e , o , k , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomFive = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, e , e , e , o , e , e , e , e },
            { o, e , k , e , d , e , e , x , e },
            { o, e , e , e , o , e , e , e , e },
            { o, o , o , o , o , e , e , e , e },
            { e, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomSix = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, e , e , e , o , e , e , e , e },
            { o, e , e , e , o , e , x , k , e },
            { o, r , e , e , o , e , e , e , e },
            { o, o , o , d , o , e , e , e , e },
            { e, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };
        Tile[,] crossRoomSeven = new Tile[,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , o , e , e , e , o , m , e },
            { e, e , e , x , o , e , e , e , e },
            { o, e , o , e , e , e , e , o , e },
            { o, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };
        
        List<Tile[,]> rooms = [crossRoom, crossRoomTwo, crossRoomThree, crossRoomFour, crossRoomFive, crossRoomSix, crossRoomSeven];

        Random random = new Random();
        
        int rndResult = random.Next(0,rooms.Count);
        
        return rooms[rndResult];
    }
}