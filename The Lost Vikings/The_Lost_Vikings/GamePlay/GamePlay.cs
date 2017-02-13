using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Content;
using The_Lost_Vikings.Content.Characters.Misc;

namespace The_Lost_Vikings
{
    /// <summary>
    /// Class of Gameplay state. 
    /// </summary>
    public class GamePlay : IGameState
    {
        private Level _currentLevel;
        public static ContentManager ContentManager;
        private GameState _nextState;
        public SoundEffectInstance BackgroundMusic;
        /// <summary>
        /// The standart Init procedure
        /// </summary>
        /// <param name="contentManager">Contentmanger to load files</param>
        public void Init(ContentManager contentManager)
        {
            ContentManager = contentManager;
            HUD.Reset();

            _currentLevel = Level.GetFromFile("Content/Levels/Level0.lvl");
            _currentLevel.Init(contentManager,this);

            BackgroundMusic = ContentManager.Load<SoundEffect>("Sounds/GamePlayMusic/BackGround").CreateInstance();
            BackgroundMusic.IsLooped = true;
            BackgroundMusic.Play();

            _nextState = GameState.GamePlay;
        }
        /// <summary>
        /// The standart Update function
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <returns></returns>
        public GameState Update(GameTime gameTime)
        {   
            _currentLevel.Update(gameTime);
            return _nextState;
        }
        /// <summary>
        /// The standart Draw function. Drawing the Level
        /// </summary>
        /// <param name="spriteBatch">SB</param>
        /// <param name="gameTime">GT</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _currentLevel.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Changing current to prm level
        /// </summary>
        /// <param name="level">Instance of Level</param>
        public void ChangeLevel(Level level)
        {
            _currentLevel.Dispose();
            this._currentLevel = level;
            level.Init(ContentManager,this);
        }

        /// <summary>
        /// Return to a MainMenu by a changing states
        /// </summary>
        public void BackToMenu()
        {
            _nextState = GameState.MainMenu;
        }

        /// <summary>
        /// A standart Dispose procedure
        /// </summary>
        public void Dispose()
        {
            _currentLevel.Dispose();
            BackgroundMusic.Stop();
        }
    }
}