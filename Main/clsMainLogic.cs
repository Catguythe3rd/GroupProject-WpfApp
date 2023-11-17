using GroupProject_WpfApp.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Main
{
    internal class clsMainLogic
    {
        private int invoiceNum;
        private DateOnly invoiceDate;
        private decimal invoiceTotal;

        public int ID
        {
            get { return invoiceNum; }
            set { invoiceNum = value; }

        }
        public DateOnly InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }

        public decimal InvoiceTotal
        {
            get { return invoiceTotal; }
            set { invoiceTotal = value; }
        }


        public override string ToString()
        {
            return "ID: "+ID.ToString() + " Date:" + invoiceDate + " Total: $" + invoiceTotal;
        }

    }
}
