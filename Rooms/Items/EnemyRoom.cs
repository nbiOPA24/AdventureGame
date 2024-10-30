public class EnemyRoom : Room
{
    public Tile[,] GenerateRoom()
    {
        Tile s = new StarterTile();
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile x = new EnemyTile("Monsters", 10, 3, DifficultyLevel.Easy, "Goblin");


        Tile[,] enemyRoom = new Tile [,]
        {
            { o, o , o , o , e , o , o , o , o },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { e, e , e , e , x , e , e , e , e },
            { o, e , e , e , e , e , e , e , o },
            { o, e , e , e , e , e , e , e , o },
            { o, o , o , o , e , o , o , o , o }

        };
        return enemyRoom;
    }
}