using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItemsSQL
    {
        private clsDataAccess dataBase;           // Holds the database that was retrived by clsDataAccess class.

        public clsItemsSQL()
        {
            dataBase = new clsDataAccess();
        }

        public DataSet selectAllItems(ref int iRef)
        {
            try
            {
                iRef = 0; // This stores how many rows have been returned from the ExecuteSQLStatement() function,
                          // which is weird because its a parameter for the function,
                          // but also gets updated by the funtion upon returning? Thats pass by reference for you.
                DataSet itemsTableDataSet = dataBase.ExecuteSQLStatement("SELECT ItemCode, ItemDesc, Cost FROM ItemDesc", ref iRef);

                return itemsTableDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public DataSet selectItemsWithItemcode(string itemCode, ref int iRef)
        {
            try
            {
                iRef = 0; // This stores how many rows have been returned from the ExecuteSQLStatement() function,
                          // which is weird because its a parameter for the function,
                          // but also gets updated by the funtion upon returning? Thats pass by reference for you.
                DataSet itemsTableDataSet = dataBase.ExecuteSQLStatement("select distinct(InvoiceNum) from LineItems where ItemCode = '" + itemCode + "'", ref iRef);

                return itemsTableDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public void updateItem(string itemDescription, decimal cost, string itemCode)
        {
            try
            {
                string SQLString = "Update ItemDesc Set ItemDesc = '" + itemDescription +
                                    "', Cost = " + cost + 
                                    "where ItemCode = '" + itemCode + "'";
                dataBase.ExecuteNonQuery(SQLString);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public void insertItem(string itemCode, string itemDescription, decimal cost)
        {
            try
            {
                string SQLString = "Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + itemCode +
                                    "', '" + itemDescription + "', " + cost + ")";
                dataBase.ExecuteNonQuery(SQLString);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public void deleteItem(string itemCode)
        {
            try
            {
                string SQLString = "Delete from ItemDesc Where ItemCode = '" + itemCode + "'";
                dataBase.ExecuteNonQuery(SQLString);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
    }
}
