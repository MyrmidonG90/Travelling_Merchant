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
        static Button main;
        static Button save;
        static Button load;
        static Button options;
        static Button fullscreen;
        static Button menu;
        static Button optionsReturn;
        static Button close;

        static Rectangle box;
        static Rectangle ring;

        static bool alt;
        static bool full;
        static bool block;

        static public void Init()
        {
            main = new Button(830, 160, 260, 80, "main", "Main Menu", TextureManager.texWhite);
            fullscreen = new Button(830, 160, 260, 80, "full", "Fullscreen", TextureManager.texWhite);

            save = new Button(830, 260, 260, 80, "save", "Save Game", TextureManager.texWhite);

            load = new Button(830, 360, 260, 80, "load", "Load Game", TextureManager.texWhite);

            options = new Button(830, 560, 260, 80, "options", "Options", TextureManager.texWhite);
            optionsReturn = new Button(830, 560, 260, 80, "options", "Return", TextureManager.texWhite);

            close = new Button(830, 660, 260, 80, "close", "Close", TextureManager.texWhite);


            menu = new Button(1820, 20, 80, 80, TextureManager.texBackArrow);

            box = new Rectangle(810, 140, 300, 620);

            alt = false;
            full = false;
        }

        static public bool CheckMainMenu()
        {
            if (main.Click() && !alt)
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
            if (save.Click() && !alt)
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
            if (load.Click() && !alt)
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
            if (alt)
            {
                if (fullscreen.Click())
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
            if (menu.Click())
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
            if (options.Click() && !alt && !block)
            {
                alt = true;
                block = true;
            }
            if (optionsReturn.Click() && alt && !block)
            {
                alt = false;
                block = true;
            }
            if (close.Click())
            {
                return true;
            }
            return false;
        }

        static public void Draw(SpriteBatch spriteBatch, bool mode)
        {
            if (mode)
            {
                spriteBatch.Draw(TextureManager.texWhite, box, Color.DarkGray);
                close.Draw(spriteBatch);
            }

            if (!alt && mode)
            {
                main.Draw(spriteBatch);
                save.Draw(spriteBatch);
                load.Draw(spriteBatch);
                options.Draw(spriteBatch);
            }
            else if (alt && mode)
            {
                fullscreen.Draw(spriteBatch);
                optionsReturn.Draw(spriteBatch);
            }

            menu.Draw(spriteBatch);
        }
    }
}
