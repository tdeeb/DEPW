//There is a bit of commented out code that I used to test 
//for character movement. It all works, but is being moved
//into Player.cs. I left it behind to be removed after the
//movement works with the Player.cs class
//-Jason

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MathInfection
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player user = new Player();

/*      Moved to Player.cs for neatness   
        Texture2D mainChar;
        Vector2 userPosition;
        int userSpeed = 5;
*/
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            //Calls init function from Player.cs
            user.init();

/*          Sets character position to center screen
            userPosition.X = Window.ClientBounds.Width / 2;
            userPosition.Y = Window.ClientBounds.Height / 2;
 */
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load Main Character
            //mainChar = Content.Load<Texture2D>("Player\\CharacterSprite");
       
            //Calls load function from Player.cs
            user.load();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            
            //Calls UpdatePlayer function from Player.cs
            user.UpdatePlayer();

            base.Update(gameTime);
        }

        //Track User Input 
/*        private void UpdatePlayer()
        {
            KeyboardState newState = Keyboard.GetState();
            //Check for Left key
            if (newState.IsKeyDown(Keys.Left))
            {
                userPosition.X -= userSpeed;
            }
            //Check for Right Key
            if (newState.IsKeyDown(Keys.Right))
            {
                userPosition.X += userSpeed;
            }
            //Check for Up Key
            if (newState.IsKeyDown(Keys.Up))
            {
                userPosition.Y -= userSpeed;
            }
            //Check for Down Key
            if (newState.IsKeyDown(Keys.Down))
            {
                userPosition.Y += userSpeed;
            }

        }
*/

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Calls from draw function from Player.cs
            user.draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
