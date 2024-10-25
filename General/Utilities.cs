class Utilities
{
    #region Validation methods
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
    #endregion
    #region ConsoleWriteColor
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
    #endregion
    #region CharByChar
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
        CharByChar(input,delay);
        Console.ResetColor();
    }
    public static void CharByChar(string input,int delay,ConsoleColor color,bool runMethod)
    {
        if(runMethod)
        { 
            CharByChar(input,delay,color);
        }
        else
        { 
            CharByChar(input,0,color);
        }
    }
    public static void CharByChar(string input,int delay,bool runMethod)
    {
        if(runMethod)
        {
            CharByChar(input,delay);
        }
        else
        { 
            CharByChar(input,0);
        }
    }
    public static void CharByCharLine(string input,int delay)
    {
        foreach(char c in input)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
    public static void CharByCharLine(string input,int delay,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        CharByCharLine(input,delay);
        Console.ResetColor();
    }
        public static void CharByCharLine(string input,int delay,ConsoleColor color,bool runMethod)
    {
        if(runMethod)
        { 
            CharByCharLine(input,delay,color);
        }
        else
        { 
            CharByCharLine(input,0,color);
        }
    }
    #endregion
    #region Menupicker
    public static int PickIndexFromList(List<string> list)
    {
        int markedIndex = 0;
        bool stillChoosing = true;
        int returnValue = 0;
        while(stillChoosing)
        {
            
            for(int i = 0; i< list.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    Console.Write(list[i]);
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

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
            Console.Clear();
        }
        return returnValue;
    }
    public static int PickIndexFromList(List<string> list,string questionToPromptSelection)
    {
        int markedIndex = 0;
        bool stillChoosing = true;
        int returnValue = 0;
        
        while(stillChoosing)
        {
            Console.Clear();
            Console.WriteLine(questionToPromptSelection);
            for(int i = 0; i< list.Count; i++)
            {
                if(i == markedIndex)
                {
                    Utilities.ConsoleWriteColor("*",ConsoleColor.Blue);
                    Console.Write(list[i]);
                    Utilities.ConsoleWriteLineColor("*",ConsoleColor.Blue);

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
            Console.Clear();
        }
        return returnValue;
    }
    #endregion
    #region ToStringList
        public static List<string> ToStringList(List<Ability> abilityList)
    {
        //puts all the names of the abilities into a string list
            List<string> stringReturnList = new();
            foreach(Ability a in abilityList)
            {
                stringReturnList.Add(a.Name);
                Console.WriteLine(a.Name);
            }
            return stringReturnList;
    }
    public static List<string> ToStringList(Ability[] abilityList)
    {
        //puts all the names of the abilities into a string list
            List<string> stringReturnList = new();
            foreach(Ability a in abilityList)
            {
                if(a != null)
                {
                stringReturnList.Add(a.Name);
                Console.WriteLine(a.Name);
                }
            }
            return stringReturnList;
    }
    public static List<string> ToStringList(List<Enemy> enemyList)
    {
        List<string> stringReturnList  = new();
        foreach(Enemy e in enemyList)
        {
            stringReturnList.Add(e.Name);
        } 
        return stringReturnList;
            
    }
    #endregion 
}
