class WeaponItem : ItemWithLevel, IRandomRarity
{
    public int Damage {get; set;}
    public WeaponType Type {get;set;}
    public WeaponItem(string name, string description, int damage, WeaponType type, int itemLevel, Rarity itemRarity) : base(name, description, itemLevel, itemRarity)
    {
        Damage = damage;
        Type = type;
    }
    public override void ShowItem()
    {
        Utilities.CharByCharLine($"{"Item",-20} {"Damage",-10} {"WeaponType",-15} {"Item rarity",-15} {"Item level",-15}", 8, ConsoleColor.DarkGreen, false);
        Utilities.CharByCharLine($"{Name,-20} {Damage,-10} {Type,-15} {ItemRarity,-15} {ItemLevel,-15}", 8, ConsoleColor.DarkBlue, false);
        Console.WriteLine(Description + "\n");
    }

}