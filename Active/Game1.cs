using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        WorldModule worldModule;
        //Button Proof-of-Concept (kan tas bort utan risk)
        Button test;
        int temp;
        //===============
        MouseState oldMouseState;
        MouseState newMouseState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            //väldigt svårt att debugga i fullscreen så rekomenderar att lämna det av förutom när det behövs /My
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            worldModule = new WorldModule();
            test = new Button(100, 100, 100, 100);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();

            worldModule.Update(gameTime);

            //Kan tas bort
            if (test.Click(newMouseState, oldMouseState))
            {
                temp++;
            }
            //==================

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            worldModule.Draw(spriteBatch);
            test.Draw(spriteBatch);
            Window.Title = temp.ToString();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
