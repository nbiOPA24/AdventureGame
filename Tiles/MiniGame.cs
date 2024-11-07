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
        
        List<Predicate<Character>> miniGames = [GuessNr, RockPaperSissor, HangMan, ReactionGame];

        if (!IsVisited)
        {
            Random random = new ();
            MethodIndex = random.Next(0, miniGames.Count -1);
            Console.WriteLine($"Greetings {playerName.Name}!");
            Console.WriteLine($"Are you ready for a challenge?!");
            Console.WriteLine("Press any key to Continue!");
            Console.ReadKey(true);
            // Success = miniGames[MethodIndex](playerName);
            Success = GuessNr(playerName);
            IsVisited = true;
        }
        else // If you revisit the same tile 
        {
            if (Success)
            {
                Console.WriteLine("You have already won this game!");
                Console.ReadKey(true);
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
        Console.WriteLine("The dragon Sinclair meets you and puts you in a danger situation, he forces you to join a whicked game. If you succeed you will get rewarded, if you fail, you will be punnished.");
        Console.WriteLine($"The dragon asks {player.Name} what nr he thinks about between 1 - 100.");
        Console.Write("What nr does the dragon think about? ");

        int tries = 0;
        int idiotTry = 0;
        bool gameIsRunning = true;
        while (gameIsRunning )
        {
            Console.WriteLine(secretNr);
            int guess = Utilities.ValidateInteger();
            tries ++;
            if (tries >= 7)
            {
                Console.WriteLine($" You have now guessed {tries} times.. Do you know what happens now? Please choose below what you think will happen- The dragon asks.");
                List<string> choices = ["I get punnished", "I get smacked", "My ass gets Whooped"];
                Utilities.PickIndexFromList(choices);
                Console.WriteLine($"The dragon smirks.. 'Im glad we understand eachother', he lifted his paw and hurled it in {player.Name}s head.");
                player.CurrentHealth -= 50;
                Console.WriteLine($"You've lost 50 HP... And now have {player.CurrentHealth} hp.");
                return false;
            }
            if (guess < 1 || guess > 100)
            {
                if (idiotTry == 3)
                {
                    Console.WriteLine("'COME BACK WHEN YOU GROW A NEW BRAIN!!' The dragon stops the game for your bad behavior..");
                    return false;
                }
                else
                {
                    Console.WriteLine($"'YOU IMBICIL!!! I said between 1 and 100!...' The dragon spits som acid on you and you lose 10 hp. ");
                    player.CurrentHealth -= 10;
                    Console.WriteLine($"Your health is now {player.CurrentHealth}");
                }
                idiotTry ++;
            }

            else if (guess < secretNr)
            {
                if(secretNr - guess < 10)
                    Console.WriteLine("The dragon smirks, and says 'higher...'");

                else
                    Console.WriteLine("Baaah! Higher!");
            }
            else if (guess > secretNr)
            {
                if(guess - secretNr < 10)
                    Console.WriteLine("The dragon smirks, and says 'lower...'");

                else
                    Console.WriteLine("Baaah! Lower!");
            }
            else if (guess == secretNr)
            {
                if (tries < 4)
                {
                    Console.WriteLine("The dragons face is stoned... 'How did you.. Baah! Get out of my face you imbicill. ' ");
                    Console.WriteLine($"The dragon was upset and suspects you for cheating. You answered correct in only {tries} tries");
                    int maxHealthBefore = player.MaxHealth;
                    Console.WriteLine("The dragon quickly while walking away from you casted a spell in the air...");
                    player.MaxHealth += 50;
                    Console.WriteLine($"You felt a strange feeling, you realised your max health has increased from {maxHealthBefore} to {player.MaxHealth} ");
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("'Congratulions!' The dragon said.. Ang gave you a health boost with 10 hp.");
                    player.MaxHealth += 10; 
                    Console.WriteLine($"Your maxHealth is now {player.MaxHealth}!");
                }
                return true;
            }

            
            
        }
        return false;
    }

    private bool RockPaperSissor(Character player)
    {
        Console.WriteLine("RockPaperSissor");
        Console.ReadKey(true);
        return true;

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