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
        City city;
        City[] cities = new City[2];
        Button[] buttons = new Button[2];

        string temp;


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
            City city = new City("test", "hej", new Vector2(2,2));


            buttons[0] = new Button((int)city.carrotTownCords.X, (int)city.carrotTownCords.Y, 100, 100, "Carrot Town");
            buttons[1] = new Button((int)city.steelVilleCords.X, (int)city.steelVilleCords.Y, 100, 100, "Steelville");


            cities[0] = new City("Carrot Town", city.carrotTownInfo, city.carrotTownCords);
            cities[1] = new City("Steelville", city.steelVilleInfo, city.steelVilleCords);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
            
            worldModule.Update(gameTime);
            
            KMReader.Update();

            base.Update(gameTime);
            

            foreach (City city in cities)
            {
                foreach (Button button in buttons)
                {
                    if (button.Click(newMouseState, oldMouseState) && button.name == city.name)
                    {
                        temp = city.name;
                    }
                }
            }
            

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            worldModule.Draw(spriteBatch);


            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

            Window.Title = temp;

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
