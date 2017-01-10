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
        public double PromotionMultiplierOnUpgradeType { get; set; }
        public double CenterBias { get; }

        public abstract double DefaultRandomChancePromotion { get; }
        public abstract double DefaultRandomChanceDemotion { get; }
        public abstract double DefaultPromotionMultiplierOnUpgradeType { get; }
        public abstract double DefaultCenterBias { get; }

        public abstract Type promotionType { get; }
        public abstract Type demotionType { get; }



        public CityStructure(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion, double promotionMultiplierOnUpgradeType, double centerBias)
        { 
            XIndex = xIndex;
            YIndex = yIndex;
            RandomChancePromotion = randomChancePromotion;
            RandomChanceDemotion = randomChanceDemotion;
            PromotionMultiplierOnUpgradeType = promotionMultiplierOnUpgradeType;
            CenterBias = centerBias;
        }

        public CityStructure(int xIndex, int yIndex)
        {
            XIndex = xIndex;
            YIndex = yIndex;
            RandomChancePromotion = DefaultRandomChancePromotion;
            RandomChanceDemotion = DefaultRandomChanceDemotion;
            PromotionMultiplierOnUpgradeType = DefaultPromotionMultiplierOnUpgradeType;
            CenterBias = DefaultCenterBias;
        }

        public void Step(CityMap cityMap)
        {
            object[] parameters = { XIndex, YIndex };
            int xMiddle = cityMap.XSize / 2;
            int yMiddle = cityMap.YSize / 2;
            int randVal = cityMap.rand.Next();
            double randomChanceWithSurrounding = cityMap.CountPercentTypeAround(XIndex, YIndex, 3, promotionType);
            double percentCenterBias = calculateCenterBias(xMiddle, yMiddle);
            double randomOddsOfPromotion = (randomChanceWithSurrounding * PromotionMultiplierOnUpgradeType) + RandomChancePromotion;
            randomOddsOfPromotion = randomOddsOfPromotion * percentCenterBias;
            if (randVal < Int32.MaxValue * randomOddsOfPromotion)
            {
                CityStructure newStructure = (CityStructure)Activator.CreateInstance(promotionType, parameters);
                cityMap[XIndex, YIndex] = newStructure;
            }
        }

        private double calculateCenterBias(int xMiddle, int yMiddle)
        {
            double centerBiasX = Math.Abs(XIndex - xMiddle);
            double centerBiasY = Math.Abs(YIndex - yMiddle);
            double percentOffCenter = (centerBiasX + centerBiasY) / (xMiddle + yMiddle);
            double percentOnCenterExp = Math.Pow(1 - percentOffCenter, 4);
            return percentOnCenterExp;
        }
    }
}
