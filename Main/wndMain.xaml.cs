﻿using GroupProject_WpfApp.Items;
using GroupProject_WpfApp.Search;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace GroupProject_WpfApp.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region windows
        wndItems itemWindow;
        wndSearch searchWindow;
        clsMainSQL mainInventory;
        clsMainLogic mainLogic;  
        clsDataAccess db;
        #endregion
        public wndMain()

        { //start window
            InitializeComponent();
            //show other windows
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
           
            searchWindow = new wndSearch(); 
            itemWindow = new wndItems();
            mainInventory = new clsMainSQL();

            //put info into invoice and inventory list/drop down
            InvoiceList();

            
        }

        /// <summary>
        /// binds information to invoice_list and itemDropDown
        /// </summary>
        private void InvoiceList()
        {
            
            invoice_List.ItemsSource = mainInventory.getAllInvoices();
            ItemDropDown.ItemsSource = mainInventory.getAllItems();//fix doesn't populate properly
        }

        /// <summary>
        /// Open Items page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            //hide main
            this.Hide();
        
            //open items page
            itemWindow.Show();
            this.Show();
        }

        /// <summary>
        /// open search page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

            
            
            this.Hide();
            searchWindow.Show();
            this.Show();
            //catch invoice id
            //invoiceList.selectedIndex = invoiceID

        }


        /// <summary>
        /// new invoice outline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            invoicebox.IsEnabled = true;
            int id = mainInventory.getnewID();
            //show new invoice number
            invoiceNum.Content = id;
            //show current Cost
            CostNum.Content = 0;
            //show Tax Cost
            taxNum.Content = 0;
            //show Total Cost
            TotalCostNum.Content = 0;

        }

        /// <summary>
        /// edit selected invoice. Doesn't work yet 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (invoice_List.SelectedItem == null) return;
            else
            {
                invoicebox.IsEnabled = true;
                //grab invoice info
                int idNum = invoice_List.SelectedIndex;
                idNum += 5000;
                clsMainLogic myInvoice = mainInventory.getOneInvoice(idNum);
                myInvoice.EditInvoice = true;
                
                
            }



        }

        /// <summary>
        /// send selected inventory obj from drop down to items list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //Grab item number getONEItem()
            
            //add item Description and cost it ItemsList
        }

        /// <summary>
        /// save edited to correct invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //save invoice date to invoice obj 


            //save itemsList to invoice obj
            //save Cost to invoice obj
            //save Tax to invoice obj
            //save Total Cost to invoice obj
            //upload invoice date to invoice obj
            //if new: newInvoice()
            //List<clsMainLogic> newInvoice = mainInventory.getOneInvoice();
            //if edit: editInvoice()

            //hide all invoice buttons

            mainInventory.newInvoice(); //doesn't  work yet.

        }

        /// <summary>
        /// delete selected items from ItemsList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int num = Int32.Parse(invoiceNum.Content.ToString());
            if(invoiceNum.Content == null) { return; }
            else  mainInventory.DeleteItemsFromInvoice(num); 
        }

        private void invoice_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsList.Items.Clear();

            int idNum = invoice_List.SelectedIndex;
            idNum+=5000;
            clsMainLogic myInvoice = mainInventory.getOneInvoice(idNum);
            decimal cost = 0;
            decimal tax = 0;
            invoiceNum.Content = myInvoice.ID;
            InvoiceDateBox.Text = myInvoice.InvoiceDate.ToString();
            CostNum.Content = 0;//to be updated once items are fixed.
            ItemsList.Items.Add(mainInventory.getAllItems());//update with getOneitems
            taxNum.Content =  decimal.Multiply(cost, tax); ; //to be updated once items are fixed.
            TotalCostNum.Content = myInvoice.InvoiceTotal.ToString();
            
        }
    }
}
