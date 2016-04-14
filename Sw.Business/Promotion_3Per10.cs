using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Business
{
    public class Promotion_3Per10 : PromotionBase
    {
        public int QuantityItem { get; set; }       
        public decimal Price { get; set; }
       
        public Promotion_3Per10()
        {
            PromotionName = "3 por 10 reais";
            QuantityItem  = 3;
            Price         = 10;
        }
    }
}
