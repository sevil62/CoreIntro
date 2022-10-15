namespace CoreIntro.Models.Entites
{
    public class OrderDetail:BaseEntity
    {
        public int ProductID { get; set; }
        public int OrderID { get; set; }

        //Relation Properties
        public virtual Order Order { get; set; }
        public virtual  Product Product { get; set; }
    }
}
