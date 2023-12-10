using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    internal class clsLineItems
    {
        public int InvoiceNumber { get; set; }
        public int LineItemNumber { get; set; }
        public string ItemCode { get; set; }

        public clsLineItems(int invoiceNumber)
        {
            this.InvoiceNumber = invoiceNumber;
        }

        public override string ToString()
        {
            return InvoiceNumber + " " + LineItemNumber + " " + ItemCode;
        }
    }
}
