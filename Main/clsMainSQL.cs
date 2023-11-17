using GroupProject_WpfApp.Items;
using Microsoft.VisualBasic;
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
        List<clsItem> lstItems;

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
                Invoice.InvoiceTotal  = decimal.Parse(dr[2].ToString());


                lstInvoices.Add(Invoice);
                
            }
            return lstInvoices;
        }

        public List<clsItem> getAllItems()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstItems = new List<clsItem>();

            ds = db.ExecuteSQLStatement("select * from Items", ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                 string ItemCode = dr[0].ToString();
                 string ItemDesc = dr[1].ToString();
                 decimal cost= decimal.Parse(dr[2].ToString());

                clsItem item = new clsItem(ItemCode, ItemDesc, cost);



                lstItems.Add(item);

            }
            return lstItems;
        }


        public void newInvoice()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;

            ds = db.ExecuteSQLStatement("INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#4/13/2018#, 100)", ref iRet);

            
        }


        public List<clsMainLogic> getOneInvoice()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstInvoices = new List<clsMainLogic>();

            ds = db.ExecuteSQLStatement("SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123", ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                clsMainLogic Invoice = new clsMainLogic();
                Invoice.ID = Int32.Parse(dr[0].ToString());
                Invoice.InvoiceDate = DateOnly.Parse(dr[1].ToString());
                Invoice.InvoiceTotal = decimal.Parse(dr[2].ToString());


                lstInvoices.Add(Invoice);

            }
            return lstInvoices;
        }


        public List<clsItem> getONEItem()
        { 
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstItems = new List<clsItem>();
            
            ds = db.ExecuteSQLStatement("SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = 5000", ref iRet);
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string ItemCode = dr[0].ToString();
                string ItemDesc = dr[1].ToString();
                decimal cost = decimal.Parse(dr[2].ToString());
            
                clsItem item = new clsItem(ItemCode, ItemDesc, cost);
            
            
            
                lstItems.Add(item);
            
            }
            return lstItems;
            
        } 

        public void DeleteInvoice()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0; 

            ds = db.ExecuteSQLStatement("DELETE FROM LineItems WHERE InvoiceNum = 5000", ref iRet);
        }

        public void editInvoice()
        {
            
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;

            ds = db.ExecuteSQLStatement("UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123", ref iRet);
        }
    }
}
