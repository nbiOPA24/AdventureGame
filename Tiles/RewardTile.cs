 abstract class RewardTile : Tile
{
    public int Reward {get;set;}
    public bool Success {get; set;}
    public RewardTile(string roomName, string roomIcon, int reward) : base (roomName, roomIcon)
    {
        Reward = reward;
    }
}
