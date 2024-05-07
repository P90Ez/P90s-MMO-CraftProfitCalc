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
            CraftTreeNode StartNode = new CraftTreeNode(Stack);

            CraftRecursive(ref StartNode);

            return StartNode;
        }

        //--- Functions needed for calculation

        /// <summary>
        /// Generates the crafting tree using the node recursive.
        /// </summary>
        private void CraftRecursive(ref CraftTreeNode CurrentNode)
        {
            if (IsItemBuyable(CurrentNode))
            {
                CurrentNode.BuyCost = GetBuycost(CurrentNode);
                CurrentNode.IsBuyable = true;
            }

            var Item = new Item(CurrentNode.Name);
            var UseableRecipes = Recipes.Where(Recipe => Recipe.Output.Contains(Item));

            if(UseableRecipes == null || UseableRecipes.Count() == 0) return;
            CurrentNode.IsCraftable = true;

            List<CraftTreeNode> CraftingPaths = new List<CraftTreeNode>();
            foreach(Recipe Recipe in UseableRecipes) //calculate for each possibile path
            {
                CraftTreeNode TmpNode = CurrentNode;
                TmpNode.InputItems = new List<CraftTreeNode>();

                double TotalCraftingCost = 0;

                foreach(ItemStack RequiredItem in Recipe.Input) //calc each required item and add it to input
                {
                    CraftTreeNode SubNode = new CraftTreeNode(RequiredItem);
                    SubNode.Amount *= CurrentNode.Amount; //multiply with required amount of current item to get total amount of sub items required for crafting

                    CraftRecursive(ref SubNode);

                    if(SubNode.BuyCost < SubNode.CraftingCost) //remove subnodes when buying is cheaper than crafting
                    {
                        SubNode.InputItems.Clear();
                    }

                    if (SubNode.IsCraftable) //sum up costs
                    {
                        TotalCraftingCost += (SubNode.TotalCraftingCost < SubNode.TotalBuyCost ? SubNode.TotalCraftingCost : SubNode.TotalBuyCost); 
                    }
                    else
                    {
                        TotalCraftingCost += SubNode.TotalBuyCost;
                    }

                    TmpNode.CraftingCost = TotalCraftingCost / TmpNode.Amount; //set the crafting cost for a single unit

                    TmpNode.InputItems.Add(SubNode);
                }

                CraftingPaths.Add(TmpNode); //store path
            }

            bool FirstSet = false;
            foreach(CraftTreeNode Path in CraftingPaths) //get best path
            {
                if (!FirstSet)
                {
                    CurrentNode = Path;
                    FirstSet = true;
                }
                else if (Path.CraftingCost < CurrentNode.CraftingCost)
                {
                    CurrentNode = Path;
                }
            }
        }

        private bool IsItemBuyable(Item Item)
        {
            return BuyableItems.Contains(Item);
        }

        private bool IsItemCraftable(Item Item)
        {
            return Recipes.Where(Recipe => Recipe.Output.Contains(Item)).Any();
        }

        private double GetBuycost(Item Item)
        {
            var Items = BuyableItems.Where(Buyables => Buyables.Name == Item.Name);

            if(Items == null || Items.Count() == 0) return -1;
            return Items.First().Price;
        }

    }
}
