using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Business
{
    public class Promotion_2Per1 : PromotionBase
    {
        public int QuantityItem { get; set; }
        public int PlusItem { get; set; }
        public double Price { get; set; }
        public double Percent { get; set; }

        public Promotion_2Per1()
        {           
            PromotionName= "Pague 1 e Leve 2";
            QuantityItem = 1;
            PlusItem     = 1;
        }
       
    }
}
