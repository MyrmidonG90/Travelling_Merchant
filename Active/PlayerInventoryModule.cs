using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        bool test;
        bool disposing;
        bool dragging;
        Inventory inventory;
        Item selected;
        int selectedSquare;
        int numberToDispose;
        Rectangle mainBox;
        Rectangle inventoryBox;
        Rectangle categoryBox;
        Button disposeButton;
        Button disposeOKButton;
        Button disposeDragger;
        Rectangle disposeBar;
        Rectangle disposeBox;
        Rectangle[] inventoryGrid;

        public PlayerInventoryModule(Inventory inv, ItemCreator itemCreator)
        {
            inventory = inv;
            test = true;
            disposing = false;

            mainBox = new Rectangle(260, 140, 1400, 880);
            inventoryBox = new Rectangle(300, 180, 720, 720);
            categoryBox = new Rectangle(1100, 750, 120, 120);
            disposeButton = new Button(1560, 920, 70, 70, TextureManager.WhiteTex);
            disposeBox = new Rectangle(660, 240, 600, 500);
            disposeBar = new Rectangle(710, 500, 520, 20);
            disposeDragger = new Button(710, 480, 20, 60, TextureManager.WhiteTex);
            disposeOKButton = new Button(900, 640, 120, 60, TextureManager.WhiteTex);

            selectedSquare = 50;

            StreamReader streamReader = new StreamReader("./Data/InventoryGrid.txt");
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

        public void Update(GameTime gameTime, ItemCreator itemCreator)
        {
            if (KMReader.MouseClick())
            {
                int counter = 0;
                foreach (Rectangle tempRectangle in inventoryGrid)
                {
                    if (tempRectangle.Contains(KMReader.GetMousePoint()))
                    {
                        try
                        {
                            selected = inventory.ItemList[counter];
                            selectedSquare = counter;
                        }
                        catch
                        {

                        }
                    }
                    counter++;
                }
            }

            if (disposeButton.Click() && selected != null)
            {
                //inventory.ItemList.Remove(selected);
                //selected = null;
                //selectedSquare = 50;
                disposing = true;
            }

            if (disposing)
            {
                if (disposeOKButton.Click())
                {
                    disposing = false;
                    disposeDragger.HitBox = new Rectangle(710, disposeDragger.HitBox.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);
                }
                if (disposeDragger.Click())
                {
                    dragging = true;
                }

                if (KMReader.HeldMouseClick())
                {
                    dragging = true;
                    if (KMReader.GetMouseVector2().X > disposeDragger.HitBox.X + (500 / 20) && disposeDragger.HitBox.X < 1211)
                    {
                        Vector2 temp = KMReader.GetMouseVector2();
                        temp.Y = disposeDragger.HitBox.Y;
                        Rectangle temp2 = new Rectangle((int)temp.X, (int)temp.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);
                        disposeDragger.HitBox = temp2;
                        numberToDispose = 5 * ((disposeDragger.HitBox.X - 710) / (500 / 20));
                    }
                    else if (KMReader.GetMouseVector2().X < disposeDragger.HitBox.X - (500f / 20) && disposeDragger.HitBox.X > 710)
                    {
                        Vector2 temp = KMReader.GetMouseVector2();
                        temp.Y = disposeDragger.HitBox.Y;
                        Rectangle temp2 = new Rectangle((int)temp.X, (int)temp.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);
                        disposeDragger.HitBox = temp2;
                        numberToDispose = 5 * ((disposeDragger.HitBox.X - 710) / (500 / 20));
                    }

                    if (disposeDragger.HitBox.X > 1211 && disposeDragger.HitBox.X > 710)
                    {
                        disposeDragger.HitBox = new Rectangle(1211, disposeDragger.HitBox.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);
                        numberToDispose = 100;
                    }

                    if (disposeDragger.HitBox.X < 710 && disposeDragger.HitBox.X < 1211)
                    {
                        disposeDragger.HitBox = new Rectangle(710, disposeDragger.HitBox.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);
                        numberToDispose = 0;
                    }
                }
                else
                {
                    dragging = false;
                }
            }

            if (test)
            {
                if (KMReader.prevKeyState.IsKeyUp(Keys.A) && KMReader.keyState.IsKeyDown(Keys.A))
                {
                    inventory.AddItem(itemCreator.createItem(1, 20));
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.B) && KMReader.keyState.IsKeyDown(Keys.B))
                {
                    inventory.AddItem(itemCreator.createItem(2, 30));
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.C) && KMReader.keyState.IsKeyDown(Keys.C))
                {
                    inventory.AddItem(itemCreator.createItem(3, 10));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.WhiteTex, mainBox, Color.Wheat);
            spriteBatch.DrawString(TextureManager.fontInventory, "Currency: " + inventory.Money.ToString(), new Vector2(300, 920), Color.White);
            spriteBatch.Draw(TextureManager.WhiteTex, inventoryBox, Color.DarkGray);

            int counter = 0;
            foreach (Item tempItem in inventory.ItemList)
            {
                tempItem.Draw(spriteBatch, inventoryGrid[counter]);
                Vector2 temp = new Vector2();
                if (tempItem.Amount > 99)
                {
                    temp = new Vector2(inventoryGrid[counter].X + 36, inventoryGrid[counter].Y + 60);
                }
                else
                {
                    temp = new Vector2(inventoryGrid[counter].X + 60, inventoryGrid[counter].Y + 60); 
                }
                spriteBatch.DrawString(TextureManager.fontInventory, tempItem.Amount.ToString(), temp, Color.White);
                counter++;
            }

            if (selected != null)
            {
                Vector2 posCategoryString = new Vector2(1110, 785);

                spriteBatch.DrawString(TextureManager.fontHeader, "Carrot", new Vector2(1100, 200), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Info: \n" + selected.Description, new Vector2(1100, 300), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Standard Price: " + selected.BasePrice.ToString() + "c", new Vector2(1100, 650), Color.White);

                if (selected.Category == 1)
                {
                    spriteBatch.Draw(TextureManager.texBox, categoryBox, Color.Green);
                    spriteBatch.DrawString(TextureManager.fontInventory, "Food", posCategoryString, Color.White);
                }
                else if (selected.Category == 2)
                {
                    spriteBatch.Draw(TextureManager.texBox, categoryBox, Color.DarkSlateGray);
                    spriteBatch.DrawString(TextureManager.fontInventory, "Metal", posCategoryString, Color.White);
                }

                disposeButton.Draw(spriteBatch);
                spriteBatch.DrawString(TextureManager.fontInventory, "D", new Vector2(1580, 930), Color.Black);
            }

            if (selectedSquare != 50)
            {
                spriteBatch.Draw(TextureManager.texSelect, inventoryGrid[selectedSquare], Color.White);
            }

            if (disposing)
            {
                spriteBatch.Draw(TextureManager.WhiteTex, disposeBox, Color.LightGray);
                Vector2 temp = TextureManager.fontInventory.MeasureString("Select amount to remove");
                spriteBatch.DrawString(TextureManager.fontInventory, "Select amount to remove", new Vector2((1920-temp.X)/2, 260), Color.Black);

                temp = TextureManager.fontInventory.MeasureString(numberToDispose.ToString());
                spriteBatch.DrawString(TextureManager.fontInventory, numberToDispose.ToString(), new Vector2((1920 - temp.X) / 2, 360), Color.Black);

                spriteBatch.Draw(TextureManager.WhiteTex, disposeBar, Color.LightSeaGreen);

                disposeOKButton.Draw(spriteBatch);
                disposeDragger.Draw(spriteBatch);
                if (dragging)
                {
                    spriteBatch.Draw(TextureManager.WhiteTex, Vector2.Zero, Color.Red);
                }
            }
        }

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }
        }
    }
}
