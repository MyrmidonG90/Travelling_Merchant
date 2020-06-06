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

        enum TabState
        {
            Inventory,
            Skills,
            Achievements,
            Glossary
        }

        TabState tab;     

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
    }
}
