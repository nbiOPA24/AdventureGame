using System.Media;
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
            case 2: //Realistic experience
                Console.Clear();
                //PlayBackgroundMusic();
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
                        List<Character> playerList = PlayerFactory.GenerateParty();
                        Ability ability = new("Hej", eTargetType.Enemy, 0, eAbilityType.OffensiveStrong);
                        ability.AddDamageEffect(999, true);
                        playerList[1].Abilities.Add(ability);
                        GreetingNewPlayersMessage(playerList);    
                        MapHandler.RunEntireMap(playerList,3,6);
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
            
    static void GreetingNewPlayersMessage(List<Character> playerList)
    {
        Console.Clear();
        string greetMessage = @$"
Darkness surrounds you, {playerList[0].Name}, {playerList[1].Name}, {playerList[2].Name}, {playerList[3].Name}, 
In the distance, you hear the echoes of forgotten battles...
The dungeon awaits, and with every step, danger looms closer.
Will you survive, or will you join the souls lost in these cursed halls?

Prepare yourself... for the journey begins now.";
        Utilities.CharByCharLine(greetMessage, 25, ConsoleColor.DarkBlue, false);
        Utilities.CharByChar("PRESS ANY KEY TO ENTER THE DUNGEON: ", 25, ConsoleColor.DarkRed, false);
        Console.ReadKey(true);
    }
}