public class ObstacleRoom : Room
{
    public override string RoomIcon {get; set;} = "[█]";

    public ObstacleRoom(string roomName) : base(roomName)
    {
    }
    public ObstacleRoom()
    {
    }
}