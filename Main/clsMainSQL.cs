using GroupProject_WpfApp.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

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
            ds = db.ExecuteSQLStatement("Select * FROM invoices", ref iRet);

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                clsMainLogic testing = new clsMainLogic();
                testing.ID = Int32.Parse(dr[0].ToString());
                testing.InvoiceDate = DateTime.Parse(dr[1].ToString());
                testing.InvoiceTotal = decimal.Parse(dr[2].ToString());
                lstInvoices.Add(testing);
            }

            

            return lstInvoices;
        }
        #region code

        /// <summary>
        /// get a list of all items.
        /// </summary>
        /// <returns></returns>
        public List<clsItem> getAllItems()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstItems = new List<clsItem>();

            ds = db.ExecuteSQLStatement("SELECT * FROM ItemDesc", ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                clsItem item = new clsItem(
                    dr[0].ToString(),
                    dr[1].ToString(),
                    (decimal)dr[2]
                ); 
                lstItems.Add(item);
            }


            return lstItems;


        }
        
        
        public void newInvoice()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
        
            db.ExecuteNonQuery("INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#4/13/2018#, 100)");
        
            
        }
        
        /// <summary>
        /// get one invoice with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public clsMainLogic getOneInvoice(int id)
         {
             clsDataAccess db = new clsDataAccess();
             DataSet ds = new DataSet();
             int iRet = 0;
             lstInvoices = new List<clsMainLogic>();
         
             ds = db.ExecuteSQLStatement("SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = "+ id, ref iRet);
         
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
         
                 clsMainLogic Invoice = new clsMainLogic();
                 Invoice.ID = int.Parse(dr[0].ToString());
                 Invoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                 Invoice.InvoiceTotal = decimal.Parse(dr[2].ToString());
         
         
                 lstInvoices.Add(Invoice);
         
             }
             return lstInvoices[0];
         }
        

        /// <summary>
        /// get one  item
        /// </summary>
        /// <returns></returns>
        public clsItem getONEItem()
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
            
                clsItem item = new clsItem(dr[0].ToString(), dr[1].ToString(), decimal.Parse(dr[2].ToString()));
            
            
            
                lstItems.Add(item);
            
            }
            return lstItems[0];
            
        } 
        //

        /// <summary>
        /// delete all items from selected invoice.
        /// </summary>
        /// <param name="invoiceID"></param>
        public void DeleteItemsFromInvoice(int invoiceID)
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
        
            db.ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum = " + invoiceID);
        }
        /// <summary>
        /// edit invoice total cost
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <param name="TotalCost"></param>
        public void editInvoice(int invoiceID, decimal TotalCost)
        {

            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();

            db.ExecuteNonQuery("UPDATE Invoices SET TotalCost = " + TotalCost +" WHERE InvoiceNum = "+invoiceID);
        }

        /// <summary>
        /// create a new id based on last largest number in id
        /// </summary>
        /// <returns></returns>
        public int  getnewID()
        {
            List<clsMainLogic> list = getAllInvoices();
            int id = 0;
            for (int i = 0; i < list.Count; i++)
            {
                id = list[i].ID;
            }
            id++;


            return id;
        }


        #endregion
    }
}
