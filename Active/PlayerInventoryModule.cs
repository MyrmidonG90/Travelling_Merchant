﻿using Microsoft.Xna.Framework;
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
        Item selected;
        Rectangle mainBox;
        Rectangle inventoryBox;
        Rectangle categoryBox;
        Rectangle disposeBox;
        Rectangle[] inventoryGrid;

        public PlayerInventoryModule(Inventory inv)
        {
            inventory = inv;
            Item temp;

            for (int i = 0; i < 12; i++)
            {
                temp = new Item(2, TextureManager.texCarrot, 1, 1, 60, "The Carrot has no natural \npredators");
                inventory.AddItem(temp);
            }

            temp = new Item(10, TextureManager.texIronIngot, 3, 2, 50, "An iron ingot what did you \nexpect?");
            inventory.AddItem(temp);


            
            for (int i = 0; i < 12; i++)
            {
                temp = new Item(6, TextureManager.texPotato, 2, 1, 90, "Potatoes are apex hunters");
                inventory.AddItem(temp);
            }

            mainBox = new Rectangle(260, 140, 1400, 800);
            inventoryBox = new Rectangle(300, 180, 720, 720);
            categoryBox = new Rectangle(1100, 650, 120, 120);
            disposeBox = new Rectangle(1100, 800, 280, 95);

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
                        //Item tempItem = inventory.ItemList[counter];
                        //inventory.ItemList.RemoveAt(counter);
                        //tempItem.Amount += 10;
                        //inventory.AddItem(tempItem);
                        //inventory.ItemList.RemoveAt(counter);
                        selected = inventory.ItemList[counter];
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
                Vector2 temp = new Vector2(inventoryGrid[counter].X + 60, inventoryGrid[counter].Y + 60);
                spriteBatch.DrawString(TextureManager.fontInventory, tempItem.Amount.ToString(), temp, Color.White);
                counter++;
            }
            if (selected != null)
            {
                //spriteBatch.Draw(TextureManager.texBox, Vector2.Zero, Color.White);
                spriteBatch.DrawString(TextureManager.fontHeader, "Carrot", new Vector2(1100, 200), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Info: \n" + selected.Description, new Vector2(1100, 300), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Standard Price: " + selected.BasePrice.ToString() + "c", new Vector2(1100, 550), Color.White);
                if (selected.Category == 1)
                {
                    spriteBatch.Draw(TextureManager.texBox, categoryBox, Color.Green);
                    spriteBatch.DrawString(TextureManager.fontInventory, "Food", new Vector2(1110, 685), Color.White);
                }
                else if (selected.Category == 2)
                {
                    spriteBatch.Draw(TextureManager.texBox, categoryBox, Color.DarkSlateGray);
                    spriteBatch.DrawString(TextureManager.fontInventory, "Metal", new Vector2(1110, 685), Color.White);
                }
                spriteBatch.Draw(TextureManager.texBox, disposeBox, Color.Red);
            }
        }
    }
}