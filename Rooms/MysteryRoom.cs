class MysteryRoom : RewardRoom
{

    public MysteryRoom(string roomName, int reward) : base(roomName, "[?]", reward)
    {
    }

    public void RunRoom()
    {
        Random random = new Random();
        int rndNr = random.Next(1,10000);
        if (rndNr < 6000)
        {
            HandleReward("common", random);
        }
        else if (rndNr < 8000)
        {
            HandleReward("uncommon", random);
        }
        else if (rndNr < 9500)
        {
            HandleReward("rare", random);
        }
        else
        {
            HandleReward("very rare", random);
        }
        RoomState = true;
    }

    private void HandleReward(string rarityLevel, Random random)
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