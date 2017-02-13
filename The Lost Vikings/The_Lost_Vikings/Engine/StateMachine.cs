using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings
{
    /// <summary>
    /// Main type of our game
    /// </summary>
    public class StateMachine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameState _currentGameState;
        private IGameState _currentStateRef;


        public StateMachine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = GameConsts.ScreenHeight;
            graphics.PreferredBackBufferWidth = GameConsts.ScreenWidth;
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
            _currentGameState = GameState.SplashScreen;

            _currentStateRef = new SplashScreen();
            
            _currentStateRef.Init(Content);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            base.Update(gameTime);
            InputManager.Update();

            GameState cache = _currentStateRef.Update(gameTime);
            if (_currentGameState != cache)
            {
                _currentGameState = cache;
                _currentStateRef.Dispose();
                switch (_currentGameState)
                {
                    case GameState.SplashScreen:
                        _currentStateRef = new SplashScreen();
                        break;
                    case GameState.MainMenu:
                        _currentStateRef = new MainMenu();
                        break;
                    case GameState.Credits:
                        _currentStateRef = new Credits();
                        break;
                    case GameState.GamePlay:
                        _currentStateRef = new GamePlay();
                        break;
                }
                _currentStateRef.Init(Content);
                
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
                        
            base.Draw(gameTime);
            //Disable interpolation while drawing by spritebatch for better looking pixel-art
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            _currentStateRef.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
