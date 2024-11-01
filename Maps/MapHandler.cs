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

    public static void MovePlayer(Character player, Tile[,] arrayWithRooms)
    {
        ConsoleKeyInfo movement = Console.ReadKey(true);
        if (movement.Key == ConsoleKey.UpArrow && player.YPos > 0 && arrayWithRooms[player.YPos - 1, player.XPos].TileIcon != "███")
        {
            player.YPos--;
        }
        if (movement.Key == ConsoleKey.DownArrow && player.YPos < arrayWithRooms.GetLength(0)-1 && arrayWithRooms[player.YPos + 1, player.XPos].TileIcon != "███")
        {
            player.YPos++;
        }
        if (movement.Key == ConsoleKey.LeftArrow && player.XPos > 0 && arrayWithRooms[player.YPos, player.XPos - 1].TileIcon != "███")
        {
            player.XPos--;
        }
        if (movement.Key == ConsoleKey.RightArrow && player.XPos < arrayWithRooms.GetLength(1)-1 && arrayWithRooms[player.YPos, player.XPos + 1].TileIcon != "███")
        {
            player.XPos++;
        }
    }

    public static void ActivateRoom(Character player, Tile[,] arrayWithRooms)
    {
        arrayWithRooms[player.YPos,player.XPos].RunTile(player);
    }

    public static void RunEntireMap(Character player, Tile[,] arrayWithRooms)
    {
        //Console.CursorVisible = false;
        while (player.CurrentHealth > 0)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-8} {"Race",-8} {"HP",-5} {"Damage",-7} {"Armor",-6} {"Cords",-7}");
            Console.WriteLine($"{player.Name,-8} {player.Race,-8} {player.CurrentHealth,-5} {player.BaseDamage,-7} {player.Armor,-6} [{player.YPos},{player.XPos}]");
            DrawMap(player, arrayWithRooms);
            ActivateRoom(player,arrayWithRooms);
            MovePlayer(player, arrayWithRooms);

        }
    }



    
}