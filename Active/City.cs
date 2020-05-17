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
        List<Vector2> modifiers;
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
            this.inv = new Inventory(inv);
        }
        public void AddModifiers(List<Vector2> modifiers)
        {
            this.modifiers = new List<Vector2>(modifiers);
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

        public string[] ToStringArray()
        {
            //string items;
            string[] total = new string[7];
            //items = "";

            total[0] = name;
            total[1] = information;
            total[2] = coordinates.X +"," + coordinates.Y;
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (i == 0)
                {
                    total[3] = neighbors[i];
                }
                else
                {
                    total[3] += ";" + neighbors[i];
                }
            }
            total[4] = inv.Money.ToString();
            for (int i = 0; i < inv.ItemList.Count; i++)
            {
                if (i == 0)
                {
                    total[5] = inv.ItemList[i].ID + "," + inv.ItemList[i].Amount;
                }
                else
                {
                    total[5] += ";" + inv.ItemList[i].ID + "," + inv.ItemList[i].Amount;
                }
            }
            for (int i = 0; i < modifiers.Count; i++)
            {
                if (i==0)
                {
                    total[6] = i + ':'+modifiers[i].ToString();
                }
                else
                {
                    total[6] += ';'+i + ':' + modifiers[i].ToString();
                }
            }

            return total;
        }      
    }
}
