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
        public wndItems()
        {
            InitializeComponent();

            // This code is for database texting purposes.
            clsDataAccess dataBase = new clsDataAccess();
            int iRef = 0;
            DataSet dataSet = dataBase.ExecuteSQLStatement("SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT", ref iRef);
        }

        private void updateLabels()
        {

        }

        private void updateDataGrid()
        {

        }
    }
}
