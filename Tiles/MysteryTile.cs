class MysteryTile : RewardTile
{

    public MysteryTile(string roomName, int reward) : base(roomName, " ? ", reward)
    {
    }

    public override void RunRoom(Player player)
    {
        if (RoomState == false)
        {
            Random random = new Random();
            int rndNr = random.Next(1,10000);
            if (rndNr < 6000)
            {
                int hp = 10;
                HandleReward("common", random, hp, player);
            }
            else if (rndNr < 8000)
            {
                int hp = 20;
                HandleReward("uncommon", random, hp, player);
            }
            else if (rndNr < 9500)
            {
                int hp = 30;
                HandleReward("rare", random, hp, player);
            }
            else
            {
                int hp = 50;
                HandleReward("very rare", random, hp, player);
            }
            RoomState = true;
        }
        else
        {
            Console.WriteLine("There's nothing left to discover here. You've already taken your chances.");
        }
    }

    private void HandleReward(string rarityLevel, Random random, int hp, Player player)
    {
        int badOrGood = random.Next(1,3);
        if (badOrGood == 1)
        {
            Console.WriteLine($"You get a {rarityLevel} good reward! Your health increases by {hp} points.");
            player.CurrentHealth += hp; // Kontrollera att det inte är större än maxHP
        }

        else
        {
            Console.WriteLine($"You get a {rarityLevel} bad reward! Your health decreases by {hp} points.");
            player.CurrentHealth -= hp;
        }
    }
}