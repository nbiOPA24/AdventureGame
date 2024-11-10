public class GR : Room
{
    public Tile[,] Room {get;set;}
    public GR()
    {
        Room = GenerateRoom();
    }
    public Tile[,] GenerateRoom()
    {
        Tile s = new StarterTile();
        Tile e = new EmptyTile();
        Tile o = new ObstacleTile();
        Tile d = new DoorTile();
        Tile m = new MysteryTile(10);
        Tile g = new GoalTile();
        Tile x = new EnemyTile("Enemy", 10, 3);


        Tile[,] goalRoom = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , o , o },
            { e, e , e , e , m , e , e , e , g },    
            { o, e , e , e , e , e , e , o , o },   
            { o, e , e , e , e , e , e , e , e },
            { o, e , e , e , e , e , e , e , e }

        };
        
        return goalRoom;
    }
}