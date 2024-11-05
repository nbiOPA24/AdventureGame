class MapHandler
{
    public static Character PlayerStartPos(Character player, Tile[,] arrayWithRooms)
    {
        for(int i = 0; i < arrayWithRooms.GetLength(0); i++)
        {
            for(int j = 0; j < arrayWithRooms.GetLength(1); j++)
            {
                if (arrayWithRooms[i,j].TileName == "Start")
                {
                    player.YPos = i;
                    player.XPos = j;
                    Console.WriteLine($"{player.YPos}, {player.XPos}");

                }
            }
        }
        return player;
    }

   public static void DrawMap(Character player, Tile[,] arrayWithRooms)
    {
        for (int i = 0; i < arrayWithRooms.GetLength(0); i++)
        {
            for (int j = 0; j < arrayWithRooms.GetLength(1); j++)
            {
                if (i == player.YPos && j == player.XPos)
                {
                    Console.Write(" ☻ ");
                }

                else
                {
                    Console.Write(arrayWithRooms[i,j].TileIcon);   
                }
            }
            Console.WriteLine();
        }
    }
    // Movement i förhållande till en Arrays kanter och om Tile är Solid.
    public static void MovePlayer(Character player, Tile[,] arrayWithRooms)
    {
        ConsoleKeyInfo movement = Console.ReadKey(true);
        if (movement.Key == ConsoleKey.UpArrow)
        {
            if(!arrayWithRooms[player.YPos - 1, player.XPos].Solid)
            {
                player.YPos--;
            }
            else
            {
                arrayWithRooms[player.YPos - 1, player.XPos].RunSolidBlock(player);
            }
        }
        if (movement.Key == ConsoleKey.DownArrow && !arrayWithRooms[player.YPos + 1, player.XPos].Solid)
        {
            player.YPos++;
        }
        if (movement.Key == ConsoleKey.LeftArrow && !arrayWithRooms[player.YPos, player.XPos - 1].Solid)
        {
            player.XPos--;
        }
        if (movement.Key == ConsoleKey.RightArrow && !arrayWithRooms[player.YPos, player.XPos + 1].Solid)
        {
            player.XPos++;
        }
        if (new List<ConsoleKey>() {ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow}.Contains(movement.Key)) {

        } 
    }

    public static void ActivateRoom(Character player, Tile[,] arrayWithRooms)
    {
        arrayWithRooms[player.YPos,player.XPos].RunTile(player);
    }

   /*  public static void RunEntireMap(Character player, Tile[,] arrayWithRooms)
    {
        player = PlayerStartPos(player, arrayWithRooms);
        while (player.CurrentHealth > 0)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-8} {"Race",-8} {"HP",-5} {"Damage",-7} {"Armor",-6} {"Cords",-7} {"Keys"}");
            Console.WriteLine($"{player.Name,-8} {player.Race,-8} {player.CurrentHealth,-5} {player.BaseDamage,-7} {player.Armor,-6} [{player.YPos},{player.XPos} {player.Inventory.Items.Count}]");
            DrawMap(player, arrayWithRooms);
            ActivateRoom(player,arrayWithRooms);
           
            MovePlayer(player, arrayWithRooms);

        }
    } */

    public static void RunEntireMap(Character player, int rows, int cols)
    {
        Tile[,] map = MapFactory.FlattenRoomArray(rows, cols);

        player = PlayerStartPos(player, map);
        
        while (player.CurrentHealth > 0)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-8} {"Race",-8} {"HP",-5} {"Damage",-7} {"Armor",-6} {"Cords",-7} {"Keys"}");
            Console.WriteLine($"{player.Name,-8} {player.Race,-8} {player.CurrentHealth,-5} {player.BaseDamage,-7} {player.Armor,-6} [{player.YPos},{player.XPos + "]", -7} {player.Inventory.Items.Count}");
            DrawMap(player, map);       // Ritar ut kartan i en forloop och skriver över med en spelarikon där spelarens y och x pos är.
            ActivateRoom(player,map);   // Kör den aktuella tile som spelaren står på med RunTile().
            MovePlayer(player, map);    // Skapar möjlighet för spelaren att göra förflyttning.

        }
    }



    
}