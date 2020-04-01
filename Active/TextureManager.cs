using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    static class TextureManager
    {

        static public Texture2D texMap;
        static public Texture2D WhiteTex;
        static public Texture2D texCarrot;
        static public Texture2D texPotato;
        static public Texture2D texIronIngot;
        static public Texture2D texMap, texBox;
        static public void LoadContent(ContentManager content)
        {
            box = content.Load<Texture2D>("box");
            texMap = content.Load<Texture2D>("merchant_map_with_nothing");
            WhiteTex = content.Load<Texture2D>("White");
            texCarrot = content.Load<Texture2D>("carrot");
            texPotato = content.Load<Texture2D>("potato");
            texIronIngot = content.Load<Texture2D>("iron_ingot");
        }

    }
}
