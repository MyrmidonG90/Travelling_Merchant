using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class KMReader
    {
        public static KeyboardState keyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;
        static bool tmp;
        public static void Update()
        {
            if (prevMouseState != null)
            {
                prevMouseState = mouseState;
            }
            if (prevKeyState != null)
            {
                prevKeyState = keyState;
            }
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static Vector2 GetMouseVector2()
        {
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }

        public static Point GetMousePoint()
        {
            return mouseState.Position;
        }

        public static bool ClickRectangle(Rectangle rect)
        {
            tmp = false;
            if (rect.Contains(mouseState.Position))
            {
                tmp = true;
            }

            return tmp;
        }

        public static bool MouseClick()
        {
            if (prevMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public static bool HeldMouseClick()
        {
            if (prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

    }
}
