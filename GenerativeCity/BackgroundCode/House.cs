using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerativeCity
{
    class House : CityStructure
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

        public override double DefaultCenterBias
        {
            get
            {
                return 0.1;
            }
        }

        public House(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion, double promotionMultiplierOnUpgradeType, double centerBias) : base(xIndex, yIndex, randomChancePromotion, randomChanceDemotion, promotionMultiplierOnUpgradeType, centerBias)
        {
        }

        public House(int xIndex, int yIndex) : base(xIndex, yIndex)
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
            double randomChanceWithCommercialBuildingNear = cityMap.CountPercentTypeAround(XIndex, YIndex, 1, typeof(CommercialBuilding));
            double randomChanceWithHighRiseFar = cityMap.CountPercentTypeAround(XIndex, YIndex, 5, typeof(HighRise));
            double randomChanceWithSurroundingBias;
            if ((randomChanceWithCommercialBuildingFar > 0.1) )
            {
                randomChanceWithSurroundingBias = 0;
            }
            else
            {
                randomChanceWithSurroundingBias = 3 * randomChanceWithCommercialBuildingNear + randomChanceWithHouse;
            }
            if (randomChanceWithHighRiseFar > 0.0001)
            {
                randomChanceWithSurroundingBias = 0.5;
            }

            double percentCenterBias = CalculateCenterBias(xMiddle, yMiddle, 4, 1);
            double randomOddsOfPromotionCommercial = (randomChanceWithSurroundingBias * PromotionMultiplierOnUpgradeType) + RandomChancePromotion;
            randomOddsOfPromotionCommercial = randomOddsOfPromotionCommercial * percentCenterBias;
            if (randVal < Int32.MaxValue * randomOddsOfPromotionCommercial)
            {
                CityStructure newStructure = (CityStructure)Activator.CreateInstance(typeof(CommercialBuilding), parameters);
                cityMap[XIndex, YIndex] = newStructure;
            }
        }
    }
}
