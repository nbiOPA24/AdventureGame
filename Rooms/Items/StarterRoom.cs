public class StarterRoom : Room
{
    public Tile[,] StartRoom {get;set;}
    public StarterRoom()
    {
        StartRoom = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile s = new StarterTile();
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile d = new DoorTile();


        Tile[,] starterRoom = new Tile [,]
        {
            { o, o , o , o , d , o , o , o , o },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { d, s , e , e , e , e , e , e , d },  // d2 
            { o, e , e , e , e , e , e , e , o },   //
            { o, e , e , e , e , e , e , e , o },
            { o, o , o , o , d , o , o , o , o }

        };
        
        return starterRoom;
    }
}