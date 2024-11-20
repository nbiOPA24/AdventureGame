class SnakeTile : RewardTile
{
    public SnakeTile() : base("name", " 🐉", 10)
    {
        Solid = true;
    }

    public override void RunSolidTile(List<Character> playerList)
    {
        Snake snake = new();
        snake.RunGame();

        Solid = false;
        RemoveTile = true;
        
    }

}