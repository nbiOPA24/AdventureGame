public class GoalRoom : Room
{
    public override string RoomIcon {get; set;} = "[⚑]";

    public GoalRoom(string roomName) : base(roomName)
    {
    }
    public GoalRoom()
    {
    }
}