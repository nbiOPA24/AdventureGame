class PuzzleTile : RewardTile
{
    public string Question { get; set; }
    public string SecretAnswer { get; set; }

    public PuzzleTile(string tileName, int reward, string question, string secretAnswer) : base(tileName, " ⌘ ", reward)
    {
        Question = question;
        SecretAnswer = secretAnswer;
    }

    public override void RunTile(List<Character> playerList)
    {
        if (IsVisited == false)
        {
            Console.WriteLine("You enter a room filled with mystery. To proceed, you must solve the puzzle that lies before you. Think carefully, adventurer, for the path forward depends on your wits.");
            Console.WriteLine(Question);
            string reply = Utilities.ValidateString();

            if (string.Equals(reply, SecretAnswer, StringComparison.OrdinalIgnoreCase))
            {
                playerList[0].CurrentHealth += Reward;
                Console.WriteLine($"Congratulations, you succeeded! You gain {Reward} health.");
                Success = true;
            }
            else
            {
                Console.WriteLine($"Sorry, that's incorrect. The correct answer is: {SecretAnswer}. You loose {Reward} health");
                playerList[0].CurrentHealth -= Reward;
                Success = false;
            }

            IsVisited = true;
        }
        else
        {
            string tileStatus = Success ? "succeeded": "failed";
            Console.WriteLine($"The puzzle has already been {tileStatus}. The area feels quieter now, as if waiting for the next adventurer to test their mind.");
        }
    }
}
