using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90ez.CraftProfitCalc.Structures
{
    public interface IItemStack : IItem
    {
        public uint Amount { get; set; }
    }
}
