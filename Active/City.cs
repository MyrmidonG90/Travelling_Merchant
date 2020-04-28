using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Active
{
    class City
    {
        readonly string name;
        string information;
        Vector2 coordinates;
        Inventory inv;

        float x, y;

        public City(string name, string information, Vector2 coordinates)
        {
            this.name = name;
            this.information = information;
            this.coordinates = coordinates;
            x = coordinates.X;
            y = coordinates.Y;
            
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void AddInventory(Inventory inv)
        {
            this.inv = inv;
        }

        public string Information
        {
            get
            {
                return information;
            }
            set
            {
                information = value;
            }
        }

        public Vector2 Coordinates
        {
            get
            {
                return coordinates;
            }
        }

        public Inventory Inv { get => inv; set => inv = value; }

        public override string ToString()
        {
            string total, items;
            items = "";

            if (inv.ItemList != null)
            {
                if (Inv.ItemList.Count != 0)
            {
                for (int i = 0; i < Inv.ItemList.Count; i++)
                {
                    items = Inv.ItemList[i].ID.ToString() + ':' + Inv.ItemList[i].Amount.ToString() + ',';
                }
                total = name + ';' + information + ';' + x + ';' + y + ';' + Inv.Money + ';' + items + '|';
            }
            else
            {
                total = name + ';' + information + ';' + x + ';' + y + ';' + Inv.Money + ';' + '|';
            }
            }
            else
            {
                total = name + ';' + information + ';' + x + ';' + y + ';' + 0 + ';' + '|';
            }
            
            return total;
        }
    }
}
