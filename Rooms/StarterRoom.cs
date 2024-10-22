public class StarterRoom : Room
{
    public override string RoomIcon {get; set;} = "[S]";

    public StarterRoom(string roomName) : base(roomName)
    {
    }
    public StarterRoom()
    {
    }
}