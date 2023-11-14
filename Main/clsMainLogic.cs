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
        static List<clsItem> items; // some sort of list created from sql database.
        static List<clsSearch> invoices; //list of invoices

        #region items

        bool isValidCost;
        bool isValidName;
        bool isValidDescription;
        clsMainLogic()
        {
            items = new List<clsItem>(); // create new list
        }


        private void saveInvoice()
        {
            // Take in selected item ID from SQL database
            //save into new sql invoice table.
        }

        void deleteItem()
        {
            // delete from invoice only
        }
        #endregion

        #region search
        #endregion

        #region main
        #endregion   
    }
}
