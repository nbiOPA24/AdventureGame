public class EmptyTile : Tile
{

    public EmptyTile(string tileName, string tileIcon)
    {
        Name = tileName;
        Icon = tileIcon;
        IsVisited = false;
        Solid = false;
    }
    public EmptyTile()
    {
        Name = "Empty Tile";
        Icon = "   ";
        IsVisited = false;
        Solid = false;
        RemoveTile = false;
    }

}
