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

        public PlayerInventoryModule(Inventory inv)
        {
            inventory = inv;
            Item temp = new Item(2, TextureManager.texCarrot, 1, 1, 60, "Carrot1");

            for (int i = 0; i < 12; i++)
            {
                inventory.AddItem(temp);
            }

            temp = new Item(10, TextureManager.texIronIngot, 3, 2, 50, "Ingot1");
            inventory.AddItem(temp);


            temp = new Item(6, TextureManager.texPotato, 2, 1, 90, "Potato1");
            for (int i = 0; i < 12; i++)
            {
                inventory.AddItem(temp);
            }

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
            if (KMReader.MouseClick())
            {
                int counter = 0;
                foreach (Rectangle tempRectangle in inventoryGrid)
                {
                    if (tempRectangle.Contains(KMReader.GetMousePoint()))
                    {
                        inventory.ItemList.RemoveAt(counter);
                    }
                    counter++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texBox, mainBox, Color.Black);
            spriteBatch.Draw(TextureManager.texBox, inventoryBox, Color.DarkGray);
            int counter = 0;
            foreach (Item tempItem in inventory.ItemList)
            {
                tempItem.Draw(spriteBatch, inventoryGrid[counter]);
                Vector2 temp = new Vector2(inventoryGrid[counter].X + 80, inventoryGrid[counter].Y + 80);
                spriteBatch.DrawString(TextureManager.fontInventory, tempItem.Amount.ToString(), temp, Color.White);
                counter++;
            }
            
        }
    }
}
