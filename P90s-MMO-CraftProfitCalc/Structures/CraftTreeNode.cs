using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class CraftTreeNode : TradeItemStack
    {
        public CraftTreeNode(string Name, double BuyCost, double CraftingCost, uint Amount) : base(Name, BuyCost, CraftingCost, Amount) { }
        
        public CraftTreeNode(TradeItemStack Stack) : base(Stack, Stack.Amount) { }

        public CraftTreeNode(TradeItem Item, uint Amount) : base (Item, Amount) { }

        /// <summary>
        /// Has zero entries when buying is cheaper than crafting.
        /// Contains all items needed for crafting this item.
        /// </summary>
        public List<CraftTreeNode> InputItems { get; set; } = new List<CraftTreeNode>();
    }
}
