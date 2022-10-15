using System.Collections;
using System.Collections.Generic;

namespace CoreIntro.Models.Entites
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string Discription { get; set; }

        //Relation Properties
        public virtual IList<Product>Products { get; set; }
    }
}
