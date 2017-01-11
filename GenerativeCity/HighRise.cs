using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerativeCity
{
    class HighRise : CityStructure
    {
        public override double DefaultRandomChancePromotion
        {
            get
            {
                return 0;
            }
        }
        public override double DefaultRandomChanceDemotion
        {
            get
            {
                return 0.1;
            }
        }

        public override double DefaultPromotionMultiplierOnUpgradeType
        {
            get
            {
                return 0.2;
            }
        }

        public override Type promotionType
        {
            get
            {
                return typeof(HighRise);
            }
        }

        public override Type demotionType
        {
            get
            {
                return typeof(CommercialBuilding);
            }
        }

        public override double DefaultCenterBias
        {
            get
            {
                return 0.1;
            }
        }

        public HighRise(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion, double promotionMultiplierOnUpgradeType, double centerBias) : base(xIndex, yIndex, randomChancePromotion, randomChanceDemotion, promotionMultiplierOnUpgradeType, centerBias)
        {
        }

        public HighRise(int xIndex, int yIndex) : base(xIndex, yIndex)
        {
            RandomChancePromotion = DefaultRandomChancePromotion;
            RandomChanceDemotion = DefaultRandomChanceDemotion;
            PromotionMultiplierOnUpgradeType = DefaultPromotionMultiplierOnUpgradeType;
        }

        public override void Step(CityMap cityMap)
        {
            object[] parameters = { XIndex, YIndex };
            int xMiddle = cityMap.XSize / 2;
            int yMiddle = cityMap.YSize / 2;
            int randVal = cityMap.rand.Next();
            double randomChanceWithHouse = cityMap.CountPercentTypeAround(XIndex, YIndex, 4, typeof(House));
            double randomChanceWithCommercialBuildingFar = cityMap.CountPercentTypeAround(XIndex, YIndex, 4, typeof(CommercialBuilding));
            double randomChanceWithHighRiseNear = cityMap.CountPercentTypeAround(XIndex, YIndex, 1, typeof(HighRise));
            double randomChanceWithEmptyFar = cityMap.CountPercentTypeAround(XIndex, YIndex, 4, typeof(Empty));
            double randomChanceWithSurroundingBias;
            if (randomChanceWithEmptyFar < 0.001)
            {
                randomChanceWithSurroundingBias = 0;
            }
            else
            {
                randomChanceWithSurroundingBias = 3 * randomChanceWithHighRiseNear + randomChanceWithHouse;
            }

            double percentCenterBias = calculateCenterBias(xMiddle, yMiddle, 4, 1);
            double randomOddsOfPromotion = (randomChanceWithSurroundingBias * PromotionMultiplierOnUpgradeType) + RandomChancePromotion;
            randomOddsOfPromotion = randomOddsOfPromotion * percentCenterBias;
            if (randVal < Int32.MaxValue * randomOddsOfPromotion)
            {
                CityStructure newStructure = (CityStructure)Activator.CreateInstance(promotionType, parameters);
                cityMap[XIndex, YIndex] = newStructure;
            }
        }
    }
}