public abstract class Room
{
    public string RoomName {get;set;} = string.Empty;
    public bool RoomState {get;set;} = false;
    public string RoomIcon {get; set;} = "[ ]";

    public Room(string roomName)
    {
        RoomName = roomName;
    }


}