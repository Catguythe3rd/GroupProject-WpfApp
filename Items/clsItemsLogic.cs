using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItemsLogic
    {
        clsItemsSQL clsItemsSQL;

        public clsItemsLogic()
        {
            clsItemsSQL = new clsItemsSQL(); 
        }

        public List<clsItem> getAllItems()
        {
            try
            {
                int iRef = 0;
                DataSet itemsTableDataSet = clsItemsSQL.selectAllItems(ref iRef);

                List<clsItem> listItems = new List<clsItem>();

                for (int i = 0; i < iRef; i++)
                {
                    clsItem tempItem = new clsItem((string)itemsTableDataSet.Tables[0].Rows[i][0],
                                                    (string)itemsTableDataSet.Tables[0].Rows[i][1],
                                                    (decimal)itemsTableDataSet.Tables[0].Rows[i][2]
                                                    );
                    listItems.Add(tempItem);
                }

                return listItems;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        public List<clsItem> getItemsWithItemcode(string itemCode)
        {
            try
            {
                int iRef = 0;
                DataSet itemsTableDataSet = clsItemsSQL.selectItemsWithItemcode(itemCode, ref iRef);

                List<clsItem> listItems = new List<clsItem>();
                for (int i = 0; i < iRef; i++)
                {
                    clsItem tempItem = new clsItem((string)itemsTableDataSet.Tables[0].Rows[i][0],
                                                    (string)itemsTableDataSet.Tables[0].Rows[i][1],
                                                    (decimal)itemsTableDataSet.Tables[0].Rows[i][2]
                                                    );
                    listItems.Add(tempItem);
                }

                return listItems;
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
                clsItemsSQL.updateItem(itemDescription, cost, itemCode);
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
                clsItemsSQL.insertItem(itemCode, itemDescription, cost);
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
                clsItemsSQL.deleteItem(itemCode);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void newItem()
        {
            // create new item object with passed in variables, 
            // set selected item to the new item in list.
        }

        private void saveItem()
        {
            // Take in selected item or its values like name, cost, and description,
            // then call sql function to update selected item with values.
        }

        private void upArrow()
        {
            // iterate up through list, store the selected item in some sort of variable.
        }

        private void downArrow()
        {
            // iterate down through list, store the selected item in some sort of variable.
        }

        void deleteItem()
        {
            // From commonly missed items - Items
            //  - When deleting an item on the def table form,
            //  a requirement says “Instead warn them with a message that tells the user which invoices that item is used on.”.
            //  Your message doesn’t tell which invoices it is on.
        }
    }
}
