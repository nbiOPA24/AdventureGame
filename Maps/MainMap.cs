public static class MainMap
{
    public static Room[,] GenerateMap()
    {
        Room e = new Room();
        Room o = new ObstacleRoom();
        Room s = new StarterRoom();
        Room g = new GoalRoom();

        #region EnemyRooms
        Room x1 = new EnemyRoom("Goblin's Den", 10, 3, DifficultyLevel.Hard, "goblin");
        Room x2 = new EnemyRoom("Orc's Lair", 10, 3, DifficultyLevel.Easy, "goblin");
        Room x3 = new EnemyRoom("Troll's Cave", 10, 3, DifficultyLevel.Easy, "goblin");
        Room x4 = new EnemyRoom("Dragon's Nest", 10, 3, DifficultyLevel.Easy, "goblin");
        Room x5 = new EnemyRoom("Bandit's Hideout", 10, 3, DifficultyLevel.Easy, "goblin");
        #endregion

        /* 
            listwithrooms.txt/Json
            Loopar igenom arrayen.
            If (X)
                Loopar igenom filen.
            Namn:Reward
            split(":")
            Målet 
                new EnemyRoom()
        */
        #region MysterRooms
        Room m1 = new MysteryRoom("hej",10);
        Room m2 = new MysteryRoom("hej",10);
        Room m3 = new MysteryRoom("hej",10);
        Room m4 = new MysteryRoom("hej",10);
        Room m5 = new MysteryRoom("hej",10);
        Room m6 = new MysteryRoom("hej",10);
        Room m7 = new MysteryRoom("hej",10);
        Room m8 = new MysteryRoom("hej",10);
        Room m9 = new MysteryRoom("hej",10);
        #endregion

        #region PuzzleRooms
        Room p1 = new PuzzleRoom("Chamber of Calculations", 10, 
            "What is the value of 7 multiplied by 6?", 
            "42");
        Room p2 = new PuzzleRoom("Hall of Equations", 10, 
            "If x + 3 = 10, what is the value of x?", 
            "7");
        Room p3 = new PuzzleRoom("Room of Fractions", 10, 
            "What is 1/2 plus 1/3 in simplest form?", 
            "5/6");
        Room p4 = new PuzzleRoom("Crypt of Squares", 10, 
            "What is the square of 12?", 
            "144");
        Room p5 = new PuzzleRoom("Algebraic Antechamber", 10, 
            "Solve for y: 2y - 4 = 10", 
            "7");
        Room p6 = new PuzzleRoom("Geometry Gallery", 10, 
            "What is the area of a circle with radius 5? (Use π = 3.14)", 
            "78.5");
        Room p7 = new PuzzleRoom("Prime Passage", 10, 
            "What is the smallest prime number greater than 20?", 
            "23");
        Room p8 = new PuzzleRoom("Maze of Multiples", 10, 
            "What is the least common multiple of 4 and 6?", 
            "12");
        Room p9 = new PuzzleRoom("Division Den", 10, 
            "What is 81 divided by 9?", 
            "9");

        #endregion

        
        Room[,] rooms = new Room[9, 15]
        {
            { o, o , o , o , o , o , o , o , o , o , o , o , o , o , o },
            { o, x2, e , p3, e , e , p5, e , e , e , x5, e , e , x4, o },
            { o, e , o , e , o , e , o , e , o , o , o , e , o , e , o },
            { o, p1, o , x3, e , e , o , p8, e , e , e , e , e , p9, o },
            { s, e , m5, o , o , o , o , o , o , o , o , o , o , x1, g },
            { o, p2, o , m2, e , e , o , p7, e , e , e , e , e , e , o },
            { o, e , o , e , o , e , o , e , o , o , o , e , o , e , o },
            { o, m1, e , p4, e , e , p6, e , e , e , m3, e , e , m4, o },
            { o, o , o , o , o , o , o , o , o , o , o , o , o , o , o },
        };

        return rooms;
    }




    public static void DrawMap(Player player, Room[,] rooms)
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                if (i == player.YPos && j == player.XPos)
                {
                    Console.Write(" ☻ ");
                }

                else
                {
                    Console.Write(rooms[i,j].RoomIcon);   
                }
            }
            Console.WriteLine();
        }
    }

    public static void MovePlayer(Player player, Room[,] arrayWithRooms)
    {
        ConsoleKeyInfo movement = Console.ReadKey(true);

        if (movement.Key == ConsoleKey.UpArrow && player.YPos > 0 && arrayWithRooms[player.YPos - 1, player.XPos].RoomIcon != "███")
        {
            player.YPos--;
        }

        if (movement.Key == ConsoleKey.DownArrow && player.YPos < arrayWithRooms.GetLength(0)-1 && arrayWithRooms[player.YPos + 1, player.XPos].RoomIcon != "███")
        {
            player.YPos++;
        }

        if (movement.Key == ConsoleKey.LeftArrow && player.XPos > 0 && arrayWithRooms[player.YPos, player.XPos - 1].RoomIcon != "███")
        {
            player.XPos--;
        }

        if (movement.Key == ConsoleKey.RightArrow && player.XPos < arrayWithRooms.GetLength(1)-1 && arrayWithRooms[player.YPos, player.XPos + 1].RoomIcon != "███")
        {
            player.XPos++;
        }

    }

    public static void ActivateRoom(Player player, Room[,] arrayWithRooms)
    {
        arrayWithRooms[player.YPos,player.XPos].RunRoom(player);
    }

    public static void RunEntireMap(Player player, Room[,] arrayWithRooms)
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