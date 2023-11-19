using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Search
{
    internal class clsSearchLogic{

        public void searchInvoice()
        {
            //this method will actually search the invoices list using the attributes supplied from the UI. Will probably make a sub-list that will
            //be built in a loop that will grab all invoices that match the required attributes. This method will probably need some overrides, so
            //that the user is not required to supply all of the requested details every single time. This will allow me to return lists as long as
            //all of them, and as short as only one. Then, it will be returned and displayed in the UI.
        }

        public void editInvoice()
        {
            //The UI will supply some data from a pop-up form for editing an invouice, this method will find it in the list and DB and apply the neseccary changes
        }

        public void getInvoiceNumbers()
        {
            //this will supply the invoice numbers to the UI using a list
        }

        public void getInvoiceTotals()
        {
            //this wll supply the invoice totals to the UI using a list
        }

        public void getInvoiceDate()
        {
            //this will supply the invoice dates to the UI using a list
        }

        public void getInvoiceList()
        {
            //this important method will supply the actual list of invoices themselves for alterationa and display.
            //This will come straight from the clsSearchSQL class
        }
    }
}
