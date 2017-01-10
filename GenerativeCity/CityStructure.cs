using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerativeCity
{
    abstract class CityStructure
    {
        public int XIndex { get; }
        public int YIndex { get; }

        public double RandomChancePromotion { get; set; }
        public double RandomChanceDemotion { get; set; }

        public abstract double DefaultRandomChancePromotion { get; }
        public abstract double DefaultRandomChanceDemotion { get; }

        public abstract Type promotionType { get; }
        public abstract Type demotionType { get; }



        public CityStructure(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion)
        {
            XIndex = xIndex;
            YIndex = yIndex;
            RandomChancePromotion = randomChancePromotion;
            RandomChanceDemotion = randomChanceDemotion;
        }

        public void Step(CityMap cityMap)
        {
            object[] parameters = { XIndex, YIndex, DefaultRandomChancePromotion, DefaultRandomChanceDemotion };

            int randVal = cityMap.rand.Next();
            double randomChanceWithHouses = cityMap.CountPercentTypeAround(XIndex, YIndex, 1, promotionType);
            double randomOddsOfPromotion = randomChanceWithHouses + RandomChancePromotion;
            if (randVal < Int32.MaxValue * randomOddsOfPromotion)
            {
                CityStructure newStructure = (CityStructure)Activator.CreateInstance(promotionType, parameters);
                cityMap[XIndex, YIndex] = newStructure;
            }
        }
    }
}
