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

namespace GroupProject_WpfApp.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public wndMain()
        {
            InitializeComponent();
            //InvoiceList();
            Example();
        }

        public int Example()
        {

            for (int i = 0; i < 100; i++)
            {

                invoice_List.Items.Add("Item " + i.ToString());
                ItemDropDown.Items.Add("Item " + i.ToString());
            }


            return 0;
        }

        private void InvoiceList()
        {
            //invoice_List.Items.Add(item id, item description, item cost)
            //ItemDropDown.Items.Add(item name, item cost)
        }

        /// <summary>
        /// Open Items page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            //hide main
            //open items page
        }

        /// <summary>
        /// open search page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            //hide main
            //open search page

        }


        /// <summary>
        /// new invoice outline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, RoutedEventArgs e)
        {

            //create new invoice obj

            //show new invoice number
            //show current Cost
            //show Tax Cost
            //show Total Cost

        }

        /// <summary>
        /// edit selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //show save button
            //show select button
            //show delete button
            //show items list label
            //show items list drop down



        }

        /// <summary>
        /// send selected inventory obj from drop down to items list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //Grab item number
            //add item Description and cost it ItemsList
        }

        /// <summary>
        /// save edited to correct invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //upload invoice date to invoice obj
            //upload itemsList to invoice obj
            //upload Cost to invoice obj
            //upload Tax to invoice obj
            //upload Total Cost to invoice obj
            //hide all invoice buttons

        }

        /// <summary>
        /// delete selected items from ItemsList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //delete selected Items from Items List
        }
    }
}
