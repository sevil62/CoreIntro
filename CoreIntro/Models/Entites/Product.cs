using System.Collections;
using System.Collections.Generic;

namespace CoreIntro.Models.Entites
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public int CategoryID { get; set; }


        //Relation Properties

        public virtual Category Category { get; set; }
        public virtual IList<OrderDetail>OrderDetails  { get; set; }
    }
}
