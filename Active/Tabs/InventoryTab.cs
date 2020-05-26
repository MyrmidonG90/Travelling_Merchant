using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    class InventoryTab : TabClass
    {
        Button disposeButton;
        Button disposeOKButton;
        Button disposeDragger;
        Item selected;
        bool disposing;
        bool dragging;
        bool fix;
        int selectedSquare;
        int numberToDispose;
        Rectangle disposeBar;
        Rectangle disposeBox;
        Rectangle[] inventoryGrid;
        Rectangle priCategoryBox;
        Rectangle secCategoryBox;
        Rectangle terCategoryBox;
        Rectangle inventoryBox;

        public InventoryTab()
        {
            name = "Inventory Tab";
            mainBox = new Rectangle(260, 150, 1400, 880);
            inventoryBox = new Rectangle(300, 170, 720, 720);

            priCategoryBox = new Rectangle(1130, 760, 120, 120);
            secCategoryBox = new Rectangle(1310, 760, 120, 120);
            terCategoryBox = new Rectangle(1490, 760, 120, 120);

            disposeButton = new Button(1560, 930, 70, 70, TextureManager.texIconTrashCan);
            //returnButton = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
            disposeBox = new Rectangle(660, 250, 600, 500);
            disposeBar = new Rectangle(710, 510, 520, 20);
            disposeDragger = new Button(710, 488, 30, 70, TextureManager.texDisposeDragger);
            disposeOKButton = new Button(900, 650, 120, 60, TextureManager.texWhite);

            selectedSquare = -1; //-1 means no slot is selected

            LoadGrid();
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texInvMenu, mainBox, Color.White);

            sb.DrawString(TextureManager.font24, Player.Inventory.Money.ToString(), new Vector2(440, 958), Color.Black);
            //spriteBatch.Draw(TextureManager.texWhite, inventoryBox, Color.DarkGray);


            DrawGrid(sb);

            if (selected != null)
            {
                if (selected.Rarity == 0)
                {
                    sb.Draw(TextureManager.texIconCommon, new Vector2(1110, 180), Color.White);
                }
                if (selected.Rarity == 1)
                {
                    sb.Draw(TextureManager.texIconUncommon, new Vector2(1110, 180), Color.White);
                }
                if (selected.Rarity == 2)
                {
                    sb.Draw(TextureManager.texIconRare, new Vector2(1110, 180), Color.White);
                }
                sb.DrawString(TextureManager.font48, selected.Name, new Vector2(1220, 200), Color.White);
                sb.DrawString(TextureManager.font32, "Info: \n" + selected.Description, new Vector2(1100, 310), Color.White);
                sb.DrawString(TextureManager.font32, "Standard Price: " + selected.BasePrice.ToString() + "c", new Vector2(1100, 660), Color.White);

                if (selected.PrimaryCategory != 999)
                {
                    sb.Draw(TextureManager.texCategories[selected.PrimaryCategory - 1], priCategoryBox, Color.White);
                }
                if (selected.SecondaryCategory != 999)
                {
                    sb.Draw(TextureManager.texCategories[selected.SecondaryCategory - 1], secCategoryBox, Color.White);

                }
                if (selected.TertiaryCategory != 999)
                {
                    sb.Draw(TextureManager.texCategories[selected.TertiaryCategory - 1], terCategoryBox, Color.White);
                }

                disposeButton.Draw(sb);
            }

            if (selectedSquare != -1)
            {
                sb.Draw(TextureManager.texSelect, inventoryGrid[selectedSquare], Color.White);
            }

            DrawDisposing(sb);
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

        public override void Update()
        {
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
        private void CheckSelect()
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
    }
}
