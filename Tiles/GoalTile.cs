public class GoalTile : Tile
{
    public GoalTile() : base("Goal tile", " ⚑ ")
    {
    }
    public override void RunTile(List<Character> playerList)
    {
        
        if (TileState == false)
        {
            Console.Clear();

            // DND ending text
            string dndEndingText = @"After a long and perilous journey, you have finally reached your goal.
    Treasures beyond your wildest dreams lie before you.
    But the adventure is not over yet...
    New realms await exploration, and legends are waiting to be forged.

    Thank you for playing!";

            // Display the text character by character
            Utilities.CharByChar(dndEndingText, 15, ConsoleColor.DarkGreen);
            Console.WriteLine("RETURNING TO MAIN MENU????????????????");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}