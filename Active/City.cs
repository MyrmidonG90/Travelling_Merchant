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

        string name;
        string information;
        Vector2 coordinates;
        Inventory inv;
        StreamReader sr;
        StreamWriter sw;
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
        /*File structure
         * name;information;x;y;money;inventory 
         * Inventory structure
         * ;item1:amount,item2:amount,item3:amount
         * 
         */

        void LoadCity()
        {
            
            FileManager.ReadFile("");
            
            FileManager.SplitText('h', FileManager.fileText);

            //Ladda in fil
            //Sök efter stadens namnet
            //Läs in datan i en string
            //Splitta datan korrekt
            //Sätt rätt data till respektive stad


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
    }
}
