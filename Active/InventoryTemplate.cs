using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class InventoryTemplate
    {
        int id;
        List<bool> amountNegOrPos;
        List<Item> items;

        public InventoryTemplate(int id, List<bool> a, List<Item> i)
        {
            this.id = id;
            amountNegOrPos = a;
            items = i;
        }

        public int ID
        {
            get => id;
        }
    }
}
