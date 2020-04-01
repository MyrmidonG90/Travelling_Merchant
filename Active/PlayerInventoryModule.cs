using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Active
{
    class PlayerInventoryModule
    {
        Inventory inventory;
        Rectangle mainBox;
        Rectangle inventoryBox;
        Rectangle[] inventoryGrid;

        public PlayerInventoryModule(Inventory i)
        {
            inventory = i;
            Item temp = new Item(2, TextureManager.texCarrot, 1, 1, 20, "Carrot1");
            inventory.AddItem(temp);
            inventory.AddItem(temp);
            mainBox = new Rectangle(260, 140, 1400, 800);
            inventoryBox = new Rectangle(300, 180, 720, 720);
            StreamReader streamReader = new StreamReader("InventoryGrid.txt");
            inventoryGrid = new Rectangle[25];
            int counter = 0;
            while (!streamReader.EndOfStream)
            {
                string newstring = streamReader.ReadLine();
                string[] temp2 = newstring.Split(',');
                Vector2 tempPos = new Vector2(int.Parse(temp2[0]), int.Parse(temp2[1]));
                inventoryGrid[counter] = new Rectangle((int)tempPos.X, (int)tempPos.Y, 120, 120);
                counter++;
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texBox, mainBox, Color.Black);
            spriteBatch.Draw(TextureManager.texBox, inventoryBox, Color.DarkGray);
            int counter = 0;
            foreach (Item tempItem in inventory.ItemList)
            {
                tempItem.Draw(spriteBatch, inventoryGrid[counter]);
                counter++;
            }
        }
    }
}
