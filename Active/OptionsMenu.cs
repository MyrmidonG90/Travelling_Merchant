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

        static Rectangle box;
        static Rectangle ring;

        static bool alt;
        static bool full;

        static public void Init()
        {
            main = new Button(830, 160, 260, 120, "main", "Main Menu", TextureManager.texWhite);
            save = new Button(830, 300, 260, 120, "save", "Save Game", TextureManager.texWhite);
            load = new Button(830, 440, 260, 120, "load", "Load Game", TextureManager.texWhite);
            options = new Button(830, 580, 260, 120, "options", "Options", TextureManager.texWhite);
            fullscreen = new Button(830, 160, 260, 120, "full", "Fullscreen", TextureManager.texWhite);
            menu = new Button(1820, 20, 80, 80, TextureManager.texBackArrow);

            box = new Rectangle(810, 140, 300, 580);

            alt = false;
            full = false;
        }

        static public bool CheckMainMenu()
        {
            if (main.Click())
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
            if (save.Click())
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
            if (load.Click())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void CheckOptions()
        {
            if (options.Click())
            {
                //disabled until i fix fullscreen
                //alt = true; 
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

        static public void Draw(SpriteBatch spriteBatch, bool mode)
        {
            if (mode)
            {
                spriteBatch.Draw(TextureManager.texWhite, box, Color.DarkGray);
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
            }

            menu.Draw(spriteBatch);
        }
    }
}
