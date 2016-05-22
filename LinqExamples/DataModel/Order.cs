using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.DataModel
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return String.Format("Id:{0} Name:{1} Q:{2}", Id, Product.Name, Quantity);
        }
    }
}
