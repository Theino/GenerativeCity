using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerativeCity
{
    class Empty : CityStructure
    {
        public override double DefaultRandomChancePromotion
        {
            get
            {
                return 0.1;
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

        public Empty(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion, double promotionMultiplierOnUpgradeType, double centerBias) : base(xIndex, yIndex, randomChancePromotion, randomChanceDemotion, promotionMultiplierOnUpgradeType, centerBias)
        {
        }

        public Empty(int xIndex, int yIndex) : base(xIndex, yIndex)
        {
            RandomChancePromotion = DefaultRandomChancePromotion;
            RandomChanceDemotion = DefaultRandomChanceDemotion;
            PromotionMultiplierOnUpgradeType = DefaultPromotionMultiplierOnUpgradeType;
        }

        public override void Step(CityMap cityMap)
        {
            object[] parameters = { XIndex, YIndex };
            int randVal = cityMap.rand.Next();
            double housePromoteProbability = 1;
            housePromoteProbability = CalculateSurroundingHouseEffectOnHousePromote(cityMap, housePromoteProbability);
            housePromoteProbability = CalculateSurroundingCommercialEffectOnHousePromote(cityMap, housePromoteProbability);
            housePromoteProbability = CalculateCenterEffectOnHousePromote(cityMap, housePromoteProbability);
            if (randVal < Int32.MaxValue * housePromoteProbability)
            {
                CityStructure newStructure = (CityStructure)Activator.CreateInstance(typeof(House), parameters);
                cityMap[XIndex, YIndex] = newStructure;
            }
        }

        private double CalculateCenterEffectOnHousePromote(CityMap cityMap, double housePromoteProbability)
        {
            int xMiddle = cityMap.XSize / 2;
            int yMiddle = cityMap.YSize / 2;
            double contributingEffect;
            contributingEffect = CalculateCenterBias(xMiddle, yMiddle, 4, 1);
            if (housePromoteProbability < 0.001)
            {
                return contributingEffect;
            }
            else
            {
                return contributingEffect / 10.0;
            }
        }

        private double CalculateSurroundingCommercialEffectOnHousePromote(CityMap cityMap, double housePromoteProbability)
        {
            double contributingEffect = cityMap.CountPercentTypeAround(XIndex, YIndex, 3, typeof(CommercialBuilding));
            return contributingEffect * housePromoteProbability;
        }

        private double CalculateSurroundingHouseEffectOnHousePromote(CityMap cityMap, double housePromoteProbability)
        {
            double contributingEffect = cityMap.CountPercentTypeAround(XIndex, YIndex, 3, typeof(House));
            return contributingEffect * housePromoteProbability;
        }
    }
}
