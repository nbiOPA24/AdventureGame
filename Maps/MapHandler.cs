class MapHandler
{
    public static Character PlayerStartPos(Character player, Tile[,] mapArray)
    {
        for(int i = 0; i < mapArray.GetLength(0); i++)
        {
            for(int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (mapArray[i,j].Name == "Start")
                {
                    player.YPos = i;
                    player.XPos = j;
                    Console.WriteLine($"{player.YPos}, {player.XPos}");

                }
            }
        }
        return player;
    }

   public static void DrawMap(Character player, Tile[,] mapArray)
    {
        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (i == player.YPos && j == player.XPos)
                {
                    Utilities.ConsoleWriteColor(" ☻ ", ConsoleColor.DarkMagenta);
                }
                else
                {
                    Utilities.ConsoleWriteColor(mapArray[i,j].Icon, mapArray[i,j].Color);   
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    public static void DrawMiniMap(Character player, Tile[,] mapArray)
    {
        for(int row = 0; row < mapArray.GetLength(0); row++)
        {
            for(int col = 0; col < mapArray.GetLength(1); col++)
            {
                if (row == player.YPos && col == player.XPos)
                {
                    Utilities.ConsoleWriteColor(" ☻ ", ConsoleColor.DarkMagenta);
                }
                else if (row >= player.YPos -5 && row <= player.YPos +5 && col >= player.XPos -5 && col <= player.XPos +5)  
                {
                    Utilities.ConsoleWriteColor(mapArray[row,col].Icon, mapArray[row,col].Color);
                }
            }
            Console.WriteLine();
        }

    }


    // Movement i förhållande till en Arrays kanter och om Tile är Solid.
    public static void MovePlayer(List<Character> playerList, Tile[,] mapArray)
    {
        Character player = playerList[0];
        ConsoleKeyInfo control = Console.ReadKey(true);
        if (control.Key == ConsoleKey.UpArrow || control.Key == ConsoleKey.W)
        {
            if (!mapArray[player.YPos - 1, player.XPos].Solid)
            {
                player.YPos--;
            }
            else
            {
                mapArray[player.YPos - 1, player.XPos].RunSolidTile(playerList);
                if(mapArray[player.YPos - 1,player.XPos].RemoveTile == true)
                    mapArray[player.YPos - 1,player.XPos] = new EmptyTile();
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.DownArrow || control.Key == ConsoleKey.S)
        {
            if (!mapArray[player.YPos + 1, player.XPos].Solid)
            {
                player.YPos++;
            }
            else
            {
                mapArray[player.YPos + 1, player.XPos].RunSolidTile(playerList);
                if(mapArray[player.YPos + 1,player.XPos].RemoveTile == true)
                    mapArray[player.YPos + 1,player.XPos] = new EmptyTile();
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.LeftArrow || control.Key == ConsoleKey.A)
        {
            if (!mapArray[player.YPos, player.XPos - 1].Solid)
            {
                player.XPos--;
            }
            else
            {
                mapArray[player.YPos, player.XPos - 1].RunSolidTile(playerList);
                if(mapArray[player.YPos,player.XPos - 1].RemoveTile == true)
                    mapArray[player.YPos,player.XPos - 1] = new EmptyTile();
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.RightArrow || control.Key == ConsoleKey.D)
        {
            if (!mapArray[player.YPos, player.XPos + 1].Solid)
            {
                player.XPos++;
            }
            else
            {
                mapArray[player.YPos, player.XPos + 1].RunSolidTile(playerList);
                if(mapArray[player.YPos,player.XPos + 1].RemoveTile == true)
                    mapArray[player.YPos,player.XPos + 1] = new EmptyTile();
                MovePlayer(playerList, mapArray);
            }
        }
    }


    public static void RunEntireMap(List<Character> playerList, int rows, int cols)
    {
        Character player = playerList[0];
        Tile[,] map = MapFactory.GenerateMap(rows, cols);

        player = PlayerStartPos(player, map);
        
        while (player.CurrentHealth > 0)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-8} {"HP",-5} {"Max HP", -5} {"Power",-7} {"Armor",-6} {"Cords",-7} {"Inventory Items"}");
            Console.WriteLine($"{player.Name,-8} {player.CurrentHealth,-5} {player.MaxHealth,-5} {player.Power,-7} {player.Armor,-6} [{player.YPos},{player.XPos + "]", -7} {player.Inventory.Items.Count}");
            
            DrawMiniMap(player, map);       // Ritar ut kartan i en forloop och skriver över med en spelarikon där spelarens y och x pos är.

            map[player.YPos,player.XPos].RunTile(playerList);// Kör den aktuella tile som spelaren står på med RunTile().
            if(map[player.YPos,player.XPos].RemoveTile == true)
                map[player.YPos,player.XPos] = new EmptyTile(); 

            MovePlayer(playerList, map);    // Skapar möjlighet för spelaren att göra förflyttning.
        }
    }
}