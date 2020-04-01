using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class TextureManager
    {
        static public Texture2D texMap;
        static public Texture2D texCarrot;
        static public Texture2D texPotato;
        static public Texture2D texIronIngot;
        static public void LoadContent(ContentManager content)
        {
            texMap = content.Load<Texture2D>("merchant_map_with_nothing");
            texCarrot = content.Load<Texture2D>("carrot");
            texPotato = content.Load<Texture2D>("potato");
            texIronIngot = content.Load<Texture2D>("iron_ingot");
        }
    }
}
