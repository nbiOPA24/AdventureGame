public class Snake
{
    private HeadTile[,] GameUi = new HeadTile[20,20];
    public void RunGame()
    {
        HeadTile [,] gameBoard = CreateGameUI();

        DrawSnakeMap(gameBoard);

    }

    private HeadTile[,] CreateGameUI()
    {
        
        for(int row = 0; row < GameUi.GetLength(0); row++)
        {
            for(int col = 0; col < GameUi.GetLength(1); col++)
            {

                if (row == 0 || row == GameUi.GetLength(0)-1 || col == 0 || col == GameUi.GetLength(1)-1)
                {
                    GameUi[row,col] = new Wall();
                }   
                else
                {
                    GameUi[row,col] = new EmptyPos();
                }
            }
        }
        return GameUi;
    }

    private void DrawSnakeMap(HeadTile[,] gameUi)
    {
        int speed = 300;
        int markedRow = gameUi.GetLength(0)/2;
        int markedCol = gameUi.GetLength(1)/2;
        bool hasFood = false;
        bool isRunning = true;
        Direction direction = Direction.down;
        HeadTile snakeHead = new SnakeHead();
        List<HeadTile> snakeTails = new();
        while (isRunning)
        {
            speed = 300 - snakeHead.Counter * 10;
            Console.Clear();
            for (int i = snakeTails.Count - 1; i >= 0; i--)
            {
                snakeTails[i].Counter--;
                if (snakeTails[i].Counter < 1)
                {
                    snakeTails.RemoveAt(i);
                }
            }

            if(!hasFood)
            {
                CreateFood();
                hasFood = true;
            }

            if(snakeHead.Counter > 0)
                snakeTails.Add(GenerateTail(markedRow, markedCol, snakeHead));
            
            for(int row = 0; row < gameUi.GetLength(0); row++)
            {
                for(int col = 0; col < gameUi.GetLength(1); col++)
                {
                    if (row == markedRow && col == markedCol)
                    {
                        Console.Write(snakeHead.Icon);
                    }
                    else
                    {
                        Console.Write(gameUi[row,col].Icon);
                    }

                    if(gameUi[row,col] is SnakeTail && gameUi[row,col].Counter == 0)
                    {
                        gameUi[row,col] = new EmptyPos();
                    }


                }
                Console.WriteLine();
            }
            Thread.Sleep(speed);
            


            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                MoveDirection(key, ref direction); 
            }


            switch (direction)
            {
                case Direction.up:
                    if(markedRow - 1 > 0 && (gameUi[markedRow - 1,markedCol] is EmptyPos || gameUi[markedRow -1,markedCol] is SnakeFood))
                    {
                        markedRow --;
                        if(gameUi[markedRow,markedCol] is SnakeFood)
                        {
                            gameUi[markedRow,markedCol] = new EmptyPos();
                            snakeHead.Counter ++;
                            foreach(HeadTile snakeTail in snakeTails)
                            {
                                snakeTail.Counter++;
                            }
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;
                    break;

                case Direction.down:
                    if(markedRow + 1 < gameUi.GetLength(0)-1 && (gameUi[markedRow + 1,markedCol] is EmptyPos || gameUi[markedRow+1,markedCol] is SnakeFood))
                    {
                        markedRow ++;
                        if(gameUi[markedRow,markedCol] is SnakeFood)
                        {
                            gameUi[markedRow,markedCol] = new EmptyPos();
                            snakeHead.Counter ++;
                            foreach(HeadTile snakeTail in snakeTails)
                            {
                                snakeTail.Counter++;
                            }
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;                    
                    break;

                case Direction.left:
                    if(markedCol - 1 > 0 && (gameUi[markedRow,markedCol-1] is EmptyPos || gameUi[markedRow,markedCol-1] is SnakeFood))
                    {
                        markedCol --;
                        if(gameUi[markedRow,markedCol] is SnakeFood)
                        {
                            gameUi[markedRow,markedCol] = new EmptyPos();
                            snakeHead.Counter ++;
                            foreach(HeadTile snakeTail in snakeTails)
                            {
                                snakeTail.Counter++;
                            }
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;                        
                    break;

                case Direction.right:
                    if(markedCol + 1 < gameUi.GetLength(1)-1 && (gameUi[markedRow,markedCol+1] is EmptyPos || gameUi[markedRow,markedCol+1] is SnakeFood))
                    {
                        markedCol ++;
                        if(gameUi[markedRow,markedCol] is SnakeFood)
                        {
                            gameUi[markedRow,markedCol] = new EmptyPos();
                            snakeHead.Counter ++;
                            foreach(HeadTile snakeTail in snakeTails)
                            {
                                snakeTail.Counter++;
                            }
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;
                    break;
            }
            
        }
    }

    private void MoveDirection(ConsoleKey key, ref Direction direction)
    {
        switch (key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                if (direction != Direction.down)
                {
                    direction = Direction.up;
                }
                break;

            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                if (direction != Direction.up)
                {
                    direction = Direction.down;
                } 
                break;


            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                if (direction != Direction.right)
                {
                    direction = Direction.left;
                }
                break;
            
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                if (direction != Direction.left)
                {
                    direction = Direction.right;
                }
                break;
        }
    }

    private enum Direction
    {
        up,
        down,
        left,
        right,
        
    }
    private void CreateFood()
    {
        Random random = new Random();
        int randomRow;
        int randomCol;
        do
        {
            randomRow = random.Next(1, GameUi.GetLength(0)-1);
            randomCol = random.Next(1, GameUi.GetLength(1)-1);
        }
        while (GameUi[randomRow,randomCol] is not EmptyPos);
        GameUi[randomRow,randomCol] = new SnakeFood();        
            
    }

    private HeadTile GenerateTail(int markedRow, int markedCol, HeadTile snakeHead)
    {
        return GameUi[markedRow,markedCol] = new SnakeTail(snakeHead.Counter);
    }




    private class SnakeHead : HeadTile
    {
        public SnakeHead() : base(" ● ", false, 0)
        {

        }
    }
    private class SnakeFood : HeadTile
    {
        public SnakeFood() : base(" * ", false, 0)
        {

        }
    }

    private class SnakeTail : HeadTile
    {
        public SnakeTail(int counter) : base(" ▣ ", false, counter)
        {
        }
        
    }

    private class EmptyPos : HeadTile
    {
        public string Icon = "   ";
        public EmptyPos() : base("   ", false, 0)
        {

        }
    }

    private class Wall : HeadTile
    {
        public string Icon = "███";
        public Wall() : base("███", false, 0)
        {

        }
    }

    private abstract class HeadTile 
    {
        public string Icon {get; set;}
        public bool RemoveIcon {get; set;}
        public int Counter {get;set;}
        public HeadTile(string icon, bool removeIcon, int counter)
        {
            Icon = icon;
            RemoveIcon = removeIcon;
            Counter = counter;
        }
    }
}