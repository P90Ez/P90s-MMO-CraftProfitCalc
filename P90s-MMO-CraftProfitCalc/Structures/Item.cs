namespace P90ez.CraftProfitCalc.Structures
{
    public class Item : IItem
    {
        public Item(string Name) { this.Name = Name; }

        public string Name { get; }


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
