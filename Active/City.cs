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

        public string name;
        public string information;
        public Vector2 coordinates;

        public City(string name, string information, Vector2 coordinates)
        {
            this.name = name;
            this.information = information;
            this.coordinates = coordinates;

        }

    }
}
