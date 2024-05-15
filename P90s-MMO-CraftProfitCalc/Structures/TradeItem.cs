using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class TradeItem : BuyableItem, ICraftableItem
    {
        public TradeItem(string Name, double BuyCost, double CraftingCost, Recipe? Recipe = null) : base(Name, BuyCost) { this.CraftingCost = CraftingCost; this.RecipeUsed = Recipe; }

        public TradeItem(BuyableItem Item, double CraftingCost = 0, Recipe? Recipe = null) : base(Item.Name, Item.BuyCost) { this.RecipeUsed = Recipe; }

        public double CraftingCost { get; }

        public bool IsCraftable { get => CraftingCost > 0; }

        /// <summary>
        /// The recipe used to calculate the optimal crafting path/cost.
        /// </summary>
        public Recipe? RecipeUsed { get; internal set; } = null;
    }
}
