
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings
{
    /// <summary>
    /// The SplashScreen State
    /// The user has to Press Enter on Keyboard or X on Gamepad to choose a preferred device
    /// The Splashscreen will end after a delay (by a timer)
    /// </summary>
    public class SplashScreen : IGameState
    {
        private SpriteFont _font;
        private Texture2D _splashscTexture2D;
        private Rectangle _rectangle;
        private Timer _timer;
        private string _text;
        public void Init(ContentManager contentManager)
        {
            _splashscTexture2D = contentManager.Load<Texture2D>("Sprites/Misc/MenuBackGround");
            _font = contentManager.Load<SpriteFont>("Fonts/HUD");
            _text = "Choose between Keyboard(Enter) or Gamepad(X)";

            _rectangle = new Rectangle(0,0,GameConsts.ScreenWidth, GameConsts.ScreenHeight);
            _timer = new Timer(GameConsts.SplashScreenShowTime);

            InputManager.EnterPressed += InputManagerOnEnterPressed;
            InputManager.AttackPressed += InputManagerOnXPressed;
        }
        /// <summary>
        /// Disable both inputs and change to GamePad
        /// </summary>
        private void InputManagerOnXPressed()
        {
            InputManager.IsKeyboard = false;
            InputManager.SplashScreenState = false;
        }
        /// <summary>
        /// Disable both inputs and change to Keyboard
        /// </summary>
        private void InputManagerOnEnterPressed()
        {
            InputManager.IsKeyboard = true;
            InputManager.SplashScreenState = false;
        }

        public GameState Update(GameTime gameTime)
        {
            if (InputManager.SplashScreenState== false)
            {
                if (_timer.Update(gameTime))
                {
                    _timer.Reset();
                    _timer.IsActive = false;
                    InputManager.ClearInput();
                    return GameState.MainMenu;
                }
                _timer.IsActive = true;
            }
            
            return GameState.SplashScreen;
        }

        

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            spriteBatch.Draw(_splashscTexture2D, _rectangle, Color.White);
            if (InputManager.SplashScreenState)
            {
                spriteBatch.DrawString(_font, _text, new Vector2(GameConsts.ScreenWidth / 2, GameConsts.ScreenHeight / 2 + 300 ) - (_font.MeasureString(_text)/2),Color.Black);
            }
        }

        public void Dispose()
        {

        }
    }
}