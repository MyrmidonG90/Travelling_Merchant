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

        enum TabState
        {
            Inventory,
            Skills,
            Achievements,
            Glossary
        }

        TabState tab;

        #region ut kommenterat
        /*
        bool disposing;
        bool dragging;
        bool fix;

        int numberToDispose;
        Rectangle mainBox;
        Rectangle inventoryBox;
        Button invTab;
        Button skillTab;
        Button achievementsTab;
        Button glossaryTab;

        Rectangle priCategoryBox;
        Rectangle secCategoryBox;
        Rectangle terCategoryBox;

        Button disposeButton;
        Button disposeOKButton;
        Button disposeDragger;
        Rectangle disposeBar;
        Rectangle disposeBox;
        Rectangle[] inventoryGrid;
        */
        #endregion

        List<TabClass> tabMenus; // Change this one if you add/remove tabs!!!

        int amountOfTabs; // Change this one if you add/remove tabs!!!
        List<Button> tabButtons;
        Button returnButton;
        Item selected;
        int selectedSquare;

        public PlayerInventoryModule()
        {
            tab = TabState.Inventory;
            returnButton = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
            amountOfTabs = 4; // Change this one if you add/remove tabs!!!
            CreateTabs(); // Change this one if you add/remove tabs!!!

            InitiateTabButtons();
        }
   
        public void Update(GameTime gameTime)
        {
            CheckTabClick();
            tabMenus[(int)tab].Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            returnButton.Draw(spriteBatch);
            tabMenus[(int)tab].Draw(spriteBatch);
            DrawTabs(spriteBatch);           
        }
        
        void InitiateTabButtons()
        {
            tabButtons = new List<Button>();
            for (int i = 0; i < amountOfTabs; i++)
            {
                tabButtons.Add(new Button(280+190*i,90,200,60,TextureManager.texTabs[i]));
            }
        }

        void DrawTabs(SpriteBatch sb)
        {
            for (int i = 0; i < tabButtons.Count; i++)
            {
                tabButtons[i].Draw(sb);
            }
            if (tab == TabState.Inventory)
            {
                sb.Draw(TextureManager.texTabSkillsDark, tabButtons[1].HitBox, Color.White);
                sb.Draw(TextureManager.texTabAchievementsDark, tabButtons[2].HitBox, Color.White);
                sb.Draw(TextureManager.texTabGlosasriesDark, tabButtons[3].HitBox, Color.White);
            }
            if (tab == TabState.Skills)
            {
                sb.Draw(TextureManager.texTabInvDark, tabButtons[0].HitBox, Color.White);
                sb.Draw(TextureManager.texTabAchievementsDark, tabButtons[2].HitBox, Color.White);
                sb.Draw(TextureManager.texTabGlosasriesDark, tabButtons[3].HitBox, Color.White);
            }
            if (tab == TabState.Achievements)
            {
                sb.Draw(TextureManager.texTabInvDark, tabButtons[0].HitBox, Color.White);
                sb.Draw(TextureManager.texTabSkillsDark, tabButtons[1].HitBox, Color.White);
                sb.Draw(TextureManager.texTabGlosasriesDark, tabButtons[3].HitBox, Color.White);
            }
            if (tab == TabState.Glossary)
            {
                sb.Draw(TextureManager.texTabInvDark, tabButtons[0].HitBox, Color.White);
                sb.Draw(TextureManager.texTabSkillsDark, tabButtons[1].HitBox, Color.White);
                sb.Draw(TextureManager.texTabAchievementsDark, tabButtons[2].HitBox, Color.White);
            }
        }

        void CreateTabs()
        {
            tabMenus = new List<TabClass>();
            tabMenus.Add(new InventoryTab());
            tabMenus.Add(new SkillTab());
            tabMenus.Add(new AchievementTab());
            tabMenus.Add(new GlossaryTab("Item Tab","Items"));
        }

        void CheckTabClick()
        {
            bool found = false;
            int counter = 0;
            while (found == false && counter != amountOfTabs)
            {
                if (tabButtons[counter].LeftClick())
                {
                    found = true;
                    tab = (TabState)counter;
                }
                else
                {
                    ++counter;
                }
            }

            //PENDING REMOVAL

            /*if (invTab.LeftClick() && skillTab.LeftClick())
            {
                tab = TabState.Inventory;
            }
            else if (invTab.LeftClick())
            {
                tab = TabState.Inventory;
            }
            else if (skillTab.LeftClick())
            {
                tab = TabState.Skills;
            }
            else if (achievementsTab.LeftClick())
            {
                tab = TabState.Achievements;
            }
            else if (glossaryTab.LeftClick())
            {
                tab = TabState.Glossary;
            }*/
        }
        public bool CheckExit()
        {
            if (KMReader.prevKeyState.IsKeyUp(Keys.Escape) && KMReader.keyState.IsKeyDown(Keys.Escape))
            {
                selected = null;
                selectedSquare = -1;
                return true;
            }
            if (returnButton.LeftClick())
            {
                selected = null;
                selectedSquare = -1;
                return true;
            }
            return false;
        }

        //PENDING REMOVAL

        /*private void CheckSelect()
        {
            if (KMReader.LeftMouseClick())
            {
                int counter = 0;
                foreach (Rectangle tempRectangle in inventoryGrid)
                {
                    if (tempRectangle.Contains(KMReader.GetMousePoint()) && !disposing)
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
        }
        private void LoadGrid()
        {
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

        private void CheckDraggerChange()
        {
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

        private void CheckDisposeEnd()
        {
            if (disposeOKButton.LeftClick())
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
                        selectedSquare = -1;
                        break;
                    }
                }
                numberToDispose = 0;
            }
        }

        

        private void UpdateInventory()
        {
            //Kollar om man har selectat ett item i inventoryn
            CheckSelect();

            //startar dispose processen
            if (disposeButton.LeftClick() && selected != null)
            {
                disposing = true;
            }

            if (disposing)
            {
                //stänger processen och resettar lämpliga värden
                CheckDisposeEnd();

                //startar slider 
                if (disposeDragger.LeftClick())
                {
                    dragging = true;
                    fix = true;
                }

                CheckDraggerChange();
            }
        }

        private void DrawDisposing(SpriteBatch spriteBatch)
        {
            if (disposing)
            {
                spriteBatch.Draw(TextureManager.texDisposeBox, disposeBox, Color.White);
                Vector2 temp = TextureManager.font32.MeasureString("Select amount to remove");
                spriteBatch.DrawString(TextureManager.font32, "Select amount to remove", new Vector2((1920 - temp.X) / 2, 270), Color.Black);

                temp = TextureManager.font32.MeasureString(numberToDispose.ToString());
                spriteBatch.DrawString(TextureManager.font32, numberToDispose.ToString(), new Vector2((1920 - temp.X) / 2, 370), Color.Black);

                spriteBatch.Draw(TextureManager.texDisposeBar, disposeBar, Color.White);

                disposeOKButton.Draw(spriteBatch);
                disposeDragger.Draw(spriteBatch);
            }
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            int counter = 0;
            foreach (Item tempItem in Player.Inventory.ItemList)
            {
                tempItem.Draw(spriteBatch, inventoryGrid[counter]);
                Vector2 temp = new Vector2();
                if (tempItem.Amount > 99)
                {
                    temp = new Vector2(inventoryGrid[counter].X + 36, inventoryGrid[counter].Y + 70);
                }
                else
                {
                    temp = new Vector2(inventoryGrid[counter].X + 60, inventoryGrid[counter].Y + 70);
                }
                spriteBatch.DrawString(TextureManager.font32, tempItem.Amount.ToString(), temp, Color.White);
                counter++;
            }
        }

        private void DrawInventory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texInvMenu, mainBox, Color.White);

            spriteBatch.DrawString(TextureManager.font24, Player.Inventory.Money.ToString(), new Vector2(440, 958), Color.Black);
            //spriteBatch.Draw(TextureManager.texWhite, inventoryBox, Color.DarkGray);


            DrawGrid(spriteBatch);

            if (selected != null)
            {
                if (selected.Rarity == 0)
                {
                    spriteBatch.Draw(TextureManager.texIconCommon, new Vector2(1110, 180), Color.White);
                }
                if (selected.Rarity == 1)
                {
                    spriteBatch.Draw(TextureManager.texIconUncommon, new Vector2(1110, 180), Color.White);
                }
                if (selected.Rarity == 2)
                {
                    spriteBatch.Draw(TextureManager.texIconRare, new Vector2(1110, 180), Color.White);
                }
                spriteBatch.DrawString(TextureManager.font48, selected.Name, new Vector2(1220, 200), Color.White);
                spriteBatch.DrawString(TextureManager.font32, "Info: \n" + selected.Description, new Vector2(1100, 310), Color.White);
                spriteBatch.DrawString(TextureManager.font32, "Standard Price: " + selected.BasePrice.ToString() + "c", new Vector2(1100, 660), Color.White);

                if (selected.PrimaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texCategories[selected.PrimaryCategory - 1], priCategoryBox, Color.White);
                }
                if (selected.SecondaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texCategories[selected.SecondaryCategory - 1], secCategoryBox, Color.White);

                }
                if (selected.TertiaryCategory != 999)
                {
                    spriteBatch.Draw(TextureManager.texCategories[selected.TertiaryCategory - 1], terCategoryBox, Color.White);
                }

                disposeButton.Draw(spriteBatch);
            }

            if (selectedSquare != -1)
            {
                spriteBatch.Draw(TextureManager.texSelect, inventoryGrid[selectedSquare], Color.White);
            }

            DrawDisposing(spriteBatch);
        }

        private void DrawSkills(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texSkillMenu, mainBox, Color.White);
            spriteBatch.DrawString(TextureManager.font32, "Wisdom: " + Player.ReturnSkillLevel("Wisdom"), new Vector2(400, 300), Color.Black);
            spriteBatch.DrawString(TextureManager.font32, "Intimidation: " + Player.ReturnSkillLevel("Intimidation"), new Vector2(400, 400), Color.Black);
            spriteBatch.DrawString(TextureManager.font32, "Persuasion: " + Player.ReturnSkillLevel("Persuasion"), new Vector2(400, 500), Color.Black);
            
        }


        private void DrawAchievements(SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(TextureManager.texSkillMenu, mainBox, Color.White);
            int temp = 0;
            foreach (Achievement achievement in AchievementManager.achievements)
            {
                
                spriteBatch.DrawString(TextureManager.font24, achievement.name +"  "+ achievement.progress , new Vector2(300, 200 + temp), Color.Black);
                spriteBatch.DrawString(TextureManager.font24, achievement.description, new Vector2(800, 200 + temp), Color.Black);
                temp += 60;
            }
            
        }*/

    }
}
