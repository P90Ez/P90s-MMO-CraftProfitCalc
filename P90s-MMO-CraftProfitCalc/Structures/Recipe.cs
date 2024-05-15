using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public class Recipe
    {
        public List<ItemStack> Output { get; set; } = new List<ItemStack>();
        public List<ItemStack> Input { get; set; } = new List<ItemStack>();
    }
}
