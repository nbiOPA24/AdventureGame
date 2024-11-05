public class DoorTile : Tile
{
    public DoorTile() : base("Door tile", " ≡ ")
    {
        Solid = true;
    }
    public override void RunSolidTile(Character player)
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
                    for (int i = 0; i < player.Inventory.Items.Count; i++)
                    {
                        if(player.Inventory.Items[i].Name.ToLower().Contains("key"))
                        {
                            player.Inventory.Items.RemoveAt(i);
                        }
                    }
                    TileState = true;
                }
            }
            else
            {
                Console.WriteLine("Sorry, The door i locked!");
            }
        }
    }
}