using System.Collections.Generic;

namespace P90ez.CraftProfitCalc.Structures
{ 
    public class Calculator
    {
        internal readonly List<BuyableItem> BuyableItems;
        internal readonly List<Recipe> Recipes;

        /// <summary>
        /// Creates a Calculator instance.
        /// </summary>
        /// <param name="BuyableItems">A list of items which can be bought at a specific price.</param>
        /// <param name="Recipes">A list of item which can be crafted.</param>
        public Calculator(in List<BuyableItem> BuyableItems, in List<Recipe> Recipes) 
        { 
            this.BuyableItems = BuyableItems;
            this.Recipes = Recipes;
        }

        /// <summary>
        /// Calculates the optimal crafting path for the highest margin.
        /// </summary>
        /// <param name="Stack">Item to craft</param>
        /// <returns>Returns a tree containing all crafting steps or null if given item is not craftabe and buyable</returns>
        public CraftTree? Craft(ItemStack Stack)
        {
            CraftTreeNode StartNode = new CraftTreeNode(Stack.Name, 0, 0, Stack.Amount);

            CraftRecursive(ref StartNode);

            return StartNode;
        }

        //--- Functions needed for calculation ---

        /// <summary>
        /// Generates the crafting tree using the node recursive.
        /// </summary>
        private void CraftRecursive(ref CraftTreeNode CurrentNode)
        {
            //fetch price if buyable
            CurrentNode = new CraftTreeNode(new TradeItem(GetBuyableItem(CurrentNode)), CurrentNode.Amount);

            //determine valid crafting recipes for given item
            var UseableRecipes = GetUseableRecipes(CurrentNode);
            if (UseableRecipes.Count() == 0) return;

            bool FirstCraftingPathSet = false;
            foreach (Recipe Recipe in UseableRecipes) //calculate for each possibile path
            {
                List<CraftTreeNode> CraftInputItems = new List<CraftTreeNode>();
                double CraftingCostPerItem = 0;

                foreach (ItemStack RequiredItem in Recipe.Input) //calc each required item and add it to input
                {
                    uint SubAmout = RequiredItem.Amount * CurrentNode.Amount; //multiply with required amount of current item to get total amount of sub items required for crafting

                    CraftTreeNode SubNode = new CraftTreeNode(RequiredItem.Name, 0, 0, SubAmout);
                    CraftRecursive(ref SubNode);

                    if (SubNode.IsCraftable) //sum up costs
                    {
                        CraftingCostPerItem += (SubNode.CraftingCost < SubNode.BuyCost ? SubNode.CraftingCost : SubNode.BuyCost) * RequiredItem.Amount;
                    }
                    else
                    {
                        CraftingCostPerItem += SubNode.BuyCost * RequiredItem.Amount;
                    }

                    CraftInputItems.Add(SubNode);
                }

                //replace current node if this path is the cheapest to craft (or the first path)
                if ((CurrentNode.BuyCost >= CraftingCostPerItem || !CurrentNode.IsBuyable) && (CurrentNode.CraftingCost > CraftingCostPerItem || !FirstCraftingPathSet))
                {
                    FirstCraftingPathSet = true;
                    CurrentNode = new CraftTreeNode(CurrentNode.Name, CurrentNode.BuyCost, CraftingCostPerItem, CurrentNode.Amount) { InputItems = CraftInputItems };
                } 
                else if (!FirstCraftingPathSet)
                {
                    FirstCraftingPathSet = true; 
                    CurrentNode = new CraftTreeNode(CurrentNode.Name, CurrentNode.BuyCost, CraftingCostPerItem, CurrentNode.Amount); //don't store InputItems because buying is cheaper.
                }
            }
        }

        private BuyableItem GetBuyableItem(Item Item)
        {
            var Items = BuyableItems.Where(Buyables => Buyables.Name == Item.Name);

            if(Items == null || Items.Count() == 0) return new BuyableItem(Item.Name, 0); //-> item not buyable
            return Items.First();
        }

        private IEnumerable<Recipe> GetUseableRecipes(Item ItemToCraft)
        {
            return Recipes.Where(Recipe => Recipe.Output.Contains(ItemToCraft));
        }

    }
}
