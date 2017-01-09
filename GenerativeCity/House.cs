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
                return 0.8;
            }
        }
        public override double DefaultRandomChanceDemotion
        {
            get
            {
                return 0.1;
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
        public House(int xIndex, int yIndex, double randomChancePromotion, double randomChanceDemotion) : base(xIndex, yIndex, randomChancePromotion, randomChanceDemotion)
        {
        }
    }
}
