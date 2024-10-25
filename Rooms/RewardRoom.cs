 abstract class RewardRoom : Room
{
    public int Reward {get;set;}
    public bool Success {get; set;}
    public RewardRoom(string roomName, string roomIcon, int reward) : base (roomName, roomIcon)
    {
        Reward = reward;
    }
}
