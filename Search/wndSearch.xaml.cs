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
        List<invoice> invoices = new List<invoice>();
        public wndSearch()
        {
            InitializeComponent();
            clsSearchSQL database = new clsSearchSQL();
            invoices = database.getInvoices();
            setInvoices();
        }


        /// <summary>
        /// Adds invoices from the imported list into the scrollviewer!
        /// </summary>
        public void setInvoices()
        {
            for (int i = 0; i < invoices.Count; i++)
            {
                ListBoxItem newInvoice = new ListBoxItem();
                newInvoice.Content = invoices[i];
            }
        }


        
    }
}
