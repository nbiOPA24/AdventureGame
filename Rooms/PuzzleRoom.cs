class PuzzleRoom : RewardRoom
{

    public PuzzleRoom (string roomName, int reward) : base(roomName, "[⌘]", reward)
    {
    }
    public override void RunRoom()
    {
        if (RoomState == false)
        {
            // Puzzle Logic

            Console.WriteLine("You enter a room filled with mystery. To proceed, you must solve the puzzle that lies before you. Think carefully, adventurer, for the path forward depends on your wits.");
            RoomState = true;
        }
        else
        {
            Console.WriteLine("The puzzle has already been solved. The room feels quieter now, as if waiting for the next adventurer to test their mind.");
        }
    }
}