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
        static Button btnPauseMusic;

        static Rectangle box;
        static bool block;
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
            btnMain = new Button(830, 160, 260, 80, "main", "Main Menu", TextureManager.texButtonOptions);
            btnFullscreen = new Button(830, 160, 260, 80, "full", "Fullscreen", TextureManager.texButtonOptions);
            btnSkillCycle = new Button(830, 160, 260, 80, "skills", "Cycle Skill", TextureManager.texButtonOptions);

            btnSave = new Button(830, 260, 260, 80, "save", "Save Game", TextureManager.texButtonOptions);
            btnPauseMusic = new Button(830, 260, 260, 80, "pause music", "Toggle Music", TextureManager.texButtonOptions);
            btnSkillIncrease = new Button(830, 260, 260, 80, "inc", "+1 Skill", TextureManager.texButtonOptions);

            btnLoad = new Button(830, 360, 260, 80, "load", "Load Game", TextureManager.texButtonOptions);
            btnSkillDecrease = new Button(830, 360, 260, 80, "dec", "-1 Skill", TextureManager.texButtonOptions);

            btnDebug = new Button(830, 460, 260, 80, "debug", "Debug", TextureManager.texButtonOptions);
            btnWorldEventTest = new Button(830, 460, 260, 80, "we", "Test", TextureManager.texButtonOptions);

            btnOptions = new Button(830, 560, 260, 80, "options", "Options", TextureManager.texButtonOptions);
            btnOptionsReturn = new Button(830, 560, 260, 80, "options", "Return", TextureManager.texButtonOptions);

            btnClose = new Button(830, 660, 260, 80, "close", "Close", TextureManager.texButtonOptions);

            btnMenu = new Button(1812, 28, 80, 80, TextureManager.texOptions);

            box = new Rectangle(810, 140, 300, 620);

            menuToggle = MenuToggle.Standard;
        }

        static public bool CheckMainMenu()
        {
            if (btnMain.LeftClick() && menuToggle == MenuToggle.Standard)
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
            if (btnSave.LeftClick() && menuToggle == MenuToggle.Standard)
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
            if (btnLoad.LeftClick() && menuToggle == MenuToggle.Standard)
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
                if (btnFullscreen.LeftClick())
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

        static public bool CheckMusic()
        {
            if (menuToggle == MenuToggle.Options)
            {
                if (btnPauseMusic.LeftClick())
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
            if (btnMenu.LeftClick())
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

            CheckStandardMenu();

            CheckDebugMenu();

            if (menuToggle == MenuToggle.Debug || menuToggle == MenuToggle.Options)
            {
                if (btnOptionsReturn.LeftClick() && !block)
                {
                    menuToggle = MenuToggle.Standard;
                    block = true;
                }
            }
            
            if (btnClose.LeftClick())
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
                spriteBatch.Draw(TextureManager.texOptionsBox, box, Color.White);
                
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
                btnPauseMusic.Draw(spriteBatch);
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

        static private void CheckStandardMenu()
        {
            if (menuToggle == MenuToggle.Standard)
            {
                if (btnOptions.LeftClick() && !block)
                {
                    menuToggle = MenuToggle.Options;
                    block = true;
                }

                if (btnDebug.LeftClick() && !block)
                {
                    menuToggle = MenuToggle.Debug;
                    block = true;
                }
            }
        }

        static private void CheckDebugMenu()
        {
            if (menuToggle == MenuToggle.Debug)
            {
                CheckSkillCycle();
                CheckSkillIncrease();
                CheckSkillDecrease();
                CheckTest();
            }
        }

        static void CheckSkillCycle()
        {
            if (btnSkillCycle.LeftClick() && !block)
            {
                selectedSkill++;
                if (selectedSkill == 3)
                {
                    selectedSkill = 0;
                }
            }
        }

        static void CheckSkillIncrease()
        {
            if (btnSkillIncrease.LeftClick() && !block)
            {
                if (selectedSkill == 0)
                {
                    Player.AddXP("Wisdom", 100);
                }
                else if (selectedSkill == 1)
                {
                    Player.AddXP("Intimidation", 100);
                }
                else if (selectedSkill == 2)
                {
                    Player.AddXP("Persuasion", 100);
                }
            }
        }

        static void CheckSkillDecrease()
        {
            if (btnSkillDecrease.LeftClick() && !block)
            {
                if (selectedSkill == 0)
                {
                    Player.AddXP("Wisdom", -100);
                }
                else if (selectedSkill == 1)
                {
                    Player.AddXP("Intimidation", -100);
                }
                else if (selectedSkill == 2)
                {
                    Player.AddXP("Persuasion", -100);
                }
            }
        }

        static void CheckTest()
        {
            if (btnWorldEventTest.LeftClick() && !block)
            {
                string[] test = new string[1];
                test[0] = "Carrot Town";
                WorldEventManager.EventFire(2, test, new Random());
            }
        }
    }
}
