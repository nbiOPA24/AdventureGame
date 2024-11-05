class MapHandler
{
    public static Character PlayerStartPos(Character player, Tile[,] mapArray)
    {
        for(int i = 0; i < mapArray.GetLength(0); i++)
        {
            for(int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (mapArray[i,j].TileName == "Start")
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
                    Console.Write(" ☻ ");
                }

                else
                {
                    Console.Write(mapArray[i,j].TileIcon);   
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
        if (control.Key == ConsoleKey.UpArrow)
        {
            if (!mapArray[player.YPos - 1, player.XPos].Solid)
            {
                player.YPos--;
            }
            else
            {
                mapArray[player.YPos - 1, player.XPos].RunSolidTile(playerList);
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.DownArrow)
        {
            if (!mapArray[player.YPos + 1, player.XPos].Solid)
            {
                player.YPos++;
            }
            else
            {
                mapArray[player.YPos + 1, player.XPos].RunSolidTile(playerList);
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.LeftArrow)
        {
            if (!mapArray[player.YPos, player.XPos - 1].Solid)
            {
                player.XPos--;
            }
            else
            {
                mapArray[player.YPos, player.XPos - 1].RunSolidTile(playerList);
                MovePlayer(playerList, mapArray);
            }
        }
        else if (control.Key == ConsoleKey.RightArrow)
        {
            if (!mapArray[player.YPos, player.XPos + 1].Solid)
            {
                player.XPos++;
            }
            else
            {
                mapArray[player.YPos, player.XPos + 1].RunSolidTile(playerList);
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
            Console.WriteLine($"{"Name",-8} {"HP",-5} {"Damage",-7} {"Armor",-6} {"Cords",-7} {"Inventory Items"}");
            Console.WriteLine($"{player.Name,-8} {player.CurrentHealth,-5} {player.BaseDamage,-7} {player.Armor,-6} [{player.YPos},{player.XPos + "]", -7} {player.Inventory.Items.Count}");
            
            DrawMap(player, map);       // Ritar ut kartan i en forloop och skriver över med en spelarikon där spelarens y och x pos är.

            map[player.YPos,player.XPos].RunTile(playerList);// Kör den aktuella tile som spelaren står på med RunTile().

            MovePlayer(playerList, map);    // Skapar möjlighet för spelaren att göra förflyttning.

        }
    }



    
}