class MapFactory
{
    public static Tile[,][,] LayoutPredefinedRooms(List<Room> leftTopCorner, List<Room> rightTopCorner, List<Room> leftBottomCorner, List<Room> rightBottomCorner)
    {
        return null; 
        //To Be Continued...
    }

    public static Tile[,] FlattenRoomArray(Tile [,][,] map)
    {
        //Förutsatt att alla rooms har lika många rader och kolumner då detta ska bygga den nya mappen korrekt med arraystorlek.
        int nrRowsInMap = map.GetLength(0) * map[0,0].GetLength(0); // Antal rader i en array med room element * antal rader i room.
        int nrColsInMap = map.GetLength(1) * map[0,0].GetLength(1); // Antal kolumner i en array med room element * antal kolumner i room.

        Tile[,] useMap = new Tile[nrRowsInMap+1,nrColsInMap+1]; //Lägger till en rad och en kolumn i nya mappen för att det ska bli enheligt vid utskrift pga hur Rooms är strukturerat 

        for (int row = 0; row < map.GetLength(0); row++) //Lopar igenom varje rad i jagged 2d array
        {
            for (int col = 0; col < map.GetLength(1); col++) // När vi är inne på rad i, så Loopar vi igenom varje kolumn i jagged 2d array
            {
                //Och i varje cell plats i och j, är det också en 2d array som vi ska gå igenom och skriva ut
                int nrRowsInRoom = map[row,col].GetLength(0);
                int nrColsInRoom = map[row,col].GetLength(1);
                for (int k = 0; k < nrRowsInRoom; k++) // på aktuell plats[i,j], så går vi igenom dess 2d arrays rader 
                {
                    for (int l = 0; l < nrColsInRoom; l++) // och för varje rad så går vi igenom alla kolumner
                    {
                        int y = row * nrRowsInRoom + k;  // vi tar varje rad av Rum och multiplicerar det med antal rows i varje rum för och adderar aktuell rad i rummet för att få rätt y position.
                        int x = col * nrColsInRoom + l;
                        //Första iterationen så kommer det stå map[0,0][0,0]. Det betyder att vi accessar den första tilen i de första rummet. Denna vill vi kopiera in till den nya mappen.
                        useMap[y,x] = map[row,col][k,l];
                    }
                }
            }
        }
        for(int row = 0; row < useMap.GetLength(0); row++)
        {
            for (int col= 0; col < useMap.GetLength(1); col++)
            {
                if (row == 0 || row == useMap.GetLength(0)-1 || col == 0 || col == useMap.GetLength(1)-1)
                {
                    useMap[row,col] = new ObstacleTile();
                }
            }
        }
        return useMap;
    }
}