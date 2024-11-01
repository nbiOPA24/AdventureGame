public class MainMap
{
    public static Tile[,][,] GenerateStaticMap()
    {
        // TLCE = Top Left Corner Easy

        Tile[,][,] map = new Tile[,][,]
        {
            {new CRE().Room,   new CRE().Room, new CRE().Room, new CRE().Room   },
            {new SR().Room,    new CRE().Room, new CR().Room,  new CRE().Room   },
            {new CRE().Room,   new CRE().Room, new CRE().Room, new CRE().Room   },
            {new CRE().Room,   new CRE().Room, new CRE().Room, new CRE().Room   },
        };
        return map;
    }
}