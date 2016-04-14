using Sw.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Business
{
    public class PromotionBase
    {
        public string PromotionName { get; set; }  
        public static List<PromotionBase> GetPromotions()
        {
            var promotion_2per1  = new Promotion_2Per1();
            var promotion_3per10 = new Promotion_3Per10();
            var promotionsList   = new List<PromotionBase>
            {
                new PromotionBase {
                    PromotionName ="Nenhuma"
                },
                new PromotionBase {
                    PromotionName = promotion_2per1.PromotionName               
                },
                new PromotionBase
                {
                    PromotionName = promotion_3per10.PromotionName                    
                }
            };
            return promotionsList;
        }        

        public static List<ProductCheckout> SetRulesPromotion(List<dynamic> listProduct)
        {          
            var listCheckOutProducts = new List<ProductCheckout>();
            Sw.Data.SwContext db = new SwContext();
            foreach ( var item in listProduct )
            {
                var productId      = (int) item.Id;
                var product        = db.Products.FirstOrDefault(p => p.Id == productId);
                switch ( (string)item.Promotion )
                {
                    case "Pague 1 e Leve 2":
                        var promotion2Per1 = new Promotion_2Per1();                       
                        if( item.Count >= promotion2Per1.QuantityItem )
                        {
                            var validProdutsPromotion = (int)(item.Count / promotion2Per1.QuantityItem);
                            for ( int i = 0; i < validProdutsPromotion; i++ )
                            {
                                listCheckOutProducts.Add(new ProductCheckout { Id = product.Id, Name = product.Name, Price = product.Price, Promotion = product.Promotion,Count=promotion2Per1.QuantityItem  + promotion2Per1.PlusItem});
                            }                            
                        }       
                        break;
                    case "3 por 10 reais":
                        var promotion3Per10 = new Promotion_3Per10();
                        if ( item.Count >= promotion3Per10.QuantityItem )
                        {
                            var validProdutsPromotion = (int)(item.Count / promotion3Per10.QuantityItem);
                            for ( int i = 0; i < validProdutsPromotion; i++ )
                            {
                                listCheckOutProducts.Add(new ProductCheckout { Id = product.Id, Name = product.Name, Price = promotion3Per10.Price, Promotion = product.Promotion, Count=promotion3Per10.QuantityItem });
                            }
                            var missingItem = item.Count -(promotion3Per10.QuantityItem * validProdutsPromotion);
                            if(validProdutsPromotion != item.Count )
                            {
                                for ( int i = 0; i < missingItem; i++ )
                                {
                                    listCheckOutProducts.Add(new ProductCheckout { Id = product.Id, Name = product.Name, Price = product.Price, Promotion = "" });
                                }                               
                            }                            
                        }
                        else
                        {
                            for ( int i = 0; i < item.Count; i++ )
                            {
                                listCheckOutProducts.Add(new ProductCheckout { Id = product.Id, Name = product.Name, Price = product.Price, Promotion = "",Count=1 });
                            }
                        }
                        break;
                    default:
                        for ( int i = 0; i < item.Count; i++ )
                        {
                            listCheckOutProducts.Add(new ProductCheckout { Id = product.Id, Name = product.Name, Price = product.Price, Promotion = "",Count= 1});
                        }
                        break;
                }
                
            }
            return listCheckOutProducts;
        }
    }
}
