using GroupProject_WpfApp.Main;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GroupProject_WpfApp.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region TestRegion_Variables
        clsItemsLogic clsItemsLogic;
        List<clsItem> itemsList;
        #endregion

        public wndItems()
        {
            InitializeComponent();
            clsItemsLogic = new clsItemsLogic(); 
            itemsList = clsItemsLogic.getAllItems();
            itemsTable_DataGrid.ItemsSource = itemsList;
        }

        private void new_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upArrow_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void downArrow_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemsTable_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*DataGrid dataGrid = sender as DataGrid;
            Edit_Code_TextBox.Text = (clsItemsSQL)dataGrid.Items[0].item;
            Edit_Description_TextBox.Text = tempItem.ItemDesc;
            Edit_Cost_TextBox.Text = tempItem.Cost.ToString();*/

            // doesn't work yet. Might make the user go through search just to use the edit box.
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            clsItemsLogic.HasBeenSaved_editedItem = true;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Code_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clsItemsLogic.HasBeenSaved_editedItem = false;
        }

        private void Edit_Description_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clsItemsLogic.HasBeenSaved_editedItem = false;
        }

        private void Edit_Cost_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clsItemsLogic.HasBeenSaved_editedItem = false;
        }

        private void Search_Code_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
