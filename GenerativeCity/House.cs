using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Type promotionType
        {
            get
            {
                return typeof(CommercialBuilding);
            }
        }

        public override Type demotionType
        {
            get
            {
                return typeof(Empty);
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
            double randomChanceWithSurroundingBias;
            if (randomChanceWithCommercialBuildingFar > 0.1)
            {
                randomChanceWithSurroundingBias = 0;
            }
            else
            {
                randomChanceWithSurroundingBias = 3 * randomChanceWithCommercialBuildingNear + randomChanceWithHouse;
            }

            double percentCenterBias = calculateCenterBias(xMiddle, yMiddle);
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
