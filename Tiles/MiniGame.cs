class MiniGame : Tile
{
    public bool Success {get; set;}
    public int MethodIndex {get; set;}
    public MiniGame() : base("Mini Game", " ⛶ ")
    {
        Solid = true;
        Color = ConsoleColor.Cyan;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        Character playerName = playerList[0];
        Console.Clear();
        // Creates a list of methods that has a predicate delegate. The method returns a bool and takes a character.
        List<Predicate<Character>> miniGames = [GuessNr, RockPaperSissor];

        if (!IsVisited) // If you visit the tile for the first time. A Random game will start.
        {
            Random random = new();
            MethodIndex = random.Next(0, miniGames.Count);
            Console.WriteLine($"Greetings {playerName.Name}!");
            Console.WriteLine($"Are you ready for a challenge?!");
            Console.WriteLine("Press any key to Continue!");
            Console.ReadKey(true);
            Success = miniGames[MethodIndex](playerName);
            IsVisited = true;
            if (Success) // If Success is true the tile will be removed and Solid will be false. It will disappear.
            {
                RemoveTile = true;
                Solid = false;
            }
        }

        else // If you revisit the same tile 
        {
            Console.WriteLine("You again!!! Wanna try your luck again?? [Y] or [N]");
            string reply = Utilities.ValidateString();
            Success = reply.ToLower() == "y" ? miniGames[MethodIndex](playerName) : false;
            if (Success) // If Success is true the tile will be removed and Solid will be false. It will disappear.
            {
                RemoveTile = true;
                Solid = false;
            }
        }
    }

    private bool GuessNr(Character player)
    {
        Random random = new();
        int secretNr = random.Next(1,101);
        Console.WriteLine("As you step forward, the ancient dragon Sinclair towers before you, his scales glistening like dark emeralds. He leers and declares, 'Only those with courage and wits may leave unharmed. Play my game and prove your worth...or face the wrath of fire and fury.'");
        Console.WriteLine($"Sinclair's eyes narrow as he addresses {player.Name}, 'I am thinking of a number between 1 and 100. Can you guess what it is?'");
        Console.Write("What number does the dragon have in mind? ");

        int tries = 0;
        int idiotTry = 0;
        bool gameIsRunning = true;
        while (gameIsRunning )
        {
            Console.WriteLine(secretNr);
            int guess = Utilities.ValidateInteger();
            
            tries++;
            if (tries >= 7)
            {
                Console.WriteLine($"You sense the air grow colder. Sinclair sneers, 'Seven guesses... and still you falter? Tell me, fool, what do you think happens now?'");
                List<string> choices = ["I shall be punished", "I shall feel the dragon's wrath", "I am doomed to despair"];
                Utilities.PickIndexFromList(choices);
                Console.WriteLine($"Sinclair's grin grows wider. 'Good, you understand the cost of failure.' In one swift motion, his massive claw strikes down, sending {player.Name} tumbling.");
                player.CurrentHealth -= 50;
                Console.WriteLine($"You feel the sting of the blow as your health drops by 50. Your remaining health is {player.CurrentHealth}.");
                return false;
            }
            
            if (guess < 1 || guess > 100)
            {
                if (idiotTry == 3)
                {
                    Console.WriteLine("'BEGONE, FOOL!' roars the dragon, as the ground trembles. 'Return when you have learned to follow instructions.' The game ends in failure.");
                    return false;
                }
                else
                {
                    Console.WriteLine($"The dragon's scales flare with a flash of green flame. 'Did I not make myself clear, mortal? BETWEEN 1 AND 100!' A wave of his toxic breath hits you, burning through {player.Name}'s defenses.");
                    player.CurrentHealth -= 10;
                    Console.WriteLine($"You stagger as your health drops by 10. Remaining health: {player.CurrentHealth}.");
                }
                idiotTry++;
            }

            else if (guess < secretNr)
            {
                if(secretNr - guess < 10)
                    Console.WriteLine("The dragon’s fanged smile grows. 'You are close, but not close enough. Try a bit higher...'");

                else
                    Console.WriteLine("The dragon growls. 'Higher, but do not waste my time!' he demands.");
            }
            else if (guess > secretNr)
            {
                if(guess - secretNr < 10)
                    Console.WriteLine("The dragon chuckles, clearly entertained. 'A little lower... perhaps there is hope for you yet.'");

                else
                    Console.WriteLine("Sinclair's patience wanes. 'Lower, mortal! Do not test me.'");
            }
            else if (guess == secretNr)
            {
                if (tries < 4)
                {
                    Console.WriteLine("Sinclair stares at you, wide-eyed. 'Impossible! Could it be...?' He recoils, muttering, 'Perhaps you have some ancient magic... but I shall not be outdone.'");
                    Console.WriteLine($"Suspicious of your quick success in only {tries} attempts, he grumbles and mutters a spell as he slinks away.");
                    int maxHealthBefore = player.MaxHealth;
                    Console.WriteLine("Before departing, he begrudgingly grants you a boon, muttering an incantation that resonates with power.");
                    player.MaxHealth += 50;
                    Console.WriteLine($"You feel a surge of vitality as your maximum health rises from {maxHealthBefore} to {player.MaxHealth}.");
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("The dragon smirks with a reluctant nod. 'You have earned my respect... barely. A modest reward, then.' He gifts you a boost of 10 health.");
                    player.MaxHealth += 10; 
                    Console.WriteLine($"Your maximum health now stands at {player.MaxHealth}.");
                }
                return true;
            }
        }
        return false;
    }


    private bool RockPaperSissor(Character player)
    {
        Console.WriteLine($"{player.Name} steps into a dark, cavernous room. Before them stand three levers, each one glowing faintly with an element: 'FIRE', 'WATER', 'EARTH'.");
        Console.WriteLine($"Out of the shadows, a tall, mysterious figure appears, moving silently towards {player.Name} and speaks in a low, echoing voice:");
        Console.WriteLine("Are you prepared for what lies ahead?");
        Console.ReadKey(true);
        List<string> replies = ["YES! Bring it on!", "How do I know which element I will face?", "Who are you?"];
        Console.WriteLine();
        bool isRunning = true;
        while (isRunning)
        {
            int replyIndex = Utilities.PickIndexFromList(replies, "Stranger... This room will be engulfed in one of these three elements for 5 rounds. You must counter it with its opposing element.");
            switch (replyIndex)
            {
                case 0:
                    Console.WriteLine("Very well, let's begin!");
                    isRunning = false;
                    Thread.Sleep(1000);
                    break;
                case 1:
                    Console.WriteLine("You won’t... not until it’s too late.");
                    Thread.Sleep(1000);
                    break;
                case 2:
                    Console.WriteLine("That is of no concern to you.");
                    Thread.Sleep(1000);
                    break;
            }
        }

        Console.WriteLine();
        Random random = new();

        List<string> elements = ["FIRE", "WATER", "EARTH"];
        int wins = 0;
        int loss = 0;
        bool elementGameIsRunning = true;
        while (elementGameIsRunning)
        {
            int indexChoice = random.Next(0, elements.Count);
            string elementNPC = elements[indexChoice];
            int elementChoice = Utilities.PickIndexFromList(elements, $" {elementNPC} !!!! BRACE YOURSELF!! The ground trembles, and one of the elements is taking form. Choose your counter! Wins: {wins} | Losses: {loss}");
            string yourElement = elements[elementChoice];

            if (yourElement == "FIRE")
            {
                if (elementNPC == "EARTH")
                {
                    Console.WriteLine("The room pauses... Your flames consume the earth around you. You have succeeded!");
                    wins++;
                }
                else if (elementNPC == "WATER")
                {
                    Console.WriteLine("The water floods in, extinguishing your flames... You have failed.");
                    loss++;
                }
                else
                {
                    Console.WriteLine("Fire meets fire, both are locked in a fierce, unyielding clash...");
                }
            }

            else if (yourElement == "WATER")
            {
                if (elementNPC == "FIRE")
                {
                    Console.WriteLine("The room calms... Your water douses the raging flames. You succeed!");
                    wins++;
                }
                else if (elementNPC == "EARTH")
                {
                    Console.WriteLine("The earth absorbs all your water, leaving nothing behind... You have failed.");
                    loss++;
                }
                else
                {
                    Console.WriteLine("Water meets water, a calmness fills the room as both forces neutralize each other.");
                }
            }

            else if (yourElement == "EARTH")
            {
                if (elementNPC == "WATER")
                {
                    Console.WriteLine("Your solid ground absorbs the rushing water, standing firm. You have parried the attack! Well done!");
                    wins++;
                }
                else if (elementNPC == "FIRE")
                {
                    Console.WriteLine("The flames scorch the ground, turning earth to ashes... You have failed.");
                    loss++;
                }
                else
                {
                    Console.WriteLine("Earth meets earth, an immovable silence fills the room...");
                }
            }

            if (wins == 3)
            {
                Console.WriteLine("Victory is yours! You have proven your strength!");
                player.Armor += 10;
                Console.WriteLine("As a reward, you gain 10 armor!");
                Thread.Sleep(1500);
                return true;
            }
            if (loss == 3)
            {
                player.Armor -= 10;
                Console.WriteLine("Defeat... you feel your strength wane as you lose 10 armor.");
                Thread.Sleep(1500);                
                return false;
            }
            Console.ReadKey(true);
        }
        return false;
    }


    private bool HangMan(Character player)
    {
        List<string> words = new();
        Console.WriteLine("Hangman");
        Console.ReadKey(true);

        return true;

    }

    private bool ReactionGame(Character player)
    {
        Console.WriteLine("ReactionGame");
        Console.ReadKey(true);
        return true;

    }
}