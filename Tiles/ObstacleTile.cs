public class ObstacleTile : Tile
{
    public ObstacleTile() : base("ObstacleRoom", "███")
    {
        Solid = true;
        Color = ConsoleColor.DarkGray;
    }
}