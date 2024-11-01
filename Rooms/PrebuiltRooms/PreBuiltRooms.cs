class PreBuiltRooms
{
    public List<Room> LeftTopCorner {get;set;}
    public List<Room> RightTopCorner {get;set;}
    public List<Room> LeftBottomCorner {get;set;}
    public List<Room> RightBottomCorner {get;set;}
    public List<Room> LeftSide {get;set;}
    public List<Room> RighSide {get;set;}
    public List<Room> BottomSide {get;set;}
    public List<Room> TopSide {get;set;}

    public PreBuiltRooms()
    {
        LeftTopCorner = AddRoomsToLeftTop();
        // O.s.v
    }

    //En metod för att kunna lägga till alla rum eller det måste nog bli manuellt.

    public List<Room> AddRoomsToLeftTop()
    {
        List<Room> rooms = new()
        {
            /* 
            new CornerRoom(),
            new CornerRoom(),
            */
        };
        return rooms;
    }
}