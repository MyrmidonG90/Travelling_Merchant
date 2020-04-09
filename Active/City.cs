using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class City
    {

        private string name;
        private string information;
        private Vector2 coordinates;

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

        public string Information
        {
            get
            {
                return information;
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
