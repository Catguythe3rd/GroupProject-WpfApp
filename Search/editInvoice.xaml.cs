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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class editInvoice : Window
    {
        wndSearch parentWindow;

        public editInvoice(wndSearch parentWindow)
        {
            this.parentWindow = parentWindow;
            InitializeComponent();
            newInvoiceCharge.Text = parentWindow.selectedInvoice.getTotal().ToString();
            newInvoiceDate.Text = parentWindow.selectedInvoice.getDate().ToString();
            newInvoiceNumber.Text = parentWindow.selectedInvoice.getNumber().ToString();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(newInvoiceNumber.Text, out _) || !Decimal.TryParse(newInvoiceCharge.Text, out _) || !DateTime.TryParse(newInvoiceDate.Text, out _))
            {

            }
            else
            {
                parentWindow.selectedInvoice.setNumber(Int32.Parse(newInvoiceNumber.Text));
                parentWindow.selectedInvoice.setTotal(newInvoiceCharge.Text);
                parentWindow.selectedInvoice.setDate(DateTime.Parse(newInvoiceDate.Text));
                Close();
            }

            }
        }
    }
}
