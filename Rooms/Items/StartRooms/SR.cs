public class SR : Room
{
    public Tile[,] Room {get;set;}
    public SR()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile s = new StarterTile();
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile d = new DoorTile();
        Tile m = new MysteryTile(10);


        Tile[,] starterRoom = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { o, s , e , e , m , e , e , e , e },    
            { o, e , e , e , e , e , e , e , o },   
            { o, e , e , e , e , e , e , e , o },
            { o, o , o , o , e , o , o , o , o }

        };
        
        return starterRoom;
    }
}