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
        invoice selectedInvoice;
        public wndSearch(Main.wndMain parent)
        {
            InitializeComponent();
            clsSearchSQL database = new clsSearchSQL();
            parentWindow = parent;
            invoices = database.getInvoices();
            setInvoices();
        }


        /// <summary>
        /// Adds invoice data to the totalCharges ComboBox, dates to the InvoiceDate ComboBox, and the invoice itself to the scrollViewer
        /// </summary>
        public void setInvoices()
        {
            foreach (invoice invoice in invoices)
            {
                //adding to the display list for invoices
                ListBoxItem newInvoice = new ListBoxItem();
                newInvoice.Content = "#" + invoice.getNumber() + ", " + invoice.getDate() + ", " + invoice.getTotal();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
            }

        }
    }
}
