using GroupProject_WpfApp.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace GroupProject_WpfApp.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region TestRegion_Variables
        clsItemsLogic clsItemsLogic;
        clsItem selectedItem;
/*
        public bool isValid_Edit_Cost = false;
        public bool isValid_Edit_Name = false;
        public bool isValid_Edit_Description = false;*/

        public bool isValid_editItemInputs = false;


        public bool isValid_Search_Cost = false;
        public bool isValid_Search_Name = false;
        public bool isValid_Search_Description = false;
        #endregion

        /// <summary>
        /// Contrustor
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();

                clsItemsLogic = new clsItemsLogic();    // Initializes item logic script.
                List<clsItem> itemsList = clsItemsLogic.getAllItems();  // Stores a list of the items from the database.
                itemsTable_DataGrid.ItemsSource = itemsList;    // Sets the datagrid to the list of items.
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void new_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Moves the selected item up in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upArrow_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null) // Checks if an item has been selected from the list.
                {
                    // Won't select a higher item if it's at the top of the list.
                    if (itemsTable_DataGrid.SelectedIndex != 0) 
                    {
                        itemsTable_DataGrid.SelectedIndex -= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Moves the selected item down in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downArrow_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null) // Checks if an item has been selected from the list.
                {
                    // Won't select a lower item if it's at the bottom of the list.
                    if (itemsTable_DataGrid.SelectedIndex != itemsTable_DataGrid.Items.Count - 1) 
                    {
                        itemsTable_DataGrid.SelectedIndex += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Updates the window when a new item is clicked or selected from the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsTable_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Set the select item variable for use in other functions.
                selectedItem = itemsTable_DataGrid.Items[itemsTable_DataGrid.SelectedIndex] as clsItem;

                // Sets the values in the Edit Items group box to match the selected item.
                Edit_Code_TextBox.Text = selectedItem.ItemCode;
                Edit_Description_TextBox.Text = selectedItem.ItemDesc;
                Edit_Cost_TextBox.Text = selectedItem.Cost.ToString(); // cost is in decimal, so converts to string.
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                if (HasBeenSaved_editedItem == true)
                {
                    clsItemsLogic.updateItem(selectedItem.ItemDesc, selectedItem.Cost, selectedItem.ItemCode);
                    HasBeenSaved_editedItem = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }*/
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Edit_Code_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*try
            {
                HasBeenSaved_editedItem = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }*/
        }

        private void Edit_Description_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* try
            {
                HasBeenSaved_editedItem = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }*/
        }

        private void Edit_Cost_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*try
            {
                HasBeenSaved_editedItem = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }*/
        }

        private void Search_Code_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Search_Description_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Search_Cost_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void UpdateErrorLabel()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
