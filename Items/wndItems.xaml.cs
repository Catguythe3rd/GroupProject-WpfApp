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

        wndMain parentWindow;           // Link to the main window.
        clsItemsLogic clsItemsLogic;    // Link to the logic class.
        List<clsItem> itemsList;        // list of jewelery items.

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
                if (itemsTable_DataGrid.SelectedItem == null && itemsTable_DataGrid != null)
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
                if (itemsTable_DataGrid.SelectedItem == null && itemsTable_DataGrid != null)
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

                itemsList = clsItemsLogic.getAllItems();         // Reloads item list from data base.
                itemsTable_DataGrid.ItemsSource = itemsList;     // Reloads datagrid with items from list.

                // The item has been added to the list and is no longer a new code.
                userHasEnteredNewItemCode = false;

                // New items are put at the bottomb of the list so, set the dataGrid's selected item to the bottom item.
                itemsTable_DataGrid.SelectedIndex = itemsList.Count - 1;
                itemsTable_DataGrid.SelectedItem = itemsTable_DataGrid.Items[itemsTable_DataGrid.SelectedIndex]; 

                // Updates the edit item buttons as this item cannot be added again.
                updateEditItemsButtons();

                //itemsTable_DataGrid.SelectedItem = selectedItem; 
                EditError_Label.Visibility = Visibility.Collapsed;
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
                clsItemsLogic.updateItem((string)Edit_Description_TextBox.Text, Decimal.Parse(Edit_Cost_TextBox.Text), (string)Edit_Code_TextBox.Text);
                itemsList = clsItemsLogic.getAllItems();         // Reloads item list from data base.

                // Saves the selected index and item because these values are set to default when
                // the dataGrid is reloaded.
                int tempIndex = itemsTable_DataGrid.SelectedIndex;
                clsItem tempItem = itemsTable_DataGrid.SelectedItem as clsItem;

                itemsTable_DataGrid.ItemsSource = itemsList;    // Reloads datagrid with updated items from list.

                // Re-adds saved index and item values to dataGrid.
                itemsTable_DataGrid.SelectedIndex = tempIndex;
                itemsTable_DataGrid.SelectedItem = tempItem;        

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

                clsItem tempItem = itemsTable_DataGrid.SelectedItem as clsItem;
                clsItemsLogic.deleteItem(tempItem.ItemCode);    // Deletes item from database.

                itemsList = clsItemsLogic.getAllItems();         // Reloads item list from data base.
                itemsTable_DataGrid.ItemsSource = itemsList;     // Reloads datagrid with items from list.

                // Sets the values in the Edit Items group box to initial null values.
                // Changing the text also calls functions to check whether they are valid or not.
                Edit_Code_TextBox.Text = null;
                Edit_Description_TextBox.Text = null;
                Edit_Cost_TextBox.Text = null; // cost is in decimal, so converts to string.

                // An item is no longer selected, so this resets the dataGrids selected values.
                itemsTable_DataGrid.SelectedItem = null;         
                itemsTable_DataGrid.SelectedIndex = -1;

                // Updates edit item buttons.
                // No buttons can be clicked since as there is no item selected and nothing in the textBoxes.
                updateEditItemsButtons();   

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
                // If there is no selected item, then there are no values to update the groupbox with.
                if (itemsTable_DataGrid.SelectedIndex != -1)
                {
                    editItems_GroupBox_Update();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Sets the values in the Edit Items group box to match the selected item.
        /// </summary>
        /// <exception cref="Exception"></exception>
        void editItems_GroupBox_Update()
        {
            try
            {
                clsItem tempItem = itemsTable_DataGrid.SelectedItem as clsItem;

                Edit_Code_TextBox.Text = tempItem.ItemCode;
                Edit_Description_TextBox.Text = tempItem.ItemDesc;
                Edit_Cost_TextBox.Text = tempItem.Cost.ToString(); // cost is in decimal, so converts to string.
                EditError_Label.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
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
                    userHasEnteredNewItemCode = true;

                    // Iterates through list and checks if the users itemCode matches an already existing code.
                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        if (Edit_Code_TextBox.Text == itemsList[i].ItemCode)
                        {
                            userHasEnteredNewItemCode = false;
                            itemsTable_DataGrid.SelectedItem = itemsList[i];
                            editItems_GroupBox_Update();
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
                TextBox tempTextBox = sender as TextBox;

                // Limits the length of text put into the database feild based on which feild/textbox it came from.
                int maxTextLength = 0;

                if (tempTextBox.Name == "Edit_Code_TextBox")
                {
                    maxTextLength = 4;
                }
                else
                {
                    maxTextLength = 25;
                }

                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z && tempTextBox.Text.Length < maxTextLength))
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
                TextBox tempTextBox = sender as TextBox;

                //Only allow letters to be entered
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9 && tempTextBox.Text.Length < 4))
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
                clsItem tempItem = itemsTable_DataGrid.SelectedItem as clsItem;
                parentWindow.itemID = tempItem;
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
