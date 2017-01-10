using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerativeCity
{
    class City
    {
        public const double initialPromotionChance = 0.1;
        public const double initialDemotionChance = 0;
        public CityMap cityMap;

        public City(int xSize, int ySize)
        {
            cityMap = new CityMap(xSize, ySize, initialPromotionChance, initialDemotionChance);
        }

        public void Step()
        {
            foreach(CityStructure structure in cityMap.getCityStructureArr())
            {
                structure.Step(cityMap);
            }
        }
    }
}
