using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Data
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name="Nome")]
        public string Name { get; set; }
        [Display(Name="Preço")] 
        [DataType(DataType.Currency)] 
        public decimal Price { get; set; }
        [Display(Name="Promoção")]
        public string Promotion { get; set; }
    }
}
