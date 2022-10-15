using System.Collections;
using System.Collections.Generic;

namespace CoreIntro.Models.Entites
{
    public class Order:BaseEntity
    {
        public string ShippedAddress { get; set; }
        public int EmployeeID { get; set; }

        //Relation Properties
        public virtual Employee Employee { get; set; }
        public virtual IList<OrderDetail>OrderDetails { get; set; }


    }
}
