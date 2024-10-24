class MysteryRoom : RewardRoom
{

    public MysteryRoom(string roomName, int reward) : base(roomName, "[?]", reward)
    {
    }

    public override void RunRoom()
    {
        if (RoomState == false)
        {
            Random random = new Random();
            int rndNr = random.Next(1,10000);
            if (rndNr < 6000)
            {
                int hp = 10;
                HandleReward("common", random, hp);
            }
            else if (rndNr < 8000)
            {
                int hp = 20;
                HandleReward("uncommon", random, hp);
            }
            else if (rndNr < 9500)
            {
                int hp = 30;
                HandleReward("rare", random, hp);
            }
            else
            {
                int hp = 50;
                HandleReward("very rare", random, hp);
            }
            RoomState = true;
        }
        else
        {
            Console.WriteLine("There's nothing left to discover here. You've already taken your chances.");
        }
    }

    private void HandleReward(string rarityLevel, Random random, int hp)
    {
        int badOrGood = random.Next(1,3);
        if (badOrGood == 1)
        {
            Console.WriteLine($"You get a {rarityLevel} good Reward!");
        }
        else
        {
            Console.WriteLine($"You get a {rarityLevel} bad Reward");
        }

    }
}