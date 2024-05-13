namespace P90ez.CraftProfitCalc.Structures
{
    public class BuyableItem : Item
    {  
        public BuyableItem(string Name, double Price) : base(Name) { this.BuyCost = Price; }
        
        /// <summary>
        /// The cost of buying this item.
        /// </summary>
        public double BuyCost { get; }

        /// <summary>
        /// Indicates if this item was found in the list of buyable items.
        /// </summary>
        public bool IsBuyable { get => BuyCost > 0; }
    }
}
