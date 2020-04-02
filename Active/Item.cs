using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    class Item
    {
        private string name;
        private int basePrice;
        private Texture2D tex;
        private int id;
        private int category;
        private int amount;
        private string description;

        public Item(string n, int bp, Texture2D tex, int id, int c, int a, string d)
        {
            name = n;
            basePrice = bp;
            this.tex = tex;
            this.id = id;
            category = c;
            amount = a;
            description = d;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Rectangle pos)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        public int BasePrice
        {
            get
            {
                return basePrice;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
        }
        public int Category
        {
            get
            {
                return category;
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}
