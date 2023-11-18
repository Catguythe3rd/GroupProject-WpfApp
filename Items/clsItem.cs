using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItem
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal Cost { get; set; }

        // The code wasn't working before because the variables didn't have { get; set; } next to them.
        // THAT'S IT. I have no idea why this change worked, but the internet suggested it.

        public clsItem(string itemCode, string itemDesc, decimal cost)
        {
            ItemCode = itemCode;
            ItemDesc = itemDesc;
            Cost = cost;
        }
    }
}
