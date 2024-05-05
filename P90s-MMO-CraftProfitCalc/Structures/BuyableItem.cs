namespace P90ez.CraftProfitCalc.Structures
{
    public class BuyableItem : Item
    {
        public BuyableItem() { }    
        public BuyableItem(string Name, double Price) { this.Name = Name; this.Price = Price; }
        public double Price { get; set; }
    }
}
