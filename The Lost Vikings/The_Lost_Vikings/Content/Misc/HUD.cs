using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Content.Characters.Misc;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// The typical HUD class. This one is showed on the Left-Up corner while Gameplay State. 
    /// Also this class contains the whole information ( Like current HP & etc ) about the player.
    /// All functions and procedures are self explanatory ( 'cuz of a good picked signatures)
    /// Part of HUD is static to be independed of changes levels
    /// Has one program Random class
    /// </summary>
    public class HUD : Object
    {
        private SpriteFont _font;
        private int _currentFps;
        public static double PlayedTime;
        private static int _currentLevelID;
        public static bool IsKeyPicked,IsDead;
        public static Random Randomizer = new Random();
        public static int PreviosHealth, CurrentHealth, Coins,Attempts;


        public SoundEffect Hitted, GameOver;
        
        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            CurrentHealth = GameConsts.PlayerHealth;
            _font = contentManager.Load<SpriteFont>("Fonts/HUD");
            Hitted = contentManager.Load<SoundEffect>("Sounds/Player/Hitted");
            GameOver = contentManager.Load<SoundEffect>("Sounds/Player/GameOver");
            this.Type = ObjectTypes.Undefined;

            
        }
        /// <summary>
        /// Function to change level via portal(change levelID and load)
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="currentLevel"></param>
        public static void ChangeLevel(int offset, Level currentLevel)
        {
           
            _currentLevelID += offset;

            currentLevel.GamePlay.ChangeLevel(Level.GetFromFile(String.Format("Content/Levels/Level{0}.lvl", _currentLevelID)));

        
        }

        public override void Update(GameTime gameTime)
        {
            double frameTime = gameTime.ElapsedGameTime.TotalSeconds;
            PlayedTime += frameTime;
            _currentFps = (int)(1/frameTime);


            if (IsDead)
            {
                _currentLevelID = 0;
            }
            if (PreviosHealth > CurrentHealth)
            {
                Hitted.Play();
            }

            PreviosHealth = CurrentHealth;

            if (CurrentHealth <= 0 && IsDead == false)

            {
                this.CurrentLevel.GamePlay.BackgroundMusic.Stop();
                GameOver.CreateInstance().Play();
                IsDead = true;

            }
        }
        /// <summary>
        /// Resetting all static variables to default 
        /// </summary>
        public static void Reset()
        {
            CurrentHealth = 100;
            Coins = 0;
            IsKeyPicked = false;
            IsDead = false;
            Cat.Reset();
            PlayedTime = 0;
            Attempts++;
        }
        /// <summary>
        /// Drawing in-game information
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsDead == false)
            {
                spriteBatch.DrawString(_font, "Played Time:" + (int)PlayedTime, new Vector2(0, -10), Color.Black);
                spriteBatch.DrawString(_font, "FPS:" + _currentFps.ToString(), new Vector2(0, 15), Color.Blue);
                spriteBatch.DrawString(_font, "Health:" + CurrentHealth.ToString(), new Vector2(0, 40), Color.Green);
                spriteBatch.DrawString(_font, "Coins:" + Coins.ToString(), new Vector2(0, 65), Color.Orange);
                spriteBatch.DrawString(_font, IsKeyPicked ? "Key Picked" : "Key Missing", new Vector2(0, 90), Color.Magenta);
            }
            
        }

        public override void Dispose()
        {
            
        }
    }
}
