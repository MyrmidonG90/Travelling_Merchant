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
        private string information;
        private Vector2 Coordinates;

        public City(string name, string information, Vector2 Coordinates)
        {
            this.name = name;
            this.information = information;
            this.Coordinates = Coordinates;

        }

        public string carrotTownInfo = "Carrot Town is the great city of carrots. " +
                "It's people are simple agricultural people who pride themselves in their ability to grow the best carrots in the land";

        public Vector2 carrotTownCords = new Vector2(250, 500);

        public Vector2 steelVilleCords = new Vector2(500, 750);


        public string steelVilleInfo = "Steelville is a village that makes a living by mining and selling minerals. Iron, steel and rocks" +
                " can be bought for a reasonable price. However, the rocky terrains surrounding the village makes it close to " +
                "impossible to grow food. The city is dependant on trade in order to get nutritions";

    }
}
