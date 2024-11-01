public class MainMap
{
    public static Tile[,][,] GenerateStaticMap()
    {
        // TLCE = Top Left Corner Easy

        Tile[,][,] map = new Tile[,][,]
        {
            {new TLCE().Room,   new TSE().Room, new TSE().Room, new TRCE().Room   },
            {new SR().Room,     new CRE().Room, new CR().Room,  new RSE().Room    },
            {new LSE().Room,    new CRE().Room, new CRE().Room,  new RSE().Room   },
            {new BRCE().Room,   new BSE().Room, new BSE().Room, new BRCE().Room   },
        };
        return map;
    }
}