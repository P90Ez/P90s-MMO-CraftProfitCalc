using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public interface ICraftableItem : IItem
    {
        /// <summary>
        /// The cost of crafting this item.
        /// </summary>
        public double CraftingCost { get; }

        /// <summary>
        /// Indicates if this item was found in the list of craftable items.
        /// </summary>
        public bool IsCraftable { get; }
    }
}
