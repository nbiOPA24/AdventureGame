class EnemyRoom : RewardRoom
{
    public override string RoomIcon {get; set;} = "[☠]";
    // public List<Enemy> enemies = new List<Enemy>();
    public EnemyRoom (string roomName) : base(roomName)
    {
    }
}