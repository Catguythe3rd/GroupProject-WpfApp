using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GroupProject_WpfApp.Search
{
    internal class clsSearchSQL
    {


        List<invoice> lstInvoices;
        List<String> lineItems;
        clsDataAccess db = new clsDataAccess();
        List<String> items;

        /// <summary>
        /// list all items in database
        /// </summary>
        /// <returns></returns>
        public List<invoice> getInvoices()
        {
            DataSet ds = new DataSet();
            int iRet = 0;
            lstInvoices = new List<invoice>();
            ds = db.ExecuteSQLStatement("Select * FROM invoices", ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                invoice newInvoice = new invoice(Int32.Parse(dr[0].ToString()), DateTime.Parse(dr[1].ToString()), decimal.Parse(dr[2].ToString()));
                lstInvoices.Add(newInvoice);
            }



            return lstInvoices;
        }

        public List<String> getLineItems()
        {
            DataSet ln = new DataSet();
            int iRet = 0;
            lineItems = new List<String>();
            ln = db.ExecuteSQLStatement("select * from LineItems", ref iRet);
            foreach (DataRow dr in ln.Tables[0].Rows)
            {
                lineItems.Add(dr[0]+" "+dr[2]);
            }
            return lineItems;
        }

        public List<String> getItems()
        {
            DataSet it = new DataSet();
            int iRet = 0;
            items = new List<String>();
            it = db.ExecuteSQLStatement("select ItemCode, ItemDesc from ItemDesc", ref iRet);
            foreach (DataRow dr in it.Tables[0].Rows)
            {
                items.Add(dr[0] + " " + dr[1]);
            }
            return items;


        }




    }
}
