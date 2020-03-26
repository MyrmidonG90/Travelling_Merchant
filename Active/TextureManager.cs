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
    class TextureManager
    {
        static public Texture2D texMap;

        public TextureManager(ContentManager Content)
        {
            texMap = Content.Load<Texture2D>("merchant_map_with_nothing");
        }
    }
}
