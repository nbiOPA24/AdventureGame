class PuzzleRoom : RewardRoom
{
    public override string RoomIcon {get; set;} = "[⌘]";

    public PuzzleRoom (string roomName) : base(roomName)
    {

    }
}