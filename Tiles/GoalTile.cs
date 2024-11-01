public class GoalTile : Tile
{
    public GoalTile() : base("Goal tile", " ⚑ ")
    {
    }
    public override void RunTile(Character player)
    {
        if (TileState == false)
        {
            Console.WriteLine("Congratulations, adventurer! You've reached your goal! Take a moment to celebrate your victory. [✓]");
            TileState = true;
            // Avsluta spelet? Return to mainMenu med en Thread.Sleep?
        }
        else
        {
            Console.WriteLine("You've already reached the goal. The adventure is complete, but you can always revisit for nostalgia.");
        }
    }
}