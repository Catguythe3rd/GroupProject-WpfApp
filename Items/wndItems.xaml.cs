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
        clsItemsSQL clsItemsSQL;
        List<clsItem> itemsList;

        public wndItems()
        {
            InitializeComponent();
            clsItemsSQL = new clsItemsSQL();
            itemsList = clsItemsSQL.getItems();
            itemsTable_DataGrid.ItemsSource = itemsList;
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

        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
