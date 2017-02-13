using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings
{

    class MainMenu : IGameState
    {

        private MainMenuState _menuState;
        private List<Button> _buttons;

        private Texture2D _mainMenuTexture2D;
        private Rectangle _rectangle;
        private bool _isEnter;

        private SoundEffect _buttonPressed, _buttonSwitch;
        private SoundEffectInstance _backGroundMusic;

        public void Init(ContentManager contentManager)
        {
            _menuState = MainMenuState.Start;
            _rectangle = new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight);
            _mainMenuTexture2D = contentManager.Load<Texture2D>("Sprites/Misc/MenuBackGround");

            _buttonPressed = contentManager.Load<SoundEffect>("Sounds/ButtonSounds/ButtonPressed");
            _buttonSwitch = contentManager.Load<SoundEffect>("Sounds/ButtonSounds/ButtonSwitch");
            _backGroundMusic = contentManager.Load<SoundEffect>("Sounds/MainMenu/MainMenu").CreateInstance();
          
            _backGroundMusic.IsLooped = true;          
            _backGroundMusic.Play();
           
            // List with all current Buttons and their placement
            _buttons = new List<Button>
            {
                new Button(contentManager, new Rectangle(415, 562, 193, 46), "Play"),
                new Button(contentManager, new Rectangle(415, 622, 193, 46), "Credits"),
                new Button(contentManager, new Rectangle(415, 682, 193, 46), "Exit")
            };
            
            
            //All avaiable Events on this State
            
            InputManager.MenuUpPressed += InputManagerOnUpPressed;
            InputManager.DownPressed += InputManagerOnDownPressed;
            InputManager.EnterPressed += InputManagerOnEnterPressed;
        }

        

        public GameState Update(GameTime gameTime)
        {
            // Clear all Selection && select current Button
            foreach (Button button in _buttons)
            {
                button.IsSelected = false;
            }

            _buttons[(int)_menuState].IsSelected = true;


            // Action - Enter (Change to the States )
            if (_isEnter)
            {
                switch (_menuState)
                {
                    case MainMenuState.Start:
                        _backGroundMusic.Stop();
                        return GameState.GamePlay;
                    case MainMenuState.Credits:
                        _backGroundMusic.Stop();
                        return GameState.Credits;
                    case MainMenuState.Exit:
                        Environment.Exit(0);
                        break;
                }
            }

            return GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            

            //Drawing the Background
            spriteBatch.Draw(_mainMenuTexture2D, _rectangle, Color.White);
            
            //Drawing all Buttons
            foreach (Button button in _buttons)
            {
                button.Draw(spriteBatch);
            }

    
        }

        /// <summary>
        /// Enter = Choose action for current Button + Sound
        /// </summary>
        private void InputManagerOnEnterPressed()
        {
            _isEnter = true;
            SoundEffectInstance tmp = _buttonPressed.CreateInstance();
            tmp.IsLooped = false;
            tmp.Pan = 0.5f;
            tmp.Play();
        }
        
        /// <summary>
        /// Scrolling down through the Menu + Sound
        /// </summary>
        private void InputManagerOnDownPressed()
        {
            if ((int)_menuState != 2)
            {
                _menuState++;
            }
            SoundEffectInstance tmp = _buttonSwitch.CreateInstance();
            tmp.IsLooped = false;
            tmp.Play();
            
        }

        /// <summary>
        /// Scrolling up through the Menu + Sound
        /// </summary>
        private void InputManagerOnUpPressed()
        {
            if ((int)_menuState != 0)
            {
                _menuState--;
            }
            SoundEffectInstance tmp = _buttonSwitch.CreateInstance();
            tmp.IsLooped = false;
            tmp.Play();
        }

        public void Dispose()
        {
            InputManager.ClearInput();
        }
    }
}
