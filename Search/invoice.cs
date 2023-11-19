using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject_WpfApp.Items;

namespace GroupProject_WpfApp
{
    internal class invoice
    {
        int invoiceNumber { get; set; }
        DateTime invoiceDate { get; set; }
        Decimal invoiceTotal { get; set; }
        List<clsItem> items = new List<clsItem>(); 
        public invoice(int invoiceNumber, DateTime invoiceDate, Decimal invoiceTotal)
        {
            this.invoiceNumber = invoiceNumber;
            this.invoiceDate = invoiceDate;
            this.invoiceTotal = invoiceTotal;

        }

        public void getItems()
        {
            //this method will reach out and grab the items that are associated with a specific invoice. It will likely be through a SQL query
            //Maybe, instead I can call on one of the SQL classes to do this for me. I could A, make a function in my sql class to do this or
            //B, call one in the clsItemsSQL class since that one is already searching for items!
        }
    }
}
