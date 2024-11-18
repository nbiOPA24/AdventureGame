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

    private void DrawSnakeMap(object[,] gameUi)
    {
        int x = 0;
        int markedRow = 9;
        int markedCol = 9;
        bool hasFood = false;
        int snakeEatenCount = 0;
        bool isRunning = true;
        Direction direction = Direction.down;
        while (isRunning)
        {
            if(!hasFood)
            {
                CreateFood();
                hasFood = true;
            }
            Console.Clear();
            for(int row = 0; row < gameUi.GetLength(0); row++)
            {
                for(int col = 0; col < gameUi.GetLength(1); col++)
                {
                    if (row == markedRow && col == markedCol)
                    {
                        Console.Write(" @ ");
                    }

                    else
                    {
                        Console.Write(gameUi[row,col]);
                    }
                }
                Console.WriteLine();
            }
            Thread.Sleep(200);


            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                MoveDirection(key, ref direction); 
            }


            switch (direction)
            {
                case Direction.up:
                    if(markedRow - 1 > 0)
                    {
                        markedRow --;
                        if(gameUi[markedRow,markedCol] == " * ")
                        {
                            gameUi[markedRow,markedCol] = "   ";
                            snakeEatenCount ++;
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;
                    break;

                case Direction.down:
                    if(markedRow + 1 < gameUi.GetLength(0)-1)
                    {
                        markedRow ++;
                        if(gameUi[markedRow,markedCol] == " * ")
                        {
                            gameUi[markedRow,markedCol] = "   ";
                            snakeEatenCount ++;
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;                    
                    break;

                case Direction.left:
                    if(markedCol - 1 > 0)
                    {
                        markedCol --;
                        if(gameUi[markedRow,markedCol] == " * ")
                        {
                            gameUi[markedRow,markedCol] = "   ";
                            snakeEatenCount ++;
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;                        
                    break;

                case Direction.right:
                    if(markedCol + 1 < gameUi.GetLength(1)-1)
                    {
                        markedCol ++;
                        if(gameUi[markedRow,markedCol] == " * ")
                        {
                            gameUi[markedRow,markedCol] = "   ";
                            snakeEatenCount ++;
                            hasFood = false;
                        }

                    }
                    else
                        isRunning = false;                        
                    break;
            }
            
        }
    }

    private void GameLogic(int width, int height)
    {
        char[,] snakeGameSize = new char[width,height];
        for(int row = 0; row < snakeGameSize.GetLength(0); row++)
        {
            for(int col = 0; col < snakeGameSize.GetLength(1); col++)
            {
                if (row == 0 || row == snakeGameSize.GetLength(0) || col == 0 || col == snakeGameSize.GetLength(1))
                {
                    snakeGameSize[row,col] = '0';
                }
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
            randomRow = random.Next(1, GameUi.GetLength(0));
            randomCol = random.Next(1, GameUi.GetLength(1));
        }
        while (3 == 2); //TODO
        
            
    }


    private class SnakeHead : HeadTile
    {
        public SnakeHead() : base(" ● ", false, 0)
        {

        }
    }

    private class SnakeTail : HeadTile
    {
        public SnakeTail() : base(" ▣ ", false, 0)
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
        int Counter {get;set;}
        public HeadTile(string icon, bool removeIcon, int counter)
        {
            Icon = icon;
            RemoveIcon = removeIcon;
            Counter = counter;
        }
    }
}