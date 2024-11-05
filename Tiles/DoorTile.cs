public class DoorTile : Tile
{
    public DoorTile() : base("Door tile", " ≡ ")
    {
        Solid = true;
    }
    public override void RunSolidBlock(Character player)
    {
        if (TileState == false)
        {
            bool hasKey = false;
            foreach(Item item in player.Inventory.Items)
            {
                if(item.Name.ToLower().Contains("key"))
                {
                    hasKey = true;
                }
            }

            if(hasKey)
            {
                Console.WriteLine("Do you wish to open the door? [Y] or [N]");
                string reply = Utilities.ValidateString();
                if (reply.ToLower() == "y")
                {
                    Solid = false;   
                    Console.WriteLine("The door opens...");
                    foreach(Item item in player.Inventory.Items)
                    {
                        if(item.Name.Contains("key"))
                        {
                            Console.WriteLine($"{item.Name} gets used and removed");
                            player.Inventory.Items.Remove(item);
                            Thread.Sleep(1500);
                        }
                    }
                    TileState = true;
                }
            }
            else
            {
                Console.WriteLine("Sorry, The door i locked!");
                Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("The door is open!");
        }
    }
}