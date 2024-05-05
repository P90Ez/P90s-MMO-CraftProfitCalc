namespace P90ez.CraftProfitCalc.Structures
{
    public class ItemStack : Item
    {
        public ItemStack() { }
        public ItemStack(string Name, uint Amount) { this.Name = Name; this.Amount = Amount; }

        public uint Amount { get; set; } = 0;
    }
}
