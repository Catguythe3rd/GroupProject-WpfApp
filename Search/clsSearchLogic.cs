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
using System.Collections.Generic;
using System.Linq;

namespace GroupProject_WpfApp.Search
{
    internal class clsSearchLogic{
        wndSearch window;
        public clsSearchLogic(wndSearch window)
        {
            //initiate the parent window
            this.window = window;
            clsSearchSQL database = new clsSearchSQL();
            //set the invoice list equal to the retrieved DB list
            window.invoices = database.getInvoices();
            window.lineItems = database.getLineItems();
            window.ItemList = database.getItems();
        }

        /// <summary>
        /// Taking in the arguements offered from the search parameters, the program will search for any invoices that have the same attributes.
        /// Does not need all of the arguements to be offered to run this program, as it will simply not find any invoice with an attribute equal to
        /// nothing.
        /// </summary>
        public void searchInvoice()
        {
            //We need to clear the searchedInvoices list so that we are not retaining invoices from old searches
            window.searchedInvoices.Clear();
            //These are the search arguements
            ComboBoxItem date = new ComboBoxItem();
            date.Content = "00/00/00";
            ComboBoxItem charge = new ComboBoxItem();
            charge.Content = "00.00";
            int invoiceNumber = 0;

            List<String> parameters = new List<String>();
            List<String> comparators = new List<String>();
            int numbSearch = 0;
            int matches = 0;

            //these three statements are setting the previous objects equal to the arguements, if applicable
            if (window.dateDropDown.SelectedItem != null)
            {
                date = window.dateDropDown.SelectedItem as ComboBoxItem;
                parameters.Add(date.Content.ToString());
                numbSearch++;
            }
            else
            {
                parameters.Add("No Input");
            }
            if (window.TotalChargesComboBox.SelectedItem != null)
            {
                charge = window.TotalChargesComboBox.SelectedItem as ComboBoxItem;
                parameters.Add(charge.Content.ToString());
                numbSearch++;
            }
            else
            {
                parameters.Add("No Input");
            }
            if (int.TryParse(window.numberInput.Text, out invoiceNumber))
            { 
                parameters.Add(invoiceNumber.ToString());
                numbSearch++;
            }
            else
            {
                parameters.Add("No Input");
            }
            
            foreach (invoice invoice in window.invoices)
            {
                matches = 0;
                comparators.Clear();
                comparators.Add(invoice.getNumber().ToString());
                comparators.Add(invoice.getTotal().ToString());
                comparators.Add(invoice.getDate().ToString());
                if(comparators[0] == parameters[0])
                {
                    matches++;
                }
                if (comparators[1] == parameters[1])
                {
                    matches++;
                }
                if (comparators[2] == parameters[2])
                {
                    matches++;
                }

                if (matches == numbSearch)
                {
                    window.searchedInvoices.Add(invoice);
                }

                foreach (String item in window.ItemList)
                {
                    foreach(String lineItem in window.lineItems)
                    {
                        char itemCode = item[1];
                        if (itemCode + " " + invoice.getNumber() == lineItem)
                        {
                            window.searchedInvoices.Add(invoice);
                        }
                    }
                }
            }
            window.setInvoices(window.searchedInvoices, false);
        }



        
        internal void selectInvoice(object sender)
        {
            //instantiate the sender as a listboxitem
            ListBoxItem selection = sender as ListBoxItem;
            //iterate through the invoices
            foreach (invoice invoice in window.invoices)
            {
                //compare the string of the invoice to the string of the sender
                if ("#" + invoice.getNumber() + ", " + invoice.getDate() + ", " + invoice.getTotal() == selection.Content)
                {
                    //if true, set the selectedInvoice equal to that invoice
                    window.selectedInvoice = invoice;
                }
            }
        } 
    }
}
