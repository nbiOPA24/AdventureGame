public static class MainRoom
{
    public static Tile[,] GenerateRoom()
    {
        Tile e = new Tile();
        Tile o = new ObstacleTile();
        Tile s = new StarterTile();
        Tile g = new GoalTile();

        #region EnemyRooms
        Tile x1 = new EnemyTile("Goblin's Den", 10, 3, DifficultyLevel.Hard, "goblin");
        Tile x2 = new EnemyTile("Orc's Lair", 10, 3, DifficultyLevel.Easy, "goblin");
        Tile x3 = new EnemyTile("Troll's Cave", 10, 3, DifficultyLevel.Easy, "goblin");
        Tile x4 = new EnemyTile("Dragon's Nest", 10, 3, DifficultyLevel.Easy, "goblin");
        Tile x5 = new EnemyTile("Bandit's Hideout", 10, 3, DifficultyLevel.Easy, "goblin");
        #endregion

        #region MysterRooms
        Tile m1 = new MysteryTile("hej",10);
        Tile m2 = new MysteryTile("hej",10);
        Tile m3 = new MysteryTile("hej",10);
        Tile m4 = new MysteryTile("hej",10);
        Tile m5 = new MysteryTile("hej",10);
        Tile m6 = new MysteryTile("hej",10);
        Tile m7 = new MysteryTile("hej",10);
        Tile m8 = new MysteryTile("hej",10);
        Tile m9 = new MysteryTile("hej",10);
        #endregion

        #region PuzzleRooms
        Tile p1 = new PuzzleTile("Chamber of Calculations", 10, "What is the value of 7 multiplied by 6?", "42");
        Tile p2 = new PuzzleTile("Hall of Equations", 10, "If x + 3 = 10, what is the value of x?", "7");
        Tile p3 = new PuzzleTile("Room of Fractions", 10, "What is 1/2 plus 1/3 in simplest form?", "5/6");
        Tile p4 = new PuzzleTile("Crypt of Squares", 10, "What is the square of 12?", "144");
        Tile p5 = new PuzzleTile("Algebraic Antechamber", 10, "Solve for y: 2y - 4 = 10", "7");
        Tile p6 = new PuzzleTile("Geometry Gallery", 10, "What is the area of a circle with radius 5? (Use π = 3.14)", "78.5");
        Tile p7 = new PuzzleTile("Prime Passage", 10, "What is the smallest prime number greater than 20?", "23");
        Tile p8 = new PuzzleTile("Maze of Multiples", 10, "What is the least common multiple of 4 and 6?", "12");
        Tile p9 = new PuzzleTile("Division Den", 10, "What is 81 divided by 9?", "9");

        #endregion

        
        Tile[,] room = new Tile[9, 15]
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

        return room;
    }

}