using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public List<clsItem> getItems()
        {
            int iRef = 0;
            DataSet itemsTableDataSet = dataBase.ExecuteSQLStatement("SELECT ItemCode, ItemDesc, Cost FROM ItemDesc", ref iRef);

            List<clsItem> listItems = new List<clsItem>();
            for (int i = 0; i < iRef; i++)
            {
                clsItem tempItem = new clsItem(
                                                (string)itemsTableDataSet.Tables[0].Rows[i][0],
                                                (string)itemsTableDataSet.Tables[0].Rows[i][1],
                                                (decimal)itemsTableDataSet.Tables[0].Rows[i][2]
                                                );
                listItems.Add(tempItem);
            }

            return listItems;
        }

        private void setItems()
        {
            // set values from sql data base as the values in this object.
        }
    }
}
