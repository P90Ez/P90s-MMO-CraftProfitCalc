using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class CraftTreeNode : ItemStack
    {
        public CraftTreeNode() { }
        public CraftTreeNode(ItemStack Stack)
        {
            this.Name = Stack.Name;
            this.Amount = Stack.Amount;
        }
        public CraftTreeNode(Item Item)
        {
            this.Name = Item.Name;
        }

        /// <summary>
        /// The cost of crafting this item.
        /// </summary>
        public double CraftingCost { get; set; } = 0;

        /// <summary>
        /// The cost of buying this item.
        /// </summary>
        public double BuyCost { get; set; } = 0;

        /// <summary>
        /// Indicates if this item was found in the list of buyable items.
        /// </summary>
        public bool IsBuyable { get; set; }

        /// <summary>
        /// Is null when buying is cheaper than crafting.
        /// Contains all items needed for crafting this item.
        /// </summary>
        public List<CraftTreeNode>? InputItems { get; set; } = null;
    }
}
