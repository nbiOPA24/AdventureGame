public static class MainMap
{
    public static void map()
    {
        Room e = new Room();
        Room o = new ObstacleRoom();
        Room s = new StarterRoom();
        Room g = new GoalRoom();

        #region EnemyRooms
        Room x1 = new EnemyRoom("Goblin's Den");
        Room x2 = new EnemyRoom("Orc's Lair");
        Room x3 = new EnemyRoom("Troll's Cave");
        Room x4 = new EnemyRoom("Dragon's Nest");
        Room x5 = new EnemyRoom("Bandit's Hideout");
        #endregion

        #region MysterRooms
        Room m1 = new MysteryRoom();
        Room m2 = new MysteryRoom();
        Room m3 = new MysteryRoom();
        Room m4 = new MysteryRoom();
        Room m5 = new MysteryRoom();
        Room m6 = new MysteryRoom();
        Room m7 = new MysteryRoom();
        Room m8 = new MysteryRoom();
        Room m9 = new MysteryRoom();
        #endregion

        #region PuzzleRooms
        Room p1 = new PuzzleRoom("Crypt of the Enigmatic Pharaoh");
        Room p2 = new PuzzleRoom("Hall of the Riddling Sphinx");
        Room p3 = new PuzzleRoom("Chamber of Shifting Runes");
        Room p4 = new PuzzleRoom("Vault of the Arcane Cipher");
        Room p5 = new PuzzleRoom("Labyrinth of the Forgotten Glyphs");
        Room p6 = new PuzzleRoom("Sanctum of the Whispering Stones");
        Room p7 = new PuzzleRoom("Shrine of the Celestial Puzzle");
        Room p8 = new PuzzleRoom("Altar of the Twisted Mind");
        Room p9 = new PuzzleRoom("Maze of the Eternal Paradox");
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

        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    Console.Write(rooms[i,j].RoomIcon);   
                }
                Console.WriteLine();
        }
    }

}