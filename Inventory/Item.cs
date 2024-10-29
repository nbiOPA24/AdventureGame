class Item
{
    public string Name {get; set;}
    public string Description {get; set;}

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public virtual void ShowItem()
    {
        Utilities.CharByCharLine($"{"Item",-12}", 8, ConsoleColor.DarkBlue, false);
        Utilities.CharByCharLine($"{Name,-12}", 8, ConsoleColor.DarkBlue, false);
        Console.WriteLine("\n" + Description);
    }
}

abstract class ItemWithLevel : Item, RandomRarity
{
    public int ItemLevel {get;set;}
    public int ItemRarity {get;set;}

    protected ItemWithLevel(string name, string description) : base(name, description)
    {
    }

    public Rarity GenerateItemWithRandomRarity()
    {
        {
            Random random = new Random();
            int roll = random.Next(1, 10001);

            if (roll <= 6000) return Rarity.Common;
            if (roll <= 8500) return Rarity.Uncommon;
            if (roll <= 9500) return Rarity.Rare;
            if (roll <= 9850) return Rarity.Epic;
            if (roll <= 9950) return Rarity.Legendary;
            return Rarity.Mythic;
        }
    }
}


interface RandomRarity
{
    public Rarity GenerateItemWithRandomRarity(); 
}

// Denna kan läggas bland enum samlade ?
enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic
}