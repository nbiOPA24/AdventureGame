class Inventory
{
    public List<Item> Items {get; set;} = new List<Item>();

    public void ShowInventory()
    {
        foreach(Item item in Items)
        {
            item.ShowItem();
        }
    }
}
