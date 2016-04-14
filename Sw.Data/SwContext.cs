using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Data
{
   public class SwContext:DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
    }
}
