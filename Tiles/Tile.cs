public class Tile
{
    public string TileName {get;set;}
    public bool TileState {get;set;} 
    public string TileIcon {get; set;}

    public Tile(string tileName, string tileIcon)
    {
        TileName = tileName;
        TileIcon = tileIcon;
        TileState = false;
    }

    public Tile()
    {
        TileName = "Empty Tile";
        TileIcon = "   ";
        TileState = false;
    }

    public virtual void RunTile(List<Character> playerList)
    {

    }
}


interface IRoom
{
    void RunTile();
}