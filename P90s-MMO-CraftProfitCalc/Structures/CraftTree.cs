using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class CraftTree
    {
        public CraftTreeNode Item = new CraftTreeNode();

        public double TotalCost { 
            get {
                if (Item.CraftingCost < Item.BuyCost) return Item.CraftingCost;
                else return Item.BuyCost; 
            } 
        }

        public double Margin { get { return Item.BuyCost - TotalCost; } }

        public bool IsBuyingCheaper { get { return Item.CraftingCost < Item.BuyCost; } }


        //--- Conversions ---
        public static implicit operator CraftTree(CraftTreeNode Node)
        {
            return new CraftTree() { Item = Node };
        }
    }
}
