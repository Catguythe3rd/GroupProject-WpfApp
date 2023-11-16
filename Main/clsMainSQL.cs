using GroupProject_WpfApp.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Main
{
    
    internal class clsMainSQL
    {
        List<clsMainLogic> lstInvoices;

        /// <summary>
        /// list all items in database
        /// </summary>
        /// <returns></returns>
        public List<clsMainLogic> getAllInvoices()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstInvoices = new List<clsMainLogic>();
            
            ds = db.ExecuteSQLStatement("select * from Invoices", ref iRet);

            foreach(DataRow dr in ds.Tables[0].Rows)
            {

                clsMainLogic Invoice = new clsMainLogic();
                Invoice.ID = Int32.Parse(dr[0].ToString());
                Invoice.InvoiceDate = DateOnly.Parse(dr[1].ToString());
                Invoice.InvoiceTotal  = float.Parse(dr[2].ToString());


                lstInvoices.Add(Invoice);
                
            }
            return lstInvoices;
        }



    }
}
