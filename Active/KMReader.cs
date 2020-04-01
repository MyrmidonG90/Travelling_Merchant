using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class KMReader
    {
        public static KeyboardState keyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;

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
        public static bool MouseClick()
        {
            if (prevMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }
}
