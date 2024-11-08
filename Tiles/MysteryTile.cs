class MysteryTile : RewardTile
{

    public MysteryTile(int reward) : base("Mystery Gift", " ? ", reward)
    {
    }

    public override void RunTile(List<Character> playerList)
    {
        if (IsVisited == false)
        {
            Random random = new Random();
            int rndNr = random.Next(1,10000);
            if (rndNr < 6000)
            {
                int hp = 10;
                HandleReward("common", random, hp, playerList[0]);
            }
            else if (rndNr < 8000)
            {
                int hp = 20;
                HandleReward("uncommon", random, hp, playerList[0]);
            }
            else if (rndNr < 9500)
            {
                int hp = 30;
                HandleReward("rare", random, hp, playerList[0]);
            }
            else
            {
                int hp = 50;
                HandleReward("very rare", random, hp, playerList[0]);
            }
            IsVisited = true;
        }
        else
        {
            Console.WriteLine("There's nothing left to discover here. You've already taken your chances.");
        }
    }

    private void HandleReward(string rarityLevel, Random random, int hp, Character player)
    {
        int badOrGood = random.Next(1,3);
        if (badOrGood == 1)
        {
            Console.WriteLine($"You get a {rarityLevel} good reward! Your health increases by {hp} + {Reward} points.");
            player.CurrentHealth += hp + Reward; // Kontrollera att det inte är större än maxHP
        }

        else
        {
            Console.WriteLine($"You get a {rarityLevel} bad reward! Your health decreases by {hp} + {Reward} points.");
            player.CurrentHealth -= hp + Reward;
        }
    }
}