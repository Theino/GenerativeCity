using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerativeCity
{
    class CityMap
    {

        private CityStructure[,] cityMap;
        public Random rand = new Random();
        public int XSize;
        public int YSize;

        public CityStructure this[int x, int y]
        {
            get
            {
                return cityMap[x, y];
            }
            set
            {
                cityMap[x, y] = value;
            }
        }

        public CityStructure[,] getCityStructureArr()
        {
            return cityMap;
        }


        public CityMap(int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            cityMap = new CityStructure[xSize, ySize];
            for (int i = 0; i < xSize; i++)
            {
                for(int j = 0; j < ySize; j++)
                {
                    cityMap[i, j] = new Empty(i, j);
                }
            }
        }

        public double CountPercentTypeAround(int xIndex, int yIndex, int distance, Type t)
        {
            int count = 0;
            int total = 0;
            double percent;
            for (int i = xIndex - distance; i < xIndex + distance; i++)
            {
                for (int j = yIndex - distance; j < yIndex + distance; j++)
                {
                    bool isNotOrigin = i != xIndex || j != yIndex;
                    if (isNotOrigin)
                    {
                        if (IsSameType(i, j, t))
                        {
                            count++;
                        }
                        total++;
                    }
                }
            }
            percent = (double)count / (double)total;
            return percent;
        }

        private bool IsSameType(int xIndex, int yIndex, Type t)
        {
            try
            {
                if (xIndex < XSize && yIndex < YSize)
                {
                    if (xIndex >= 0 && yIndex >= 0)
                    {
                        if (cityMap[xIndex, yIndex].GetType() == t)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }
    }
}
