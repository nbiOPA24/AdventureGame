abstract class ItemWithLevel : Item, IRandomRarity
{
    public int ItemLevel {get;set;}
    public Rarity ItemRarity {get;set;}

    protected ItemWithLevel(string name, string description, int itemLevel, Rarity itemRarity) : base(name, description)
    {
        ItemLevel = itemLevel;
        ItemRarity = itemRarity;
    }

    protected ItemWithLevel(string name, string description) : base(name, description)
    {

    }

    public virtual Rarity GenerateItemWithRandomRarity()
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

    /* public virtual int GenerateItemLevel()
    {
        Rarity rarity = GenerateItemWithRandomRarity();
        if (rarity == Rarity.Common) return 10;
        if (rarity == Rarity.Uncommon) return 20;
        if (rarity == Rarity.Rare) return 30;
        if (rarity == Rarity.Epic) return 40;
        if (rarity == Rarity.Legendary) return 50;
        if (rarity == Rarity.Mythic) return 60;

    } */
}