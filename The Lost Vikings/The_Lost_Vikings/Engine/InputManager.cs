using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using The_Lost_Vikings.Content;

namespace The_Lost_Vikings.Engine
{
    /// <summary>
    /// This static class is an Input Manager based on C# events instead standart(based on bools and comparasing in update methods)
    /// </summary>
    static class InputManager
    {
        //Bools to able choose between GPad & Keyboard
        public static bool SplashScreenState = true, IsKeyboard;
        //All input events used in game
        public static event Action Up,
            Down ,
            Left ,
            Right ,
            Enter ,
            Attack ,
            MenuUp,
            MenuUpPressed,
            UpPressed ,
            DownPressed ,
            EnterPressed ,
            LeftPressed ,
            RightPressed ,
            AttackPressed ,
            AttackUp ,
            UpUp ,
            DownUp ,
            LeftUp ,
            RightUp ,
            ChangeCharacterPressed ,
            ChangeCaracter;

        private static KeyboardState _currentKeyboardState, _pastKeyboardState;
        private static GamePadState _currentGamePadState, _pastGamePadState;

        public static void Update()
        {
            //Handle input in splashscreen to choose variants
            if (SplashScreenState)
            {
                SplashScreenControls();
            }
            else
            {
                //Handle choosed type of input
                if (IsKeyboard)
                {
                    _currentKeyboardState = Keyboard.GetState();
                    KeyboardInput(Keys.A, Left, LeftPressed, LeftUp);
                    KeyboardInput(Keys.W, Up, UpPressed, UpUp);
                    KeyboardInput(Keys.S, Down, DownPressed, DownUp);
                    KeyboardInput(Keys.W, MenuUp, MenuUpPressed, UpUp);

                    KeyboardInput(Keys.Enter, Enter, EnterPressed);
                    KeyboardInput(Keys.J, Attack, AttackPressed, AttackUp);
                    KeyboardInput(Keys.D, Right, RightPressed, RightUp);
                    KeyboardInput(Keys.Q, ChangeCaracter, ChangeCharacterPressed);
                    _pastKeyboardState = _currentKeyboardState;
                }
                else
                {
                    _currentGamePadState = GamePad.GetState(0);
                    
                    GamePadInputButtons(Buttons.A, Enter, EnterPressed);
                    GamePadInputButtons(Buttons.A, Up, UpPressed, UpUp);
                    GamePadInputButtons(Buttons.X, Attack, AttackPressed, AttackUp);
                    GamePadInputButtons(Buttons.Y, ChangeCaracter, ChangeCharacterPressed);
                    GamePadInputButtons(Buttons.DPadLeft, Left, LeftPressed, LeftUp);
                    GamePadInputButtons(Buttons.DPadRight, Right, RightPressed, RightUp);
                    GamePadInputButtons(Buttons.DPadDown, Down, DownPressed, DownUp);
                    GamePadInputButtons(Buttons.DPadUp, MenuUp, MenuUpPressed);
                    _pastGamePadState = _currentGamePadState;
                }
            }

        }
        /// <summary>
        /// Handle input in splashscreen(when you choosing input method)
        /// </summary>
        static void SplashScreenControls()
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(0);

            KeyboardInput(Keys.Enter, Enter,EnterPressed);
            GamePadInputButtons(Buttons.X, Attack,AttackPressed);

            _pastKeyboardState = _currentKeyboardState;
            _pastGamePadState = _currentGamePadState;
        }

        /// <summary>
        /// Handle GamePad buttons
        /// </summary>
        /// <param name="button">Gamepad button to handle</param>
        /// <param name="keyEvent">Event that handles every time while key pressed</param>
        /// <param name="pressedKeyEvent">Event that handles only when key was pressed(not every time)</param>
        /// <param name="upKeyEvent">>Event that handles only when key was upped</param>

        static void GamePadInputButtons(Buttons button, Action keyEvent, Action pressedKeyEvent, Action upKeyEvent = null)
        {
            if (_currentGamePadState.IsButtonDown(button) && _pastGamePadState.IsButtonDown(button))
            {
                if (keyEvent != null) keyEvent();
            }
            else if (_currentGamePadState.IsButtonDown(button) && (_pastGamePadState.IsButtonUp(button)))
            {
                if (pressedKeyEvent != null) pressedKeyEvent();
            }
            else if (_currentGamePadState.IsButtonUp(button) && _pastGamePadState.IsButtonDown(button))
            {
                if (upKeyEvent != null) upKeyEvent();
            }
        }

        /// <summary>
        /// Handle Keyboard buttons
        /// </summary>
        /// <param name="key">Gamepad button to handle</param>
        /// <param name="keyEvent">Event that handles every time while key pressed</param>
        /// <param name="pressedKeyEvent">Event that handles only when key was pressed(not every time)</param>
        /// <param name="upKeyEvent">>Event that handles only when key was upped</param>
        /// 
        static void KeyboardInput(Keys key, Action keyEvent, Action pressedKeyEvent, Action upKeyEvent = null)
        {

            if (_currentKeyboardState.IsKeyDown(key) && _pastKeyboardState.IsKeyDown(key))
            {
                if (keyEvent != null) keyEvent();
            }
            else if (_currentKeyboardState.IsKeyDown(key) && (_pastKeyboardState.IsKeyUp(key)))
            {
                if (pressedKeyEvent != null) pressedKeyEvent();
            }
            else if(_currentKeyboardState.IsKeyUp(key) && _pastKeyboardState.IsKeyDown(key))
            {
                if (upKeyEvent != null) upKeyEvent();
            }
        }
    

        /// <summary>
        /// This method clearing all Input
        /// </summary>
        public static void ClearInput()
        {
            Up = null;
            Down = null;
            Enter = null;
            Left = null;
            MenuUp = null;
            MenuUpPressed = null;
            LeftPressed = null;
            LeftUp = null;
            Right = null;
            RightPressed = null;
            UpPressed = null;
            DownPressed = null;
            EnterPressed = null;
            ChangeCaracter = null;
            ChangeCharacterPressed = null;
            AttackPressed = null;
            Attack = null;
            AttackUp = null;
        }
    }
}
