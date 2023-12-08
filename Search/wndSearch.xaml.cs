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

namespace GroupProject_WpfApp.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        Window parentWindow;
        List<invoice> invoices = new List<invoice>();
        List<invoice> searchedInvoices = new List<invoice>();
        internal invoice selectedInvoice;
        public wndSearch(Main.wndMain parent)
        {
            InitializeComponent();
            clsSearchSQL database = new clsSearchSQL();
            parentWindow = parent;
            invoices = database.getInvoices();
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editInvoice editWindow = new editInvoice(this);
            editWindow.Owner = this;
            editWindow.ShowDialog();


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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
