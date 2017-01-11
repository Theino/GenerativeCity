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


        public abstract void Step(CityMap cityMap);


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

        public double CalculateCenterBias(int xMiddle, int yMiddle, double expConst, double scaleConst)
        {
            double centerBiasX = Math.Abs(XIndex - xMiddle) * scaleConst;
            double centerBiasY = Math.Abs(YIndex - yMiddle) * scaleConst;
            double percentOffCenter = (Math.Pow(centerBiasX,2) + Math.Pow(centerBiasY,2)) / (Math.Pow(xMiddle, 2) + Math.Pow(yMiddle, 2));
            double percentOnCenterExp = Math.Pow(1 - percentOffCenter, expConst);
            return percentOnCenterExp;
        }
    }
}
