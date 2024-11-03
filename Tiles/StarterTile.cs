public class StarterTile : Tile
{
    public StarterTile() : base("Start", " S ")
    {
    }

    public override void RunTile(List<Character> playerList)
    {
        if (TileState == false)
        {
            Console.WriteLine("Welcome, adventurer! This is where your journey begins. Take a deep breath and step boldly into the unknown. Good luck!");
            TileState = true;
        }
        else
        {
            Console.WriteLine("You've already been here. The journey awaits beyond, no need to linger.");
        }
    }

}