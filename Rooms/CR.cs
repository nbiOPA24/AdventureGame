public class CR : Room  
{
    public Tile [,] Room {get;set;}

    public CR()
    {
        Room = GeneratRandomRoom();
    }
    public Tile[,] GeneratRandomRoom()
    {
        Tile e = new EmptyTile();
        Tile o = new ObstacleTile();
        Tile d = new DoorTile();
        Tile x = new EnemyTile("Dragon Lair", 10, 3);
        Tile m = new MysteryTile(20);
        Tile r = new ItemRewardTile();
        Tile g = new MiniGame();
        Tile l = new LogicTile();
        Tile s = new SnakeTile();


        Tile[,] crossRoom = new Tile[,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , g , e },
            { o, e , o , o , d , o , o , e , e },
            { e, e , o , x , e , e , o , s , e },
            { o, e , o , e , e , l , o , e , e },
            { o, e , o , o , o , o , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomTwo = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, m , e , e , e , e , e , e , e },
            { o, e , e , s , e , e , e , e , e },
            { o, e , o , o , o , o , o , o , e },
            { o, e , x , e , e , e , e , e , e },
            { o, o , o , o , o , o , o , o , e },
            { e, e , e , e , e , l , e , e , e }            
        };

        Tile[,] crossRoomThree = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, m , e , e , o , e , e , e , e },
            { o, e , e , g , o , e , e , e , e },
            { o, e , e , e , o , e , e , e , e },
            { o, o , d , o , o , e , e , e , e },
            { l, e , e , e , s , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomFour = new Tile[,]
        {
            { o, o , o , o , o , l , o , o , o },
            { o, e , e , d , e , e , e , e , e },
            { o, e , e , o , e , e , e , e , e },
            { o, x , e , o , e , e , e , e , e },
            { o, o , o , o , e , e , e , e , e },
            { e, e , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomFive = new Tile[,]
        {
            { o, o , o , e , o , o , o , o , o },
            { o, e , e , e , o , e , e , o , e },
            { o, e , e , s , d , e , x , o , e },
            { o, e , e , e , o , e , e , o , e },
            { o, e , e , e , o , o , o , o , e },
            { e, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e }
        };

        Tile[,] crossRoomSix = new Tile[,]
        {
            { o, o , o , o , o , e , o , o , o },
            { o, e , e , o , e , e , e , e , e },
            { o, e , e , o , e , e , x , e , e },
            { o, r , e , o , e , o , o , o , e },
            { o, o , d , o , e , d , e , o , e },
            { e, e , l , e , e , o , o , o , e },
            { o, e , e , e , e , e , e , e , e }
        };
        Tile[,] crossRoomSeven = new Tile[,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , o , e , e , e , o , m , e },
            { e, e , e , x , o , e , e , e , e },
            { o, e , o , e , e , e , e , o , e },
            { o, g , e , e , e , e , o , e , e },
            { o, e , e , e , e , e , e , e , e }
        };
        
        List<Tile[,]> rooms = [crossRoom, crossRoomTwo, crossRoomThree, crossRoomFour, crossRoomFive, crossRoomSix, crossRoomSeven];

        Random random = new Random();
        
        int rndResult = random.Next(0,rooms.Count);
        
        return rooms[rndResult];
    }
}