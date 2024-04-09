namespace _Project.Scripts.Gameplay.GameGrid
{
    public static class GridParser
    {
        public static GridModel FromData(string data)
        {
            int sizeX = 0;
            int sizeY = 0;
            int[,] arr;
            
            string[] lines = data.Split("\n");
            //Get size
            string[] sizeData = lines[0].Split(" ");
            sizeX = int.Parse(sizeData[0]);
            sizeY = int.Parse(sizeData[1]);

            arr = new int[sizeX, sizeY];

            for (int i = 1; i < lines.Length; i++)
            {
                string[] rowData = lines[i].Split(" ");
                for (int j = 0; j < rowData.Length; j++)
                {
                    arr[j, sizeY - i] = int.Parse(rowData[j]);
                }
            }

            return new GridModel(arr);
        }

        public static string ToData(GridModel gridModel)
        {
            string text = "";
            text += gridModel.SizeX + " ";
            text += gridModel.SizeY + "\n";
            
            for (int j = gridModel.SizeY - 1; j >= 0; j--)
            {
                for (int i = 0; i < gridModel.SizeX; i++)
                {
                    text += gridModel.Get(i, j);
                    if (i < gridModel.SizeX - 1)
                    {
                        text += " ";
                    }
                }

                if (j > 0)
                {
                    text += "\n";
                }
            }
            return text;
        }
    }
}