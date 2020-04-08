using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    static class TextureManager
    {

        static public SpriteFont font;
        static public SpriteFont fontInventory;
        static public SpriteFont fontHeader;


        static public Texture2D texMap;
        static public Texture2D WhiteTex;
        static public Texture2D texCarrot;
        static public Texture2D texPotato;
        static public Texture2D texIronIngot;
        static public Texture2D texBox;
        static public Texture2D texSelect;
        static public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("File");
            fontInventory = content.Load<SpriteFont>("fontInventory");
            fontHeader = content.Load<SpriteFont>("fontHeader");

            texBox = content.Load<Texture2D>("box");
            texMap = content.Load<Texture2D>("merchant_map_with_nothing");
            WhiteTex = content.Load<Texture2D>("White");
            texCarrot = content.Load<Texture2D>("carrot");
            texPotato = content.Load<Texture2D>("potato");
            texIronIngot = content.Load<Texture2D>("iron_ingot");
            texSelect = content.Load<Texture2D>("select");
        }

    }
}
