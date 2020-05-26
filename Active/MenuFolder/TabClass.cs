using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
     abstract class TabClass
    {
        protected string name;
        protected Rectangle mainBox;

        public string Name { get => name;}

        abstract public void Update();
        abstract public void Draw(SpriteBatch sb);
    }
}
