using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// A simple GameOver - sequence. Will be showed after the game is completed.
    /// The Player may choose between 2 buttons 
    /// 1) Retry to retry a game
    /// 2) Back to main menu
    /// The procedures and methods are self explanatory
    /// </summary>
    class YouDiedScreen : Object
    {
        private SpriteFont _font;
        private List<PlayerButton> _buttons;
        private int _currentindex;
        private SoundEffect _song,_buttonPressed,_buttonSwitch;
        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            this.Type = ObjectTypes.Undefined;

            _font = contentManager.Load<SpriteFont>("Fonts/YouDiedFont");
            _song = contentManager.Load<SoundEffect>("Sounds/Misc/GameOverVoice");
            _buttonPressed = contentManager.Load<SoundEffect>("Sounds/ButtonSounds/ButtonPressed");
            _buttonSwitch = contentManager.Load<SoundEffect>("Sounds/ButtonSounds/ButtonSwitch");

            

            _song.Play();
            this.Position = new Vector2(GameConsts.ScreenWidth / 2, GameConsts.ScreenHeight / 2 - 240) - (_font.MeasureString("YOU DIED!!!")/2);

            _buttons = new List<PlayerButton>()
            {
                new PlayerButton(new Vector2(GameConsts.ScreenWidth / 2 - 250, 350), "Try again") {IsSelected = true},
                new PlayerButton(new Vector2(GameConsts.ScreenWidth/ 2 + 150, 350), "Back to menu")
            };

            foreach (var playerButton in _buttons)
            {
                CreateObject(playerButton);
            }

            InputManager.LeftPressed += LeftHandler;
            InputManager.RightPressed += RightHandler;
            InputManager.EnterPressed += EnterHandler;
            }

        private void EnterHandler()
        {
            _buttonPressed.Play();
            if (_currentindex == 0)
            {
                this.CurrentLevel.GamePlay.ChangeLevel(Level.GetFromFile("Content/Levels/Level0.lvl"));
                this.CurrentLevel.GamePlay.BackgroundMusic.Play();
            }
            if (_currentindex == 1)
            {
                this.CurrentLevel.GamePlay.BackToMenu();
            }
            
            HUD.Reset();
            }
      
        private void RightHandler()
        {
            _buttonSwitch.Play();
            if (_currentindex < 1)
            {
                _currentindex++;
            }
        }

        private void LeftHandler()
        {
            _buttonSwitch.Play();
            if (_currentindex > 0)
            {
                _currentindex--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var playerButton in _buttons)
            {
                playerButton.IsSelected = false;
            }
            _buttons[_currentindex].IsSelected = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(_font,"YOU DIED!!!", Position, Color.Red);
        }

        public override void Dispose()
        {
            
        }
    }
}
