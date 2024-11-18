class MapFactory
{
    public static Tile[,] GenerateMap(int rows, int cols)
    {
        Tile[,] countRoom = new CR().Room; //Skapar detta rum för att använda det till att beräkna storleken av rum.

        //Förutsatt att alla rooms har lika många rader och kolumner då detta ska bygga den nya mappen korrekt med arraystorlek.
        int nrRowsInRoom = countRoom.GetLength(0); // Tar reda på hur många rader denna array har.
        int nrColsInRoom = countRoom.GetLength(1); // Tar reda på hur många columner denna array har.
        int nrRowsInMap = rows * nrRowsInRoom; // Antal rader i en array med room element * antal rader i room.
        int nrColsInMap = cols * nrColsInRoom; // Antal kolumner i en array med room element * antal kolumner i room.

        Tile[,] useMap = new Tile[nrRowsInMap+1,nrColsInMap+1]; //Lägger till en rad och en kolumn i nya mappen för att det ska bli enheligt vid utskrift pga hur Rooms är strukturerat 

        Random random = new();
        int startPlacement = random.Next(0,rows);
        int goalPlacement = random.Next(0,rows);


        for (int row = 0; row < rows; row++) //Lopar igenom varje rad i jagged 2d array
        {
            for (int col = 0; col < cols; col++) // När vi är inne på rad i, så Loopar vi igenom varje kolumn i jagged 2d array
            {
                Tile[,] room = (row == startPlacement && col == 0) ? new SR().Room : new CR().Room;  // Tilldelar startrummet en plats.

                if (row == goalPlacement && col == cols-1)
                {
                    room = new GR().Room;
                } 

                for (int k = 0; k < nrRowsInRoom; k++) // på aktuell plats[i,j], så går vi igenom dess 2d arrays rader 
                {
                    for (int l = 0; l < nrColsInRoom; l++) // och för varje rad så går vi igenom alla kolumner
                    {
                        int y = row * nrRowsInRoom + k;  // vi tar varje rad av Rum och multiplicerar det med antal rows i varje rum för och adderar aktuell rad i rummet för att få rätt y position.
                        int x = col * nrColsInRoom + l;

                        useMap[y,x] = room[k,l];
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
        for(int row = 0; row < useMap.GetLength(0); row++)
        {

            for (int col= 0; col < useMap.GetLength(1); col++)
            {
                if (useMap[row,col] is DoorTile) 
                {
                    
                    int testX;
                    int testY;
                    while (true)
                    {
                        do
                        {
                            testX = random.Next(-20,20);
                            testY = random.Next(-20,20);


                        } while 
                            (
                            (row+testY) > useMap.GetLength(0)-1 || (row+testY) < 0  
                            || 
                            (col+testX) > useMap.GetLength(1)-1 || (col+testX) < 0
                            );

                        if (useMap[row+testY,col+testX] is EmptyTile)
                        {
                            useMap[row+testY,col+testX] = new KeyTile();
                            break;
                        } 
                                                  
                    }

                }                                                           
            }                                                            
      
        }
        return useMap;
    }
}