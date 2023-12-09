using GroupProject_WpfApp.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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

        wndMain parentWindow;           // Allow manipulating variables of the arleady existing main function.
        clsItemsLogic clsItemsLogic;    // Link to the logic class.
        clsItem? selectedItem;           // The cureently selected item in the list.
        List<clsItem> itemsList;        // list of jewelery items

        bool isValid_Code = false;
        bool isValid_Description = false;
        bool isValid_Cost = false;

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

                clsItemsLogic = new clsItemsLogic();                // Initializes item logic script.
                itemsList = clsItemsLogic.getAllItems();            // Stores a list of the items from the database.
                itemsTable_DataGrid.ItemsSource = itemsList;        // Sets the datagrid to the list of items.
                EditError_Label.Visibility = Visibility.Collapsed;  // Closes error box before starting the program.

                add_Button.IsEnabled = false;
                save_Button.IsEnabled = false;
                delete_Button.IsEnabled = false;
                upArrow_Button.IsEnabled = false;
                downArrow_Button.IsEnabled = false;
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
                // If there's no selected item, but there are items in the datagrid, select the top most one.
                if (selectedItem == null && itemsTable_DataGrid != null)
                {
                    itemsTable_DataGrid.SelectedIndex = 0;
                }
                else // Iterate down one in the datagrid.
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
                // If there's no selected item, but there are items in the datagrid, select the bottom most one.
                if (selectedItem == null && itemsTable_DataGrid != null)
                {
                    itemsTable_DataGrid.SelectedIndex = itemsList.Count - 1;
                }
                else // Iterate down one in the datagrid.
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

        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsItemsLogic.insertItem((string)Edit_Code_TextBox.Text, (string)Edit_Description_TextBox.Text, Decimal.Parse(Edit_Cost_TextBox.Text));
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
                clsItemsLogic.updateItem(selectedItem.ItemDesc, selectedItem.Cost, selectedItem.ItemCode);
                EditError_Label.Visibility = Visibility.Collapsed;
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
                // must check if selected item is in an existing invoice, if so don't allow delete, update error label.
                clsItemsLogic.deleteItem(selectedItem.ItemCode); // Deletes item from database.
                itemsList = clsItemsLogic.getAllItems();         // Reloads item list from data base.
                itemsTable_DataGrid.ItemsSource = itemsList;     // Reloads datagrid with items from list.
                selectedItem = null;                             // Sets slected item to default.

                // Sets the values in the Edit Items group box to initial null values.
                Edit_Code_TextBox.Text = null;
                Edit_Description_TextBox.Text = null;
                Edit_Cost_TextBox.Text = null; // cost is in decimal, so converts to string.

                // Disables save and delete buttons
                save_Button.IsEnabled = false;
                delete_Button.IsEnabled = false;

                // Collapses error label
                EditError_Label.Visibility = Visibility.Collapsed;
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
                if (string.IsNullOrEmpty(Edit_Code_TextBox.Text) == true)
                {
                    isValid_Code = false;
                }
                else
                {
                    isValid_Code = true;
                    userHasEnteredNewItemCode = false;

                    // Iterates through list and checks if the users itemCode matches an already existing code.
                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        if (Edit_Code_TextBox.Text == itemsList[i].ItemCode)
                        {
                            userHasEnteredNewItemCode = true;
                            selectedItem = itemsList[i];
                            itemsTable_DataGrid.SelectedItem = selectedItem;
                            break;
                        }
                    }
                }

                updateEditItemsButtons();
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Edit_Description_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Edit_Description_TextBox.Text) == true)
                {
                    isValid_Description = false;
                }
                else
                {
                    isValid_Description = true;
                }

                updateEditItemsButtons();
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Edit_Cost_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Edit_Cost_TextBox.Text) == true)
                {
                    isValid_Cost = false;
                }
                else
                {
                    isValid_Cost = true;
                }

                updateEditItemsButtons();
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void updateEditItemsButtons()
        {
            try
            {
                // Delete
                if (isValid_Code == true && userHasEnteredNewItemCode == false)
                {
                    delete_Button.IsEnabled = true;
                }
                else
                {
                    delete_Button.IsEnabled = false;
                }

                // Save
                if (isValid_Code == true && isValid_Description == true && isValid_Cost == true 
                    && userHasEnteredNewItemCode == false)
                {
                    save_Button.IsEnabled = true;
                }
                else
                {
                    save_Button.IsEnabled = false;
                }

                // Add
                if(isValid_Code == true && isValid_Description == true && isValid_Cost == true 
                    && userHasEnteredNewItemCode == true)
                {
                    add_Button.IsEnabled = true;
                }
                else
                {
                    add_Button.IsEnabled= false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

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

        // Returns the selected Item to main when the window closes.
        private void itemWindow_Close(object sender, CancelEventArgs e)
        {
            try
            {
                parentWindow.itemID = selectedItem;
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
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
