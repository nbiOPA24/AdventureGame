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

        Tile[,] useMap = new Tile[nrRowsInMap,nrColsInMap];

        for (int i = 0; i < map.GetLength(0); i++) //Lopar igenom varje rad i jagged 2d array
        {
            for (int j = 0; j < map.GetLength(1); j++) // När vi är inne på rad i, så Loopar vi igenom varje kolumn i jagged 2d array
            {
                //Och i varje cell plats i och j, är det också en 2d array som vi ska gå igenom och skriva ut
                int nrRowsInRoom = map[i,j].GetLength(0);
                int nrColsInRoom = map[i,j].GetLength(1);
                for (int k = 0; k < nrRowsInRoom; k++) // på aktuell plats[i,j], så går vi igenom dess 2d arrays rader 
                {
                    for (int l = 0; l < nrColsInRoom; l++) // och för varje rad så går vi igenom alla kolumner
                    {
                        int y = i * nrRowsInRoom + k;
                        int x = j * nrColsInRoom + l;
                        //Första iterationen så kommer det stå map[0,0][0,0]. Det betyder att vi accessar den första tilen i de första rummet. Denna vill vi kopiera in till den nya mappen.
                        useMap[y,x] = map[i,j][k,l];
                    }
                }
            }
        }

        return useMap;
    }
}