public class ItemRewardTile : Tile
{
    public ItemRewardTile() : base("Item Reward tile", " ★ ")
    {
    }
    public override void RunTile(List<Character> playerList)
    {
        Character player = playerList[0];
        if (IsVisited == false)
        {
            Random random = new();
            int result = random.Next(0,2);
            if (result == 0)
            {
                GiveSword(player);
            }
            else
            {
                Nothing(player);
            }
        }
    }

    private void GiveSword(Character player)
    {
        player.Inventory.Items.Add(new WeaponItem("Sword", "Just a sword", 5, WeaponType.Sword, 5, Rarity.Common));
        Console.WriteLine("Congratulations you got a sword to you invetory! You also get 10 extra basedamage!");
        player.BaseDamage +=10;
    }

    private void Nothing(Character player)
    {
        Console.WriteLine($"{player.Name} got nothing. Bye!");
    }
}