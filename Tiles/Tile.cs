public class Tile
{
    public string Name {get;set;}
    public bool IsVisited {get;set;} 
    public string Icon {get; set;}
    public bool Solid {get;set;}
    public Tile(string tileName, string tileIcon)
    {
        Name = tileName;
        Icon = tileIcon;
        IsVisited = false;
        Solid = false;
    }
    public Tile()
    {
        Name = "Empty Tile";
        Icon = "   ";
        IsVisited = false;
        Solid = false;
    }

    public virtual void RunTile(List<Character> playerList)
    {

    }

    public virtual void RunSolidTile(List<Character> playerList)
    {

    }
}


interface IRoom
{
    void RunTile();
}