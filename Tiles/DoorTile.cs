public class DoorTile : Tile
{
    public DoorTile() : base("Door tile", " ≡ ")
    {
        Solid = true;
        Tile KeyTile = new KeyTile();
        Color = ConsoleColor.DarkGray;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        Character player = playerList[0];
        if (!IsVisited)
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
                /* Console.WriteLine("Do you wish to open the door? [Y] or [N]");
                string reply = Utilities.ValidateString();
                if (reply.ToLower() == "y")
                { */
                    Solid = false;   
                    Console.WriteLine("The door opens...");
                    for (int i = 0; i < player.Inventory.Items.Count; i++)
                    {
                        if(player.Inventory.Items[i].Name.ToLower().Contains("key"))
                        {
                            player.Inventory.Items.RemoveAt(i);
                            break;
                        }

                    }
                    IsVisited = true;
                    RemoveTile = true;
                /* } */
            }
            else
            {
                Console.WriteLine("Sorry, The door i locked!");
            }
        }
    }
}