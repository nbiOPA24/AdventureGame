class EnemyRoom : RewardRoom
{
    public EnemyRoom(string roomName, int reward) : base(roomName, "[☠]", reward)
    {
    }
    
    public override void RunRoom()
    {
        if (RoomState == false)
        {
            Console.WriteLine("Welcome, adventurer! Beware... enemies lurk ahead. But fear not, Andreas is currently sharpening his coding sword to add some epic combat logic!");
            
            RoomState = true;
        }
        else
        {
            Console.WriteLine("You've been here before. No battles yet, but stay tuned! Andreas is preparing something exciting!");
        }
    }

}