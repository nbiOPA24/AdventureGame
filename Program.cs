﻿using System.Media;
public class Program
{
    public static void Main()
    {
        Console.Clear();
        Console.WriteLine("1. Fast Test Mode");
        Console.WriteLine("2. Realistic experience");
        int choice = Utilities.ValidateInteger(1,2);

        switch (choice)
        {
            case 1:
                Test.RunTest();
                return;
                Room [,] rooms = MainMap.GenerateMap();
                IRace race = new Human();
                Player player = MapHandler.PlayerStartPos(new ("Goku", 100, race, 15, 10), rooms);  // Kan man inte lägga in så här istället: "new ("Goku", 100, new Human(), 15, 10)"
                MainMap.RunEntireMap(player, rooms);
                break;
            case 2: //Realistic experience
                Console.Clear();
                PlayBackgroundMusic();
                Utilities.CharByChar("Welcome to the adventure game!", 8, ConsoleColor.DarkBlue); Console.WriteLine();
                Utilities.CharByChar("1. Start new game", 8, ConsoleColor.DarkBlue); Console.WriteLine();
                Utilities.CharByChar("2. Load game", 8, ConsoleColor.DarkBlue);
                Utilities.CharByChar("Q. Exit game", 8, ConsoleColor.DarkBlue);
                Console.Clear();
                List<string> mainMenu = new() 
                {
                    "1. Start new game",
                    "2. Load game",
                    "Q. Exit game"
                };
                int menuIndex = Utilities.PickIndexFromList(mainMenu, "Welcome to the adventure game!", ConsoleColor.DarkBlue);
                switch(menuIndex)
                {
                    // Start new game, creating a character.
                    
                    case 0:
                        Player playerName = CreateCharacter();
                        Room [,] gameMap = MainMap.GenerateMap();
                        playerName = MapHandler.PlayerStartPos(playerName, gameMap);
                        GreetingNewPlayerMessage(playerName);
                        MainMap.RunEntireMap(playerName, gameMap);
                        break;
                    case 1:
                        break;
                    case 2:
                        Utilities.CharByChar("Terminating game session...", 15, ConsoleColor.Red);
                        break;
                }
                break;
        }
    }
    public static void PlayBackgroundMusic()
    {
        SoundPlayer player = new SoundPlayer("Music.wav"); // Uppdatera med WAV-filen
        player.PlayLooping(); // Spelar musiken i en loop
    }

    static Player CreateCharacter()
    {
        //string name,int startingHealth,IRace race,int baseDamage,int armor
        Utilities.CharByChar("Please enter a player name: ", 8, ConsoleColor.DarkBlue);
        string name = Utilities.ValidateString();

        Utilities.CharByChar("Please Choose a race", 8);
        List<string> raceType = new() {"Human", "Dwarf", "Elf"};
        int raceTypeIndex = Utilities.PickIndexFromList(raceType, "Please Choose your race!");
        List<IRace> races = new() {new Human(), new Dwarf(), new Elf()};
        IRace race = races[raceTypeIndex];


        List<string> gameDifficulty = new() {"Easy", "Medium", "Hard"}; 
        int difficultyChoiceIndex = Utilities.PickIndexFromList(gameDifficulty, "Please Choose a difficulty!");
        try
        {
            if (difficultyChoiceIndex == 0)
            {
                //EASY
                return new Player(name, 250, race, 40, 20);
            }
            else if (difficultyChoiceIndex == 1)
            {
                //MEDIUM
                return new Player(name, 150, race, 30, 10);

            }
            else if (difficultyChoiceIndex == 2)
            {
                //HARD
                return new Player(name, 80, race, 15, 4);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"NÅGOT GICK FEEEEL! {ex}");
        }
        
        throw new Exception("SKAPADE INGEN CHARKTÄR!! NÅNTING ÄR FEL I CreateCharacter() METODEN!!!");

    }

    static void GreetingNewPlayerMessage(Player player)
    {
        Console.Clear();
        string greetMessage = @$"
Darkness surrounds you, {player.Name}, the {player.Race}.
In the distance, you hear the echoes of forgotten battles...
The dungeon awaits, and with every step, danger looms closer.
Will you survive, or will you join the souls lost in these cursed halls?

Prepare yourself... for the journey begins now.";
        Utilities.CharByCharLine(greetMessage, 25, ConsoleColor.DarkBlue);
        Utilities.CharByChar("PRESS ANY KEY TO ENTER THE DUNGEON: ", 25, ConsoleColor.DarkRed);
        Console.ReadKey(true);
    }
}