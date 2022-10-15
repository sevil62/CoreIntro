using CoreIntro.Models.Entites;
using System.Collections.Generic;

namespace CoreIntro.VMClasses
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
