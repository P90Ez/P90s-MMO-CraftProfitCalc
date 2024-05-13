using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class TradeItem : BuyableItem, ICraftableItem
    {
        public TradeItem(string Name, double BuyCost, double CraftingCost) : base(Name, BuyCost) { this.CraftingCost = CraftingCost; }

        public TradeItem(BuyableItem Item, double CraftingCost = 0) : base(Item.Name, Item.BuyCost) { }

        public double CraftingCost { get; }

        public bool IsCraftable { get => CraftingCost > 0; }
    }
}
