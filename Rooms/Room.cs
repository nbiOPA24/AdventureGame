public class Room
{
    public string RoomName {get;set;}
    public bool RoomState {get;set;} 
    public string RoomIcon {get; set;}

    public Room(string roomName, string roomIcon)
    {
        RoomName = roomName;
        RoomIcon = roomIcon;
        RoomState = false;
    }

    public Room()
    {
        RoomName = "Empty Room";
        RoomIcon = "   ";
        RoomState = false;
    }

    public virtual void RunRoom(Player player)
    {

    }
}


interface IRoom
{
    void RunRoom();
}