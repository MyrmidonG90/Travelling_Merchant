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
        Inventory templateInv;
        int lastTraded;
        bool traded;
        List<string> neighbors;

        float x, y;

        public City(string name, string information, Vector2 coordinates, List<string> neighbors)
        {
            this.name = name;
            this.information = information;
            this.coordinates = coordinates;
            x = coordinates.X;
            y = coordinates.Y;
            inv = new Inventory(100);
            templateInv = new Inventory(100);
            this.neighbors = neighbors;
        }

        public void Update()
        {
            if (traded)
            {
                if (lastTraded + 7 <= Calendar.TotalDays)
                {
                    InvReset();
                    traded = false;
                }
            }
        }

        public void InvReset()
        {
            inv = new Inventory(templateInv);
            int i = 0;
        }

        public void InvInit()
        {
            templateInv = new Inventory(inv);
        }

        public void InvNewSet(Inventory newInv)
        {
            templateInv = newInv;
            CheckDate();
            traded = true;
        }

        public void CheckDate()
        {
            lastTraded = Calendar.TotalDays;
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

        public bool Traded
        {
            get
            {
                return traded;
            }
            set
            {
                traded = value;
            }
        }

        public List<string> Neighbors
        {
            get => neighbors;
        }

        public Inventory TemplateInv
        {
            get => templateInv;
            set => templateInv = value;
        }

        public Inventory Inv { get => inv; set => inv = value; }

        public override string ToString()
        {
            string total, items;
            items = "";

            if (inv != null)
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
