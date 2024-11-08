class MiniGame : Tile
{
    public bool Success {get; set;}
    public int MethodIndex {get; set;}
    public MiniGame() : base("Mini Game", " ⛶ ")
    {
        Solid = true;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        Console.Clear();
        Character playerName = playerList[0];
        
        List<Predicate<Character>> miniGames = [GuessNr, RockPaperSissor];

        if (!IsVisited)
        {
            Random random = new ();
            MethodIndex = random.Next(0, miniGames.Count);
            Console.WriteLine($"Greetings {playerName.Name}!");
            Console.WriteLine($"Are you ready for a challenge?!");
            Console.WriteLine("Press any key to Continue!");
            Console.ReadKey(true);
            Success = miniGames[MethodIndex](playerName);
            IsVisited = true;
        }
        else // If you revisit the same tile 
        {
            if (Success)
            {
                Console.WriteLine("You have already won this game!");
            }
            else
            {
                Console.WriteLine("You again!!! Wanna try your luck again??");
                Success = miniGames[MethodIndex](playerName);
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
        Console.WriteLine($"{player.Name} enters a dark big darkroom, he can see three levers in front of him self. Each lever has diffrent elements shining on them. 'FIRE', 'WATER', 'EARTH'.");
        Console.WriteLine($"Suddenly a tall figure appears from no where.. And walkes towards {player.Name} and sais: ");
        Console.WriteLine("Are you ready?");
        List<string> replys = ["YES! Bring it on!", "How do i know what element i will face?", "Who are you?"];
        Console.WriteLine();
        bool isRunning = true;
        while (isRunning)
        {
            int replyIndex = Utilities.PickIndexFromList(replys,"Friend.. This room will be filled with one of the 3 elements you see for 5 rounds. You must dodge the element by encounter it with it opposite weakness");
            switch (replyIndex)
            {
                case 0:
                    Console.WriteLine("Lets go!");
                    isRunning = false;
                    break;
                case 1:
                    Console.WriteLine("You dont..");
                    break;
                case 2:
                    Console.WriteLine("Not your buisness..");
                    break;
            }
        }

        Console.WriteLine();

        List<string> elements = ["FIRE", "WATER", "EARTH"];
        bool elementGameIsRunning = true;
        while (elementGameIsRunning)
        {
            int elementChoice = Utilities.PickIndexFromList(elements, "PREPARE YOURSELF!! The room started crumbling.. One of the elements is on its way. Quickly what do you choose?! ");
        }



        return false;

    }

    private bool HangMan(Character player)
    {
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