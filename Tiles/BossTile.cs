class BossTile : RewardTile
{
    public BossTile() : base("Boss", " ♛ ", 999999)
    {
        Color = ConsoleColor.DarkRed;
        Solid = true;
    }
    public override void RunSolidTile(List<Character> playerList)
    {
        Character player = playerList[0];
        if (!IsVisited)
        {
            Utilities.CharByCharLine($"ARE YOU READY FOR THE FREEWORLD BEYOND {player.Name.ToUpper()}?! ", 15, ConsoleColor.DarkRed);
            Utilities.CharByCharLine("Well se about that.", 15, ConsoleColor.DarkRed);
            Utilities.CharByCharLine($"The mysterious creature picks up a large thing and puts it on {player.Name} chest, it started suck on him and he looses 150 health", 15, ConsoleColor.DarkBlue);
            if (player.CurrentHealth <= 150)
            {
                Utilities.CharByCharLine("You lost the game...", 15, ConsoleColor.DarkBlue);
                Thread.Sleep(1000);
                player.CurrentHealth -= 150;
            }

            else
            {
                Utilities.CharByCharLine("Congrats!, you can now move to the goal!", 15, ConsoleColor.DarkBlue);
                Thread.Sleep(1000);
                Solid = false;
                RemoveTile = true;
            }
        }
    }
}