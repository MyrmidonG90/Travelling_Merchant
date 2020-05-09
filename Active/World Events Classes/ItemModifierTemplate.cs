using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class ItemModifierTemplate
    {
        int id;
        List<int> itemCategories;
        List<float> itemModifiers;

        public ItemModifierTemplate(int i, List<int> c, List<float> m)
        {
            id = i;
            itemCategories = c;
            itemModifiers = m;
        }

        public int ID
        {
            get
            {
                return id;
            }
        }
        public List<int> ItemCategories
        {
            get
            {
                return itemCategories;
            }
        }
        public List<float> ItemModifiers
        {
            get
            {
                return itemModifiers;
            }
        }
    }
}
