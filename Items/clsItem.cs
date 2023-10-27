using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_WpfApp.Items
{
    public class clsItem
    {
        string name;
        string description;
        float cost;
        int IDcode;

        clsItem(string name, string description, float cost, int iDcode)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.IDcode = iDcode;
        }

        private void SQL_get()
        {
            // get values from sql data base and save them into this object.
        }

        private void SQL_set()
        {
            // set values from sql data base as the values in this object.
        }
    }
}
