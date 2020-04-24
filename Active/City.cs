using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    public class City
    {

        string name;
        string information;
        Vector2 coordinates;

        public City(string name, string information, Vector2 coordinates)
        {
            this.name = name;
            this.information = information;
            this.coordinates = coordinates;

        }

        public string Name
        {
            get
            {
                return name;
            }
        }
<<<<<<< Updated upstream
=======

        public void AddInventory(Inventory inv)
        {
            this.inv = inv;
        }
>>>>>>> Stashed changes

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
    }
}
