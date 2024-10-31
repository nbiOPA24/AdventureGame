 abstract class RewardTile : Tile
{
    public int Reward {get;set;}
    public bool Success {get; set;}
    public RewardTile(string tileName, string tileIcon, int reward) : base (tileName, tileIcon)
    {
        Reward = reward;
    }
}
