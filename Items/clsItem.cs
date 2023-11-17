using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItem
    {
        public string ItemCode;
        public string ItemDesc;
        public decimal cost;

        public clsItem(string ItemCode, string ItemDesc, decimal cost)
        {

            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.cost = cost;
        }
    }
}
