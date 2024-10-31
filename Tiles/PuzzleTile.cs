class PuzzleTile : RewardTile
{
    public string Question { get; set; }
    public string SecretAnswer { get; set; }

    public PuzzleTile(string roomName, int reward, string question, string secretAnswer) : base(roomName, " ⌘ ", reward)
    {
        Question = question;
        SecretAnswer = secretAnswer;
    }

    public override void RunRoom(Character player)
    {
        if (RoomState == false)
        {
            Console.WriteLine("You enter a room filled with mystery. To proceed, you must solve the puzzle that lies before you. Think carefully, adventurer, for the path forward depends on your wits.");
            Console.WriteLine(Question);
            string reply = Utilities.ValidateString();

            if (string.Equals(reply, SecretAnswer, StringComparison.OrdinalIgnoreCase))
            {
                player.CurrentHealth += Reward;
                Console.WriteLine($"Congratulations, you succeeded! You gain {Reward} health.");
                Success = true;
            }
            else
            {
                Console.WriteLine($"Sorry, that's incorrect. The correct answer is: {SecretAnswer}. You loose {Reward} health");
                player.CurrentHealth -= Reward;
                Success = false;
            }

            RoomState = true;
        }
        else
        {
            string roomStatus = Success ? "succeeded": "failed";
            Console.WriteLine($"The puzzle has already been {roomStatus}. The room feels quieter now, as if waiting for the next adventurer to test their mind.");
        }
    }
}
