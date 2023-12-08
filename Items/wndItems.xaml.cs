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

        wndMain parentWindow;
        clsItemsLogic clsItemsLogic;    // 
        clsItem selectedItem;           // The cureently selected item in the list.
        List<clsItem> itemsList;        // list of jewelery items

        bool userHasChangedItemCodeTextBox = false;
        bool userHasEnteredNewItemCode = false;
        #endregion

        /// <summary>
        /// Contrustor
        /// </summary>
        internal wndItems(wndMain parent)
        {
            try
            {
                InitializeComponent();
                this.parentWindow = parent;

                clsItemsLogic = new clsItemsLogic();            // Initializes item logic script.
                itemsList = clsItemsLogic.getAllItems();        // Stores a list of the items from the database.
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

                // Since new info has loaded into the cost textbox, the saved values haven't been changed by the user.
                userHasChangedItemCodeTextBox = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // The item code will be different when a user is trying to create a new item, 
                // so they can't click save. Only click "create new".
                if (userHasEnteredNewItemCode == false)
                {
                    clsItemsLogic.updateItem(selectedItem.ItemDesc, selectedItem.Cost, selectedItem.ItemCode);
                }
                else
                {
                    UpdateErrorLabel();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        /*

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
        }*/

        private void txtLetterInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter 
                        || e.Key == Key.Space))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void txtNumberInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When the testbox is changed, it checks whether the item code has been changed by the user.
        /// If its a different code than one in the list, then the user can create a new item.
        /// If its the same code as one in the list, then the user can update the corresponding item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Code_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (userHasChangedItemCodeTextBox == false)
                {
                    userHasEnteredNewItemCode = true;   // Is true by default,
                                                        // if a duplicate is found in the list, its set to false.

                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        if (Edit_Code_TextBox.Text == itemsList[i].ItemCode)
                        {
                            userHasEnteredNewItemCode = false;
                            break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void UpdateErrorLabel()
        {
            try
            {
                EditError_Label.Content = "ID code can only be changed when creating a new Item.";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void itemWindow_Close()
        {
            parentWindow.itemID = selectedItem;
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
