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

            //these three statements are setting the previous objects equal to the arguements, if applicable
            if (window.dateDropDown.SelectedItem != null)
            {
                date = window.dateDropDown.SelectedItem as ComboBoxItem;
            }
            if (window.TotalChargesComboBox.SelectedItem != null)
            {
                charge = window.TotalChargesComboBox.SelectedItem as ComboBoxItem;
            }
            int.TryParse(window.numberInput.Text, out invoiceNumber);


            foreach (invoice invoice in window.invoices)
            {
                //does the invoice have the same date?
                if (invoice.getDate().ToString() == date.Content.ToString())
                {
                    window.searchedInvoices.Add(invoice);
                }
                //does the invoice have the same invoice number?
                else if (invoice.getNumber() == invoiceNumber)
                {
                    window.searchedInvoices.Add(invoice);
                }
                //does the invoice have the same totalCharge?
                else if (invoice.getTotal().ToString() == charge.Content.ToString())
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



        /// <summary>
        /// Called by the window class on a listbox item click to find the selected invoice
        /// </summary>
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
