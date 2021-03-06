﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class KMReader
    {
        public static KeyboardState keyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;
        static int scrollWheelValue,prevScrollWheelValue;
        
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
            prevScrollWheelValue = scrollWheelValue;
            scrollWheelValue = mouseState.ScrollWheelValue;
        }

        public static Vector2 GetMouseVector2()
        {
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }

        public static bool IfScrolledUp()
        {            
            return scrollWheelValue > prevScrollWheelValue ? true: false;
        }
        public static bool IfScrolledDown()
        {
            return scrollWheelValue < prevScrollWheelValue ? true : false;
        }

        public static Point GetMousePoint()
        {
            return mouseState.Position;
        }

        /// <summary>
        /// Used to check if mouse is hovering above the rectacngle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool OverRectangle(Rectangle rect)
        {
            return rect.Contains(mouseState.Position);
        }

        public static bool LeftClickRectangle(Rectangle rect)
        {
            return LeftMouseClick() && OverRectangle(rect) ? true:false;
        }

        public static bool RightClickRectangle(Rectangle rect)
        {
            return RightMouseClick() && OverRectangle(rect) ? true : false;
        }

        public static bool LeftMouseClick()
        {
            if (prevMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public static bool RightMouseClick()
        {
            if (prevMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
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

        public static bool KeyClicked(Keys key)
        {
            return keyState.IsKeyUp(key) == prevKeyState.IsKeyDown(key) ? true:false;
        }

        public static bool IsKeyDown(Keys key)
        {
            tmp = false;
            if (keyState.IsKeyDown(key))
            {
                tmp = true;
            }
            return tmp;
        }
    }
}
