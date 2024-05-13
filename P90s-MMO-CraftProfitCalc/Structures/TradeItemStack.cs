using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class TradeItemStack : TradeItem, IItemStack
    {
        public TradeItemStack(string Name, double BuyCost, double CraftingCost, uint Amount) : base(Name, BuyCost, CraftingCost) { this.Amount = Amount; }
        public TradeItemStack(TradeItem BaseItem, uint Amount) : base(BaseItem.Name, BaseItem.BuyCost, BaseItem.CraftingCost) { this.Amount = Amount; }

        public uint Amount { get; set; }

        /// <summary>
        /// The total cost of crafting the required amount of this item.
        /// </summary>
        public double TotalCraftingCost { get => CraftingCost * Amount; }

        /// <summary>
        /// The total cost of buying the required amount of this item.
        /// </summary>
        public double TotalBuyCost { get => BuyCost * Amount; }
    }
}
