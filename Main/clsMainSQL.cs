using GroupProject_WpfApp.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Main
{
    
    internal class clsMainSQL
    {
        List<clsItem> lstItems;

        /// <summary>
        /// list all items in database
        /// </summary>
        /// <returns></returns>
        public List<clsItem> getALLItems()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();
            int iRet = 0;
            lstItems = new List<clsItem>();
            
            ds = db.ExecuteSQLStatement("select ItemCode, ItemDesc, Cost from ItemDesc", ref iRet);

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                string description = "";
                int idCode = Int32.Parse(dr[0].ToString());
                string name = dr[1].ToString();
                float cost  = float.Parse(dr[2].ToString());

                //something wrong here.
                clsItem item = new clsItem(name,description, cost, idCode);

                lstItems.Add(item);
                
            }
            return lstItems;
        }

    }
}
