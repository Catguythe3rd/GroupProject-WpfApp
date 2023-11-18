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
            //put info into invoice and invenotry list/drop down
            InvoiceList();

            
        }

        /// <summary>
        /// binds information to invoice_list and ditmeDropDown
        /// </summary>
        private void InvoiceList()
        {
            // List<clsMainLogic> invoices = mainInventory.getAllInvoices();

            invoice_List.ItemsSource = mainInventory.getAllInvoices();
            ItemDropDown.ItemsSource = mainInventory.getAllItems();
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


            //show new invoice number
            //show current Cost
            //show Tax Cost
            //show Total Cost

            //call newInvoice()
            InvoiceList();

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

            //getONEInvoice()
            //populate information to invoice box

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
