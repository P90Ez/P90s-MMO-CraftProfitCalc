using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class CraftTree
    {
        public CraftTree(CraftTreeNode Node) { Item = Node; }

        public CraftTreeNode Item { get; }

        public double TotalCost { 
            get {
                if (Item.CraftingCost < Item.BuyCost) return Item.CraftingCost;
                else return Item.BuyCost; 
            } 
        }

        public double TotalMargin { get { return Item.BuyCost - TotalCost; } }

        public bool IsBuyingCheaper { get { return Item.BuyCost < Item.CraftingCost || !Item.IsCraftable; } }


        //--- Conversions ---
        public static implicit operator CraftTree(CraftTreeNode Node)
        {
            return new CraftTree(Node);
        }
    }
}
