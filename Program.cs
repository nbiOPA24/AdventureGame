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
                        Character playerName = CreateCharacter();
                        List<Character> playerList = new List<Character>();
                        playerList.Add(playerName);
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

    static Character CreateCharacter()
    {
        //string name,int startingHealth,IRace race,int baseDamage,int armor
        Utilities.CharByChar("Please enter a player name: ", 8, ConsoleColor.DarkBlue);
        string name = Utilities.ValidateString();
        ICombatSelection playerCombatInterface = new PlayerCombatSelector();
        Character player = new Character(name,120,15,10,playerCombatInterface,ConsoleColor.Cyan,1);

        Ability attack = new Ability("Attack",eTargetType.Enemy,0,eAbilityType.Offensive);
        attack.AddDamageEffect(15);
        Ability healOther = new("Heal other",eTargetType.Friendly,3,eAbilityType.HealingOther);
        healOther.AddHealingEffect(10);
        Ability ignite = new("Venomous Ignite",eTargetType.Enemy,0,eAbilityType.Offensive);
        ignite.AddBurnEffect(1,1);
        //ignite.AddPoisonEffect(1,1);
        player.Abilities.Add(attack);
        player.Abilities.Add(healOther);
        player.Abilities.Add(ignite);
        player.ICombatHandler.AbilityList = player.Abilities;
        player.ICombatHandler.Self = player;
        return player;
    }
            

        


    

    static void GreetingNewPlayerMessage(Character player)
    {
        Console.Clear();
        string greetMessage = @$"
Darkness surrounds you, {player.Name}, the Human.
In the distance, you hear the echoes of forgotten battles...
The dungeon awaits, and with every step, danger looms closer.
Will you survive, or will you join the souls lost in these cursed halls?

Prepare yourself... for the journey begins now.";
        Utilities.CharByCharLine(greetMessage, 25, ConsoleColor.DarkBlue, false);
        Utilities.CharByChar("PRESS ANY KEY TO ENTER THE DUNGEON: ", 25, ConsoleColor.DarkRed, false);
        Console.ReadKey(true);
    }
}