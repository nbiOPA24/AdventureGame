public class Tile
{
    public string Name {get;set;}
    public bool IsVisited {get;set;} 
    public bool RemoveTile {get;set;}
    public string Icon {get; set;}
    public string EmptyIcon {get; set;}
    public bool Solid {get;set;}
    public ConsoleColor Color {get; set;}
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
        RemoveTile = false;
        EmptyIcon = "   ";
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