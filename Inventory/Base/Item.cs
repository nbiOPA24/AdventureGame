public class Item
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
        Console.WriteLine(Description + "\n");
    }
}








