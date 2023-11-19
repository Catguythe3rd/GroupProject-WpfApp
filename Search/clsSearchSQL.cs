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

        /// <summary>
        /// list all items in database
        /// </summary>
        /// <returns></returns>
        public List<invoice> getInvoices()
        {
            clsDataAccess db = new clsDataAccess();
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



    }
}
