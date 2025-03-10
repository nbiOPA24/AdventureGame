public class StarterTile : Tile
{
    public StarterTile() : base("Start", " S ")
    {
        Color = ConsoleColor.Cyan;
    }

    public override void RunTile(List<Character> playerList)
    {
        if (IsVisited == false)
        {
            Console.WriteLine("Welcome, adventurer! This is where your journey begins. Take a deep breath and step boldly into the unknown. Good luck!");
            IsVisited = true;
        }
        else
        {
            Console.WriteLine("You've already been here. The journey awaits beyond, no need to linger.");
        }
    }
}