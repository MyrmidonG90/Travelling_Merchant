using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class OptionsMenu
    {
        static Button btnMain;
        static Button btnSave;
        static Button btnLoad;
        static Button btnOptions;
        static Button btnFullscreen;
        static Button btnMenu;
        static Button btnOptionsReturn;
        static Button btnClose;
        static Button btnDebug;
        static Button btnSkillCycle;
        static Button btnSkillIncrease;
        static Button btnSkillDecrease;
        static Button btnWorldEventTest;

        static Rectangle box;
        static Rectangle ring;

        static bool alt;
        static bool full;
        static bool block;
        static bool debug;

        static public int selectedSkill;

        enum MenuToggle
        {
            Standard,
            Options,
            Debug,
        }

        static MenuToggle menuToggle;

        static public void Init()
        {
            btnMain = new Button(830, 160, 260, 80, "main", "Main Menu", TextureManager.texWhite);
            btnFullscreen = new Button(830, 160, 260, 80, "full", "Fullscreen", TextureManager.texWhite);
            btnSkillCycle = new Button(830, 160, 260, 80, "skills", "Cycle Skill", TextureManager.texWhite);

            btnSave = new Button(830, 260, 260, 80, "save", "Save Game", TextureManager.texWhite);
            btnSkillIncrease = new Button(830, 260, 260, 80, "inc", "+1 Skill", TextureManager.texWhite);

            btnLoad = new Button(830, 360, 260, 80, "load", "Load Game", TextureManager.texWhite);
            btnSkillDecrease = new Button(830, 360, 260, 80, "dec", "-1 Skill", TextureManager.texWhite);

            btnDebug = new Button(830, 460, 260, 80, "debug", "Debug", TextureManager.texWhite);
            btnWorldEventTest = new Button(830, 460, 260, 80, "we", "Test", TextureManager.texWhite);

            btnOptions = new Button(830, 560, 260, 80, "options", "Options", TextureManager.texWhite);
            btnOptionsReturn = new Button(830, 560, 260, 80, "options", "Return", TextureManager.texWhite);

            btnClose = new Button(830, 660, 260, 80, "close", "Close", TextureManager.texWhite);

            btnMenu = new Button(1820, 20, 80, 80, TextureManager.texOptions);

            box = new Rectangle(810, 140, 300, 620);

            alt = false;
            full = false;
            debug = false;
            menuToggle = MenuToggle.Standard;
        }

        static public bool CheckMainMenu()
        {
            if (btnMain.Click() && menuToggle == MenuToggle.Standard)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CheckSaveGame()
        {
            if (btnSave.Click() && menuToggle == MenuToggle.Standard)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CheckLoadGame()
        {
            if (btnLoad.Click() && menuToggle == MenuToggle.Standard)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       

        static public bool CheckFullscreen()
        {
            if (menuToggle == MenuToggle.Options)
            {
                if (btnFullscreen.Click())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        static public bool CheckMenuToggle()
        {
            if (btnMenu.Click())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool Update()
        {
            block = false;
            if (btnOptions.Click() && menuToggle == MenuToggle.Standard && !block)
            {
                menuToggle = MenuToggle.Options;
                block = true;
            }
            if (btnOptionsReturn.Click() && menuToggle != MenuToggle.Standard && !block)
            {
                menuToggle = MenuToggle.Standard;
                block = true;
            }
            if (btnDebug.Click() && menuToggle == MenuToggle.Standard && !block)
            {
                menuToggle = MenuToggle.Debug;
                block = true;
            }
            if (btnSkillCycle.Click() && menuToggle == MenuToggle.Debug && !block)
            {
                selectedSkill++;
                if (selectedSkill == 3)
                {
                    selectedSkill = 0;
                }
            }
            if (btnSkillIncrease.Click() && menuToggle == MenuToggle.Debug && !block)
            {
                if (selectedSkill == 0)
                {
                    Player.SetSkillLevel("Wisdom", 0);
                }
                else if (selectedSkill == 1)
                {
                    Player.SetSkillLevel("Intimidation", 0);
                }
                else if (selectedSkill == 2)
                {
                    Player.SetSkillLevel("Persuasion", 0);
                }
            }
            if (btnSkillDecrease.Click() && menuToggle == MenuToggle.Debug && !block)
            {
                if (selectedSkill == 0)
                {
                    Player.SetSkillLevel("Wisdom", -1f);
                }
                else if (selectedSkill == 1)
                {
                    Player.SetSkillLevel("Intimidation", -1f);
                }
                else if (selectedSkill == 2)
                {
                    Player.SetSkillLevel("Persuasion", -1f);
                }
            }
            if (btnWorldEventTest.Click() && menuToggle == MenuToggle.Debug && !block)
            {
                string[] test = new string[1];
                test[0] = "Carrot Town";
                WorldEventManager.EventFire(1, test, new Random());
            }
            if (btnClose.Click())
            {
                menuToggle = MenuToggle.Standard;
                return true;
            }
            return false;
        }

        static public void Draw(SpriteBatch spriteBatch, bool mode)
        {
            if (mode)
            {
                spriteBatch.Draw(TextureManager.texWhite, box, Color.DarkGray);
                
                btnClose.Draw(spriteBatch);
            }

            if (menuToggle == MenuToggle.Standard && mode)
            {
                btnMain.Draw(spriteBatch);
                btnSave.Draw(spriteBatch);
                btnLoad.Draw(spriteBatch);
                btnDebug.Draw(spriteBatch);
                btnOptions.Draw(spriteBatch);
            }
            else if (menuToggle == MenuToggle.Options && mode)
            {
                btnFullscreen.Draw(spriteBatch);
                btnOptionsReturn.Draw(spriteBatch);
            }
            else if (menuToggle == MenuToggle.Debug && mode)
            {
                btnOptionsReturn.Draw(spriteBatch);
                btnSkillIncrease.Draw(spriteBatch);
                btnSkillDecrease.Draw(spriteBatch);
                btnSkillCycle.Draw(spriteBatch);
                btnWorldEventTest.Draw(spriteBatch);
            }

            btnMenu.Draw(spriteBatch);
        }
    }
}
