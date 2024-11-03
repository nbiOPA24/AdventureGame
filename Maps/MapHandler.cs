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

    public static void ActivateRoom(List<Character> playerList, Tile[,] arrayWithRooms)
    {
        arrayWithRooms[playerList[0].YPos,playerList[0].XPos].RunTile(playerList);
    }

    public static void RunEntireMap(List<Character> playerList, Tile[,] arrayWithRooms)
    {
        //Console.CursorVisible = false;
        while (playerList[0].CurrentHealth > 0)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-8} {"Race",-8} {"HP",-5} {"Damage",-7} {"Armor",-6} {"Cords",-7}");
            Console.WriteLine($"{playerList[0].Name,-8} {playerList[0].CurrentHealth,-5} {playerList[0].BaseDamage,-7} {playerList[0].Armor,-6} [{playerList[0].YPos},{playerList[0].XPos}]");
            DrawMap(playerList[0], arrayWithRooms);
            ActivateRoom(playerList,arrayWithRooms);
            MovePlayer(playerList[0], arrayWithRooms);

        }
    }



    
}