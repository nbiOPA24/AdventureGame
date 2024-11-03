public class DoorTile : Tile
{
    public DoorTile() : base("Door tile", " ≡ ")
    {
    }
    public override void RunTile(Character player)
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
                else
                {
                    hasKey = false;
                }
            }

            if(hasKey)
            {
                Console.WriteLine("Do you wish to open the door? [Y] or [N]");
                string reply = Utilities.ValidateString();
                if (reply.ToLower() == "y")
                {   
                    Console.WriteLine("The door opens...");
                }
                TileState = true;
            }
            else
            {
                Console.WriteLine("Sorry, The door i locked!");
                player.YPos ++;
            }
        }
        else
        {
            Console.WriteLine("The door is open!");
        }
    }
}