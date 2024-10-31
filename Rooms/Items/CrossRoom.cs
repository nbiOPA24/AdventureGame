public class CrossRoom : Room
{
    public Tile[,] GenerateRoom()
    {
        Tile s = new StarterTile();
        Tile e = new Tile();
        Tile o = new ObstacleTile();


        Tile[,] crossRoom = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { e, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { o, o , o , o , e , o , o , o , o }

        };
        return crossRoom;
    }
}