using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Business
{
    public class ProductCheckout
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public decimal Price { get; set; }     
        public string Promotion { get; set; }
        public int? Count { get; set; }
        

    }
}
