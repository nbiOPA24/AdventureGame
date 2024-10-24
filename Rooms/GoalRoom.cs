public class GoalRoom : Room
{
    public GoalRoom() : base("Goal room", "[⚑]")
    {
    }
    public override void RunRoom(Player player)
    {
        if (RoomState == false)
        {
            Console.WriteLine("Congratulations, adventurer! You've reached your goal! Take a moment to celebrate your victory. [✓]");
            RoomState = true;
            // Avsluta spelet? Return to mainMenu med en Thread.Sleep?
        }
        else
        {
            Console.WriteLine("You've already reached the goal. The adventure is complete, but you can always revisit for nostalgia.");
        }
    }
}