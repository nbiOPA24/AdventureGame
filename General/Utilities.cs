class Utilities
{
    public static int ValidateInteger()
    {
        int tries = 0;
        while(true)
        {
            tries ++;
            if(!int.TryParse(Console.ReadLine(), out int integ))
            {
                if (tries > 3)
                {
                    Console.WriteLine("Du kan endast skriva ut siffror här. T.ex '123' o.s.v...");
                }
                else
                {
                    Console.WriteLine("Vänligen ange en heltalssiffra!");

                }
            }
            else
            {
                return integ;
            }
        }
    }
        /// <summary>
        /// min is the lowest value you want returned
        /// max is the maximum value you want returned
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int ValidateInteger(int min, int max)
    {
        int tries = 0;
        while(true)
        {
            tries ++;
            if(!int.TryParse(Console.ReadLine(), out int integ))
            {
                
                if (tries > 3)
                {
                    Console.WriteLine("Du kan endast skriva ut siffror här. T.ex '123' o.s.v...");
                }
                else
                {
                    Console.WriteLine("Vänligen ange en heltalssiffra!");

                }
            }
            else
            {
                if(integ > 0 && integ <= max)
                {
                    return integ;
                }
                else
                    Console.WriteLine($"Ditt val måste vara mellan {min}-{max}");
            
                
            }
        }
    }

    public static string ValidateString()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Please write something! You can't leave a empty space.");
            }

        }
    }
    public static string ValidateString(string input)
    {
        while (true)
        {
            if(!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Please write something! You can't leave a empty space.");
            }
            input = Console.ReadLine();

        }
    }

    public static void ConsoleWriteColor(string input, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(input);
        Console.ResetColor();
    }
    public static void ConsoleWriteLineColor(string input, ConsoleColor color)
    {
        ConsoleWriteColor(input,color);
        Console.WriteLine();
    }
    //Skriver ut en sträng med vald delay
    public static void CharByChar(string input,int delay)
    {
        foreach(char c in input)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
    //Överlagring med en tredje parameter för färg på texten som skall "fadeas" in
    public static void CharByChar(string input,int delay,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        foreach(char c in input)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.ResetColor();
    }
    public static int PickIndexFromList(List<string> list)
    {
        int markedIndex = 0;
        bool stillChoosing = true;
        int returnValue = 0;
        while(stillChoosing)
        {
            Console.Clear();
            for(int i = 0; i< list.Count; i++)
            {
                if(i == markedIndex)
                {
                    Console.Write("*");
                    Console.WriteLine(list[i]+"*");

                }
                else
                {
                    Console.WriteLine(list[i]);
                }
            }
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch(pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(markedIndex > 0 )
                    {
                        markedIndex--;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if(markedIndex < list.Count -1 )
                    {
                        markedIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    returnValue = markedIndex;
                    stillChoosing = false;
                    break;
            }
        }
        return returnValue;
    }
}
