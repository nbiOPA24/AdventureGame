public class Room
{
    public string RoomName {get;set;} = string.Empty;
    public virtual bool RoomState {get;set;} = false;
    public virtual string RoomIcon {get; set;} = "[ ]";

    public Room(string roomName)
    {
        RoomName = roomName;
    }
    public Room()
    { 
    }


}