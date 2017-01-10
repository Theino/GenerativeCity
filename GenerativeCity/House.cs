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
    }
}
