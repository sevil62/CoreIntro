namespace CoreIntro.Tools
{
    public class CartItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal 
        {
            get
            {
                return Amount * Price;
            }
        }

        public CartItem()
        {
            Amount++;
        }

    }
}
