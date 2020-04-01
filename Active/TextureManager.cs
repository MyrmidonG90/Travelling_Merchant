using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    static class TextureManager
    {
        static public Texture2D texMap, box;
        static public void LoadContent(ContentManager content)
        {
            box = content.Load<Texture2D>("box");
            texMap = content.Load<Texture2D>("merchant_map_with_nothing");
        } 
      
    }
}
