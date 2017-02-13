using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Content.Credits;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings
{
    /// <summary>
    /// Game state to show the credits
    /// </summary>
    public class Credits : IGameState
    {
        private SpriteFont _font;
        private Texture2D _background;
        private List<TextLine> _text;
        private bool _enterPressed;
        private Timer _timer;

        public void Init(ContentManager contentManager)
        {
            _font = contentManager.Load<SpriteFont>("Fonts/HUD");
            _text = new List<TextLine>();

            _background = contentManager.Load<Texture2D>("Sprites/GamePlay/Background");

            StreamReader readStream = new StreamReader(File.Open("Content/Credits/Credits.cr", FileMode.Open));
            string tmp = readStream.ReadLine();
            int tmp2 = GameConsts.ScreenHeight;
            
            while (tmp != null)
            {
                _text.Add(new TextLine(new Vector2(GameConsts.ScreenWidth / 2, tmp2) - _font.MeasureString(tmp) / 2, tmp));
                tmp2 += 20;
                tmp = readStream.ReadLine();
            }
            readStream.Close();

            _timer = new Timer(GameConsts.CreditsTime) {IsActive = true};
            
            InputManager.EnterPressed += InputManagerOnEnterPressed;
        }

        private void InputManagerOnEnterPressed()
        {
            _enterPressed = true;
        }
        /// <summary>
        /// Scroll all textlines up to target time of timer(or exit by pressing enter).
        /// There you'll be returned to Main menu
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public GameState Update(GameTime gameTime)
        {
            if (_enterPressed)
            {
                return GameState.MainMenu;
            }
            if (_timer.Update(gameTime) == false)
            {
                foreach (TextLine line in _text)
                {
                    line.Position.Y -= 1;
                }
            }
            
            return GameState.Credits;
        }
        /// <summary>
        /// Drawing all textlines on their positions
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_background,Vector2.Zero, Color.White);
            foreach (TextLine line in _text)
            {
                spriteBatch.DrawString(_font,line.Text,line.Position,Color.White);
            }           
        }

        public void Dispose()
        {

        }
    }
}