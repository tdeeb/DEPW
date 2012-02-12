using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MathInfection
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont hudFont;
        private GamePadState oldGamePadState;
        private KeyboardState oldKeyboardState;

        private HeadsUpDisplay hud;

        private Player player1;
        private Player player2;
        private List<Enemy> enemyList;
        private List<Boss> bossList;
        private Texture2D player1Texture;
        private List<Texture2D> enemyTexList;
        private List<Texture2D> bossTexList;

        private Vector2 playerSize;
        private Vector2 windowSize;
        private Vector2 initialPlayerPosition;
        private Vector2 playerVelocity;

        private bool singleMode;
        private int numPlayers;
        private int numMoveStrategies;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Math Infection";

            oldGamePadState = GamePad.GetState(PlayerIndex.One);
            hud = new HeadsUpDisplay();
            enemyList = new List<Enemy>();
            bossList = new List<Boss>();
            enemyTexList = new List<Texture2D>();
            bossTexList = new List<Texture2D>();
            windowSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
            initialPlayerPosition = new Vector2(windowSize.X/2, windowSize.Y-60);
            playerVelocity = new Vector2(6, 6);
            // TODO: determine game mode: single or versus. Use single for now.
            singleMode = true;
            numPlayers = singleMode ? 1 : 2;
            numMoveStrategies = 3;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hudFont = Content.Load<SpriteFont>("HUDFont");

            player1Texture = Content.Load<Texture2D>(@"CharacterImages/Player1");
            playerSize = new Vector2(player1Texture.Width, player1Texture.Height);
            if(!singleMode)
            {
                // TODO: use player1's texture for now, might make a different tex for player2 later.
                // player2Texture = Content.Load<Texture2D>(@"CharacterImages/Player2");
            }
            player1 = new Player(player1Texture, initialPlayerPosition, playerVelocity, playerSize, windowSize);

            bossTexList.Add(Content.Load<Texture2D>(@"CharacterImages/Boss1"));
            Vector2 charSize = new Vector2(bossTexList[0].Width, bossTexList[0].Height);
            bossList.Add(new Boss(RandomGenerator.RandomMoveStrategy(numMoveStrategies), bossTexList[0],
                                  RandomGenerator.RandomPosition(windowSize, charSize), charSize, windowSize));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
               Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            CheckBoostKeyPress();

            player1.update(Vector2.Zero);
            foreach(Boss b in bossList)
            {
                b.update(player1.PlayerPosition);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player1.draw(spriteBatch);
            foreach(Boss b in bossList)
            {
                b.draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckBoostKeyPress()
        {
            GamePadState newGamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState newKeyboardState = Keyboard.GetState();
            // NOTE: assume user wouldn't switch between keyboard and gamepad while speeding up
            if (newGamePadState.IsButtonDown(Buttons.A) || newKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                if(!oldGamePadState.IsButtonDown(Buttons.A) || !oldKeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    player1.StartBoost = true;
                }
            }
            else if(oldGamePadState.IsButtonDown(Buttons.A) || oldKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                player1.StartBoost = false;
            }
            oldGamePadState = newGamePadState;
            oldKeyboardState = newKeyboardState;
        }
    }
}
