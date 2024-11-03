public class TileKey : Tile
{
    public TileKey() : base("Key tile", " ⚿ ")
    {
    }
    public override void RunTile(Character player)
    {
        if (TileState == false)
        {
            Console.WriteLine("Congratulations, adventurer! You get a key to your inventory!");
            player.Inventory.Items.Add(new Item("DoorKey", "A mysterious key... What can it be for..?"));
            TileState = true;
        }
        else
        {
            Console.WriteLine("You've already gotten the key. But you can always revisit for nostalgia.");
        }
    }
}