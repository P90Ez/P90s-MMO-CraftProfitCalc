using System.Collections.Generic;

namespace P90ez.CraftProfitCalc.Structures
{ 
    public class Calculator
    {
        internal readonly List<BuyableItem> BuyableItems;
        internal readonly List<Recipe> Recipes;

        public Calculator(in List<BuyableItem> BuyableItems, in List<Recipe> Recipes) 
        { 
            this.BuyableItems = BuyableItems;
            this.Recipes = Recipes;
        }

        public CraftTree? Craft(ItemStack Stack)
        {
            CraftTreeNode StartNode = new CraftTreeNode(Stack);

            bool IsCraftable = IsItemCraftable(Stack);
            bool IsBuyable = IsItemBuyable(Stack);
            StartNode.IsBuyable = IsBuyable;

            if (!IsBuyable && IsCraftable) return null;
            if (!IsCraftable)
            {
                StartNode.BuyCost = GetBuycost(Stack);
                return StartNode;
            }

            CraftRecursive(ref StartNode);

            return StartNode;
        }

        //--- Functions needed for calculation

        /// <summary>
        /// Generates the crafting tree using the node recursive.
        /// </summary>
        private void CraftRecursive(ref CraftTreeNode CurrentNode)
        {
            var Item = new Item(CurrentNode.Name);
            var UseableRecipes = Recipes.Where(x => x.Output.Contains(Item));

            if(UseableRecipes == null || UseableRecipes.Count() == 0) return;

            List<CraftTreeNode> CraftingPaths = new List<CraftTreeNode>();
            foreach(Recipe Recipe in UseableRecipes) //calculate for each possibile path
            {
                CraftTreeNode TmpNode = CurrentNode;
                TmpNode.InputItems = new List<CraftTreeNode>();

                foreach(ItemStack RequiredItem in Recipe.Input) //calc each required item and add it to input
                {
                    CraftTreeNode SubNode = new CraftTreeNode(RequiredItem);
                    SubNode.BuyCost = GetBuycost(RequiredItem);
                    CraftRecursive(ref SubNode);

                    if(SubNode.BuyCost < SubNode.CraftingCost) //remove subnodes when buying is cheaper than crafting
                    {
                        SubNode.InputItems = null;
                    }

                    TmpNode.CraftingCost += SubNode.CraftingCost; //sum up crafting costs
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
            var Items = BuyableItems.Where(Buyables => Buyables == Item);

            if(Items == null || Items.Count() == 0) return -1;
            return Items.First().Price;
        }

    }
}
