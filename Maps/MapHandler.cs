class MapHandler
{
    public static Player PlayerStartPos(Player player, Room[,] arrayWithRooms)
    {
        for(int i = 0; i < arrayWithRooms.GetLength(0); i++)
        {
            for(int j = 0; j < arrayWithRooms.GetLength(1); j++)
            {
                if (arrayWithRooms[i,j].RoomName == "Start")
                {
                    player.YPos = i;
                    player.XPos = j;
                    Console.WriteLine($"{player.YPos}, {player.XPos}");

                }
            }
        }
        return player;
    }


    
}