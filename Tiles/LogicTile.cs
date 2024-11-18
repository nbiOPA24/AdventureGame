class LogicTile : RewardTile
{
    public LogicTile() : base("name", " 🧠", 10)
    {
        Solid = true;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        Character playerName = playerList[0];
        Success = RunGame(playerName);

    }

    private bool RunGame(Character player)
    {

        string[,] memoryBoard = CreateMemoryBoard();

        DrawMemoryBoard(memoryBoard);
        Thread.Sleep(2000);
        int tries = RunMemory(memoryBoard);
        Console.ReadKey(true);
        Solid = false;
        RemoveTile = true;
        if (tries < 12)
        {
            player.CurrentHealth += 999999;
            player.Power += 500;

        }
        else if (tries < 17)
        {
            Console.WriteLine("GREAT JOB!!!, you get 100 hp and 5 power.");
            if (player.CurrentHealth + 100 > player.MaxHealth)
            {
                player.CurrentHealth = player.MaxHealth;
            }
            else
            {
                player.CurrentHealth += 100;
            }
            player.Power += 5;
        }
        else if (tries < 19)
        {
            Console.WriteLine($"Nice one! you get 50 hp and 2 power.");
            if (player.CurrentHealth + 50 > player.MaxHealth)
            {
                player.CurrentHealth = player.MaxHealth;
            }
            else
            {
                player.CurrentHealth += 50;
            }
            player.Power += 2;
        }
        else if (tries < 27)
        {
            player.CurrentHealth += 0;
            player.Armor += 10;
            Console.WriteLine("You stupid, so you get 10 armor to manage life...");
        }
        else if (tries > 27)
        {
            player.CurrentHealth -= 50;
            Console.WriteLine("That was horrible, you loose 50 HP!");
        }


        return false;

    }

    private string[,] CreateMemoryBoard()
    {
        string[,] memoryBoard = new string[9,8];

        List<string> symbols = new List<string>
        {
            "  ⚔  ", // Korsade svärd
            "  ⚔  ", // Korsade svärd
            "  ☠  ", // Dödskalle
            "  ☠  ", // Dödskalle
            "  ⚗  ", // Trolldryck
            "  ⚗  ", // Trolldryck
            "  ⚒  ", // Korsade hammare
            "  ⚒  ", // Korsade hammare
            "  ⚙  ", // Kugghjul
            "  ⚙  ", // Kugghjul
            "  ⚖  ", // Vågar
            "  ⚖  ", // Vågar
            "  ✦  ", // Stjärna
            "  ✦  ", // Stjärna
            "  ✪  ", // Cirkelstjärna
            "  ✪  ", // Cirkelstjärna
            "  ♞  ",  // Schackhäst
            "  ♞  "  // Schackhäst
        };



        for(int row = 0; row < memoryBoard.GetLength(0); row++)
        {
            for(int col = 0; col < memoryBoard.GetLength(1); col++)
            {
                if( row == 0 || row == memoryBoard.GetLength(0)-1 || 
                    col == 0 || col == memoryBoard.GetLength(1)-1)
                {
                    memoryBoard[row,col] = "█████";
                }
                else if(row % 2 != 0)
                {
                    memoryBoard[row,col] = "=====";
                }
                else
                {
                    Random random = new();
                    int index = random.Next(0,symbols.Count-1);
                    memoryBoard[row,col] = symbols[index];
                    symbols.Remove(symbols[index]);
                }

            }
        }
        return memoryBoard;
    }

    private void DrawMemoryBoard(string[,] board)
    {
        for(int row = 0; row < board.GetLength(0); row++)
        {
            for(int col = 0; col < board.GetLength(1); col++)
            {
                Console.Write(board[row,col]);
            }
            Console.WriteLine();
        }
    }
    
    private string[,] CreateHiddenBoard(string[,] board)
    {
        string[,] hiddenBoard = new string[board.GetLength(0),board.GetLength(1)];
        for(int row = 0; row < hiddenBoard.GetLength(0); row++)
        {
            for(int col = 0; col < hiddenBoard.GetLength(1); col++)
            {
                if( row == 0 || row == hiddenBoard.GetLength(0)-1 || 
                    col == 0 || col == hiddenBoard.GetLength(1)-1)
                {
                    hiddenBoard[row,col] = "█████";
                }
                else if(row % 2 != 0)
                {
                    hiddenBoard[row,col] = "=====";
                }
                else
                {
                    hiddenBoard[row,col] = "     ";
                }
            }
        }
        return hiddenBoard;
    }
    private int RunMemory(string[,] board)
    {
        int nrCorrectAnswers = 0;
        int nrTries = 0;
        int markedRow = 2;
        int markedCol = 1;
        string[,] hiddenBoard = CreateHiddenBoard(board);
        string[,] hiddenRefBoard = CreateHiddenBoard(board);

        List<List<int>> savedPos = new();
        List<string> savedString = new();

        while(nrCorrectAnswers < 9)
        {
            Console.Clear();
            Console.WriteLine($"Nr of tries: {nrTries} ");
            Console.WriteLine($"Nr of correct answers: {nrCorrectAnswers}");
            
            for(int row = 0; row < hiddenBoard.GetLength(0); row++)
            {
                for(int col = 0; col < hiddenBoard.GetLength(1); col++)
                {
                    if (row == markedRow && col == markedCol)
                    {
                        if (hiddenRefBoard[row,col] != "     ")
                            hiddenBoard[row,col] = $"[ {hiddenRefBoard[row,col].Trim()} ]";       // Här trycker vi ibland Enter eller förflyttar oss se rad 206
                        else
                            hiddenBoard[row,col] = $"[ {hiddenRefBoard[row,col].Trim()}  ]";       // Här trycker vi ibland Enter eller förflyttar oss se rad 206
                    }
                    else
                    {
                       hiddenBoard[row,col] = hiddenRefBoard[row,col];
                    }
                    Console.Write(hiddenBoard[row,col]);
                }
                Console.WriteLine();
            }
            


            ConsoleKeyInfo control = Console.ReadKey(true);
            switch (control.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (hiddenBoard[markedRow -2, markedCol] != "█████")
                    {
                        markedRow -= 2;
                    }
                    break;
                
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (hiddenBoard[markedRow +2, markedCol] != "█████")
                    {
                        markedRow += 2;
                    }
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (hiddenBoard[markedRow, markedCol -1] != "█████")
                    {
                        markedCol --;
                    }
                    break;
                
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (hiddenBoard[markedRow, markedCol +1] != "█████")
                    {
                        markedCol ++;
                    }
                    break;
                
                case ConsoleKey.Enter: //När vi trycker enter (Se rad 144 var vi gör denna logik)
                    hiddenRefBoard[markedRow, markedCol] = board[markedRow,markedCol];  // Vi ger hiddenRefBoard Temporärt sträng värdet från den synliga brädan,
                    if (savedPos.Count == 1 && (savedPos[0][0] != markedRow || savedPos[0][1] != markedCol))
                    {
                        savedPos.Add([markedRow,markedCol]);                                
                        savedString.Add(board[markedRow,markedCol]);
                        nrTries ++;
                    }
                    else if (savedPos.Count == 0)
                    {
                        savedPos.Add([markedRow,markedCol]);                                // Vi sparar platsen i en lista
                        savedString.Add(board[markedRow,markedCol]);                        // Vi sparar strängen i en lista (Nu är plats o sträng på samma index i var sin lista)                        
                    }
                    if ( savedPos.Count == 2 && savedString.Count == 2 && savedString[0] == savedString[1] &&                                     
                            (savedPos[0][0] != savedPos[1][0] || savedPos[0][1] != savedPos[1][1])  
                        )
                        {
                            nrCorrectAnswers ++;                        
                            hiddenBoard[savedPos[0][0],savedPos[0][1]] = savedString[0];        // Då sparas den dolda paret från den första
                            hiddenBoard[savedPos[1][0],savedPos[1][1]] = savedString[1];        // Då sparas den andra i paraet som man sist tryckte på.
                            savedPos.Clear();
                            savedString.Clear();
                        }
                        else if (savedPos.Count == 2 && savedString.Count == 2 && savedString[0] != savedString[1]) // Om man kommit upp i 2 sparningar, men de 2 är inte lika
                        {
                            Console.Clear();
                            Console.WriteLine($"Nr of tries: {nrTries} ");
                            Console.WriteLine($"Nr of correct answers: {nrCorrectAnswers}");
                            DrawMemoryBoard(hiddenRefBoard);
                            Console.ReadKey();
                            hiddenRefBoard[savedPos[0][0],savedPos[0][1]] = "     ";           
                            hiddenRefBoard[savedPos[1][0],savedPos[1][1]] = "     ";

                            savedPos.Clear();
                            savedString.Clear();

                        }

                        else if (savedPos.Count > 2)
                        {
                            savedPos.Clear();
                            savedString.Clear();
                        }
                    break;
            }


        }
        Console.Clear();
        DrawMemoryBoard(board);
        Console.WriteLine($"You finnished the game in {nrTries} tries.");
        return nrTries;
    }

    private void Movement(string[,] board)
    {

    }

    private Tile[,] CreateTileMemoryBoard()
    {
        List<Tile> icons = new()
        {
            new KeyTile(),
            new KeyTile(),
            new MysteryTile(10),
            new MysteryTile(10),
            new BossTile(),
            new BossTile(),
            new StarterTile(),
            new StarterTile(),
            new GoalTile(),
            new GoalTile(),            
            new DoorTile(),
            new DoorTile(),
            new ItemRewardTile(),
            new ItemRewardTile(),
            new EnemyTile("",1,1),
            new EnemyTile("",1,1),
            new PuzzleTile("",1,"",""),
            new PuzzleTile("",1,"",""),
        };

        Tile[,] memoryBoard = new Tile[5,8];

        Random random = new();
        
        for(int row = 1; row < memoryBoard.GetLength(0)-1; row++)
        {
            for(int col = 1; col < memoryBoard.GetLength(1)-1; col++)
            {
                int index = random.Next(0, icons.Count);

            }

        }
        return memoryBoard;
        




    }
}