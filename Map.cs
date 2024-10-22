public class Map
{
    public int Width {get; set;}
    public int Height {get; set;}
    public Room[,] Rooms;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Rooms = new Room[width, height];
    }

    public void ShowMap()
    {
        for (int i = 0; i < Rooms.GetLength(0); i++)
        {
            for (int j = 0; j < Rooms.GetLength(1); j++)
            Console.Write($"[{Rooms[i,j].RoomIcon}]");
        }
        Console.WriteLine();

    }
}