namespace P90ez.CraftProfitCalc
{
    public class Item
    {
        public Item() { }
        public Item(string Name) { this.Name = Name; }
        public string Name { get; set; } = String.Empty;

        //--- overrides ---
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Item) return false;

            return ((Item)obj).Name == this.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
