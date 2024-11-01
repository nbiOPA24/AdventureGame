public class CR : Room
{
    public Tile [,] Room {get;set;}

    public CR()
    {
        Room = GeneratRoom();
    }
    public Tile[,] GeneratRoom()
    {
        Tile s = new StarterTile();
        Tile e = new Tile();
        Tile o = new ObstacleTile();


        Tile[,] crossRoom = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { e, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e }

        };
        return crossRoom;
    }
}