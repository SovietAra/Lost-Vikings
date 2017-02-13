using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;
using Microsoft.Xna.Framework.Audio;

namespace The_Lost_Vikings.Content.Characters.Misc
{
    /// <summary>
    /// A simple Win - sequence. Will be showed after the game is completed.
    /// The HUD information will be showed up over all objects
    /// A enter will be need to exit this
    /// The procedures and methods are self explanatory
    /// </summary>
    class YouWinScreen : Object
    {
        private SpriteFont _font;
        private Vector2 _collectedCoins, _playedTime, _attempts,_continue;
        private int _currentPlayedTime;
        private SoundEffectInstance _winMusic;
        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            
            this.Type = ObjectTypes.Undefined;

            this._font = contentManager.Load<SpriteFont>("Fonts/YouDiedFont");

            _currentPlayedTime = (int) HUD.PlayedTime;

            _winMusic = contentManager.Load<SoundEffect>("Sounds/Misc/WinSong").CreateInstance();

            _winMusic.IsLooped = true;
            _winMusic.Play();

            this.Position = new Vector2(GameConsts.ScreenWidth / 2, GameConsts.ScreenHeight / 2 - 240) - (_font.MeasureString("YOU WIN!!!") / 2);
            this._collectedCoins = new Vector2(GameConsts.ScreenWidth / 2, Position.Y + 150) - (_font.MeasureString("Collected coins: " + HUD.Coins)/2);
            this._playedTime = new Vector2(GameConsts.ScreenWidth / 2, Position.Y + 250) - (_font.MeasureString("Played time: " + _currentPlayedTime) / 2);
            this._attempts = new Vector2(GameConsts.ScreenWidth / 2, Position.Y + 350) - (_font.MeasureString("Attemps: " + HUD.Attempts) / 2);
            this._continue = new Vector2(GameConsts.ScreenWidth / 2, Position.Y + 550) - (_font.MeasureString("Press Enter/A to return") / 2);

            HUD.IsDead = true;
            InputManager.EnterPressed += InputManagerOnEnterPressed;
        }

        private void InputManagerOnEnterPressed()
        {
            _winMusic.Stop();
            this.CurrentLevel.GamePlay.BackToMenu();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(_font, "YOU WIN!!!", Position, Color.GreenYellow);
            spriteBatch.DrawString(_font, "Collected coins: " + HUD.Coins,this._collectedCoins, Color.Black);
            spriteBatch.DrawString(_font, "Played time: " + _currentPlayedTime, this._playedTime, Color.Black);
            spriteBatch.DrawString(_font, "Attempts " + HUD.Attempts, this._attempts, Color.Black);
            spriteBatch.DrawString(_font, "Press Enter/X to return" , this._continue, Color.OrangeRed);
        }

        public override void Dispose()
        {
            InputManager.ClearInput();   
        }
    }
}
