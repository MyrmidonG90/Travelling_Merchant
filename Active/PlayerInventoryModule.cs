﻿using Microsoft.Xna.Framework;
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
        bool disposing;
        bool dragging;
        bool fix;
        Item selected;
        int selectedSquare;
        int numberToDispose;
        Rectangle mainBox;
        Rectangle inventoryBox;

        Rectangle priCategoryBox;
        Rectangle secCategoryBox;
        Rectangle terCategoryBox;

        Vector2 posPriCategoryString;
        Vector2 posSecCategoryString;
        Vector2 posTerCategoryString;

        Button disposeButton;
        Button disposeOKButton;
        Button disposeDragger;
        Button returnButton;
        Rectangle disposeBar;
        Rectangle disposeBox;
        Rectangle[] inventoryGrid;

        Color[] colors;
        string[] cats;

        public PlayerInventoryModule()
        {
            disposing = false;

            mainBox = new Rectangle(260, 140, 1400, 880);
            inventoryBox = new Rectangle(300, 180, 720, 720);

            priCategoryBox = new Rectangle(1100, 750, 120, 120);
            secCategoryBox = new Rectangle(1240, 750, 120, 120);
            terCategoryBox = new Rectangle(1380, 750, 120, 120);

            posPriCategoryString = new Vector2(1110, 785);
            posSecCategoryString = new Vector2(1250, 785);
            posTerCategoryString = new Vector2(1390, 785);

            disposeButton = new Button(1560, 920, 70, 70, TextureManager.texWhite);
            returnButton = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
            disposeBox = new Rectangle(660, 240, 600, 500);
            disposeBar = new Rectangle(710, 500, 520, 20);
            disposeDragger = new Button(710, 480, 20, 60, TextureManager.texWhite);
            disposeOKButton = new Button(900, 640, 120, 60, TextureManager.texWhite);

            colors = new Color[8];
            cats = new string[8];

            colors[0] = Color.Green;
            colors[1] = Color.DarkSlateGray;
            colors[2] = Color.LightGray;
            colors[3] = Color.SaddleBrown;
            colors[4] = Color.Gold;
            colors[5] = Color.DarkBlue;
            colors[6] = Color.Red;
            colors[7] = Color.Beige;

            cats[0] = "Food";
            cats[1] = "Metal";
            cats[2] = "Raw Materials";
            cats[3] = "Textiles";
            cats[4] = "Gear";
            cats[5] = "Magic";
            cats[6] = "Valuables";
            cats[7] = "Manufactured";

            selectedSquare = 50; //50 means no slot is selected

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
            streamReader.Close();
        }

        public bool CheckExit()
        {
            if (KMReader.prevKeyState.IsKeyUp(Keys.Escape) && KMReader.keyState.IsKeyDown(Keys.Escape))
            {
                return true;
            }
            if (returnButton.Click())
            {
                return true;
            }

            return false;
        }


        public void Update(GameTime gameTime)
        {
            //Kollar om man har selectat ett item i inventoryn
            if (KMReader.MouseClick())
            {
                int counter = 0;
                foreach (Rectangle tempRectangle in inventoryGrid)
                {
                    if (tempRectangle.Contains(KMReader.GetMousePoint()))
                    {
                        try
                        {
                            selected = Player.Inventory.ItemList[counter];
                            selectedSquare = counter;
                        }
                        catch
                        {
                            //Mys 10/10 felhantering
                        }
                    }
                    counter++;
                }
            }

            //startar dispose processen
            if (disposeButton.Click() && selected != null)
            {
                disposing = true;
            }

            if (disposing)
            {
                //stänger processen och resettar lämpliga värden
                if (disposeOKButton.Click())
                {
                    disposing = false;
                    dragging = false;
                    disposeDragger.HitBox = new Rectangle(710, disposeDragger.HitBox.Y, disposeDragger.HitBox.Width, disposeDragger.HitBox.Height);

                    selected.Amount -= numberToDispose;
                    foreach (Item tempItem in Player.Inventory.ItemList)
                    {
                        if (tempItem.Amount <= 0)
                        {
                            Player.Inventory.ItemList.Remove(tempItem);
                            selected = null;
                            selectedSquare = 50;
                            break;
                        }
                    }
                    numberToDispose = 0;
                }

                //startar slider 
                if (disposeDragger.Click())
                {
                    dragging = true;
                    fix = true;
                }

                if (KMReader.HeldMouseClick() && dragging == true)
                {
                    dragging = true;
                    fix = false;
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
                else if (!fix)
                {
                    dragging = false;
                }
            }         
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            returnButton.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.texWhite, mainBox, Color.Wheat);
            spriteBatch.DrawString(TextureManager.fontInventory, "Currency: " + Player.Inventory.Money.ToString(), new Vector2(300, 920), Color.White);
            spriteBatch.Draw(TextureManager.texWhite, inventoryBox, Color.DarkGray);

            int counter = 0;
            foreach (Item tempItem in Player.Inventory.ItemList)
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
                

                spriteBatch.DrawString(TextureManager.fontHeader, selected.Name, new Vector2(1100, 200), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Info: \n" + selected.Description, new Vector2(1100, 300), Color.White);
                spriteBatch.DrawString(TextureManager.fontInventory, "Standard Price: " + selected.BasePrice.ToString() + "c", new Vector2(1100, 650), Color.White);

                if (selected.PrimaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texBox, priCategoryBox, colors[selected.PrimaryCategory-1]);
                    spriteBatch.DrawString(TextureManager.fontInventory, cats[selected.PrimaryCategory-1], posPriCategoryString, Color.White);
                }
                if (selected.SecondaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texBox, secCategoryBox, colors[selected.SecondaryCategory-1]);
                    spriteBatch.DrawString(TextureManager.fontInventory, cats[selected.SecondaryCategory-1], posSecCategoryString, Color.White);
                }
                if (selected.TertiaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texBox, terCategoryBox, colors[selected.TertiaryCategory-1]);
                    spriteBatch.DrawString(TextureManager.fontInventory, cats[selected.TertiaryCategory-1], posTerCategoryString, Color.White);
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
                spriteBatch.Draw(TextureManager.texWhite, disposeBox, Color.LightGray);
                Vector2 temp = TextureManager.fontInventory.MeasureString("Select amount to remove");
                spriteBatch.DrawString(TextureManager.fontInventory, "Select amount to remove", new Vector2((1920-temp.X)/2, 260), Color.Black);

                temp = TextureManager.fontInventory.MeasureString(numberToDispose.ToString());
                spriteBatch.DrawString(TextureManager.fontInventory, numberToDispose.ToString(), new Vector2((1920 - temp.X) / 2, 360), Color.Black);

                spriteBatch.Draw(TextureManager.texWhite, disposeBar, Color.LightSeaGreen);

                disposeOKButton.Draw(spriteBatch);
                disposeDragger.Draw(spriteBatch);                
            }
        }
    }
}
