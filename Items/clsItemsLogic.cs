using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItemsLogic
    {
        // When Main window initializes, Items window will be initialized with items list,
        // and Search window will be initialized with invoice list. 

        // A button in main will call a new itmes window.
        // This will call constructor and update the items list with sql data.
        // When user closes the items window with either x or esscape, 
        // main window will call a function or reset a variable that represents the list of
        // items from itme window.

        // variables holding the user input from the front end.

        static List<clsItem> items; // some sort of list created from sql database.

        bool isValidCost;
        bool isValidName;
        bool isValidDescription;

        clsItemsLogic()
        {
            items = new List<clsItem>(); // reset the list items upon contruction.
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
