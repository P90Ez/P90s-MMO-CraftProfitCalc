namespace P90ez.CraftProfitCalc.Structures
{
    public class ItemStack : Item, IItemStack
    {
        public ItemStack(string Name, uint Amount) : base(Name) { this.Amount = Amount; }

        public uint Amount { get; set; }
    }
}
