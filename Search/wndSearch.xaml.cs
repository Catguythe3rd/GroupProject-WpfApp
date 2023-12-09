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
//#1: CREATE METHOD AND IMPLEMENT METHOD FOR SELECTING A DATE AS A SEARCH PAREMETER
//#2: CREATE A METHOD AND IMPLEMENT METHOD FOR DISPLAYING INVOICE ITEMS AS SEARCH PARAMETERS
//#3: MOVE SOME OF THE BUSINESS LOGIC BACK TO THE LOGIC CLASS
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
        Main.wndMain parent;
        //general list if invoices
        List<invoice> invoices = new List<invoice>();
        //list of searched invoices
        List<invoice> searchedInvoices = new List<invoice>();
        //selecyed invoice
        internal invoice selectedInvoice = null;
        #endregion

        /// <summary>
        /// Constructor. Takes parent window as an arguement so that it can access the parent window's internal-variables
        /// </summary>
        /// <param name="parent"></param>
        public wndSearch(Main.wndMain parent)
        {
            //generate UI
            InitializeComponent();
            //instantiate the DB
            clsSearchSQL database = new clsSearchSQL();
            //set the parent
            this.parent = parent;
            //set the invoice list equal to the retrieved DB list
            invoices = database.getInvoices();
            //call list that sets invoices to the listbox
            setInvoices(invoices,true);
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
                newInvoice.MouseLeftButtonUp += selectInvoice;
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
        /// calls the edit invoice sub-window. Passes this window as a parent as an arguement so that it has direct access to the selcted invoice, 
        /// rather than passing the invoice as an arguement, instantiating a copy, and then returning it and setting the original equal to that
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //instantiate window, passing this one as an arguement
            editInvoice editWindow = new editInvoice(this);
            //set this window as the owner of it
            editWindow.Owner = this;
            //display new window under showDialog so that this one cannot be alter'd anymore
            editWindow.ShowDialog();
            //now that we have made potentially substantial changes to an invoice, we will regenerate the invoice list in it's entirety
            setInvoices(invoices, true);
        }

        /// <summary>
        /// Taking in the arguements offered from the search parameters, the program will search for any invoices that have the same attributes.
        /// Does not need all of the arguements to be offered to run this program, as it will simply not find any invoice with an attribute equal to
        /// nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //We need to clear the searchedInvoices list so that we are not retainign invoices from old searches
            searchedInvoices.Clear();
            //These are the search arguements
            ComboBoxItem date = new ComboBoxItem();
            ComboBoxItem charge = new ComboBoxItem();
            int invoiceNumber = 0;

            //these three statements are setting the previous objects equal to the arguements, if applicable
            if(dateDropDown.SelectedItem != null)
            {
                date = dateDropDown.SelectedItem as ComboBoxItem;
            }
            if (TotalChargesComboBox.SelectedItem != null)
            {
                charge = TotalChargesComboBox.SelectedItem as ComboBoxItem;
            }
            int.TryParse(numberInput.Text, out invoiceNumber);


            foreach (invoice invoice in invoices)
            {
                //does the invoice have the same date?
                if (invoice.getDate().ToString() == date.Content.ToString())
                {
                    searchedInvoices.Add(invoice);
                }
                //does the invoice have the same invoice number?
                else if (invoice.getNumber() == invoiceNumber)
                {
                    searchedInvoices.Add(invoice);
                }
                //does the invoice have the same totalCharge?
                else if (invoice.getTotal().ToString() == charge.Content.ToString())
                {
                    searchedInvoices.Add(invoice);
                }
                setInvoices(searchedInvoices, false);
            }

        }

        /// <summary>
        /// This is a click function that is assigned to the ListBoxItems on their click events as they are generated 
        /// It will compare the same format of the listboxitem string using each invoice to the actual sender of the arguement (AKA, the listboxitem)
        /// When it is found, that invoice is set to the selectedInvoice, a global variable for this class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectInvoice(object sender, RoutedEventArgs e)
        {
            //instantiate the sender as a listboxitem
            ListBoxItem selection = sender as ListBoxItem;
            //iterate through the invoices
            foreach(invoice invoice in invoices)
            {
                //compare the string of the invoice to the string of the sender
                if ("#" + invoice.getNumber() + ", " + invoice.getDate() + ", " + invoice.getTotal() == selection.Content)
                {
                    //if true, set the selectedInvoice equal to that invoice
                    selectedInvoice = invoice;
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
            //clear the search results List
            searchedInvoices.Clear();
            //restore the displayed invoices
            setInvoices(invoices, true);
            //clear the selected invoice
            selectedInvoice = null;
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
    }
}
