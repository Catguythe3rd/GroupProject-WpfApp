using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//SEARCH TO-DO LIST:
//#2: CREATE A METHOD AND IMPLEMENT METHOD FOR DISPLAYING INVOICE ITEMS AS SEARCH PARAMETERS
namespace GroupProject_WpfApp.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        //instantiating some important data for the window
        #region
        //parent window
        internal Main.wndMain parent;
        //general list if invoices
        internal List<invoice> invoices = new List<invoice>();
        //list of searched invoices
        internal List<invoice> searchedInvoices = new List<invoice>();
        //selected invoice
        internal invoice selectedInvoice = null;
        clsSearchLogic logicClass;
        //lineItems list
        internal List<String> lineItems;
        //items list
        internal List<String> ItemList;
        //list of selected items
        internal List<String> selectedItems = new List<String>();
        #endregion

        /// <summary>
        /// Constructor. Takes parent window as an arguement so that it can access the parent window's internal-variables
        /// </summary>
        /// <param name="parent"></param>
        public wndSearch(Main.wndMain parent)
        {
            //generate UI
            InitializeComponent();
            //set the parent
            this.parent = parent;
            //set value to logic class
            logicClass = new clsSearchLogic(this);
            //call list that sets invoices to the listbox
            setInvoices(invoices,true);
            //set items to the item listbox
            setItems();
        }

        /// <summary>
        /// Adds invoice data to the totalCharges ComboBox, dates to the InvoiceDate ComboBox, and the invoice itself to the scrollViewer.
        /// Takes in an arguement for what list of invoices to actually use.
        /// Set as internal so that it an take an invoiceList as an arguement.
        /// </summary>
        internal void setInvoices(List<invoice> givenInvoices, bool everything)
        {
            //The invoices list will always be changed when this method is called, so this will clear the invoice box so that we can generate it from scratch
            InvoiceListBox.Items.Clear();
            foreach (invoice invoice in givenInvoices)
            {
                //adding to the display list for invoices
                ListBoxItem newInvoice = new ListBoxItem();
                newInvoice.MouseLeftButtonUp += selectCall;
                newInvoice.Content = "#" + invoice.getNumber() + ", " + invoice.getDate() + ", " + invoice.getTotal();
                InvoiceListBox.Items.Add(newInvoice);
                //we don't want to do these two if we are just updating the listBox to reflect a search. Because of this, if the search function called this method,
                //"everything" will be false and thus these two sections will not be used. The only time these are used is when we are generating the initial list,
                //an invoice was added,  or an invoice was deleted
                if (everything)
                {
                    //adding to the ComboBox for dates
                    ComboBoxItem newDate = new ComboBoxItem();
                    newDate.Content = invoice.getDate();
                    dateDropDown.Items.Add(newDate);

                    //adding to the ComboBox for Totals
                    ComboBoxItem newTotal = new ComboBoxItem();
                    newTotal.Content = invoice.getTotal();
                    TotalChargesComboBox.Items.Add(newTotal);
                }
            }
        }

        /// <summary>
        /// When the button is clicked, restore the original search state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //nullify the TotalCharges selection
            TotalChargesComboBox.SelectedItem = null;
            //reset the numberInput textbox
            numberInput.Text = "";
            //nullify the dateDropDown selection
            dateDropDown.SelectedItem = null;
            if (searchedInvoices != null)
            {
                //clear the search results list if it's not null
                searchedInvoices.Clear();
            }
            if (selectedItems != null)
            {
                //clear the items result list it it's not null
                selectedItems.Clear();
            }
            //restore the displayed invoices
            setInvoices(invoices, true);
        }

        /// <summary>
        /// When this window is closed, I should send the Invoice Number of the selected invoice back to the main window
        /// (assuming that there is a selected invoice)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchWindow_Close(object sender, RoutedEventArgs e)
        {
            //insure that an invoice was actually selected
            if (selectedInvoice != null)
            {
                //if so, reach back to the parent variable value and set it equal to the selectedInvoice invoiceNumber
                parent.invoiceID = selectedInvoice.getNumber();
            }

        }

        /// <summary>
        /// Calls a logic class that will search for the selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectCall(object sender, RoutedEventArgs e)
        {
            logicClass.selectInvoice(sender);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            logicClass.searchInvoice();
        }

        private void setItems()
        {
            foreach (String item in ItemList)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.MouseDoubleClick += itemSelected;
                newItem.Content = item;
                ItemListBox.Items.Add(newItem);
            }
        }


        private void itemSelected(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = sender as ListBoxItem;
            foreach (String item in ItemList)
            {
                if (selectedItem.Content == item)
                {
                    selectedItems.Add(item);
                }
            }
        }
    }
}
