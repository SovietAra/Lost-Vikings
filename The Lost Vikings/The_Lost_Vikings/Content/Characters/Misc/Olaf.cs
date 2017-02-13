using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content.Characters.Misc
{
    /// <summary>
    /// A simple object of the game.
    /// A player has to touch this, to finish the game.
    /// A win-sequence (Animation) will be showed up after a intersection.
    /// All Procedures and Methods are self-explanotory
    /// </summary>
    class Olaf : Object
    {
        private Animation _animationOlaf, _animationEric, _animationBaleog;
        private AnimationPlayer _olafPlayer, _ericPlayer, _baleogPlayer;
        private Vector2 _ericPosition, _baleogPosition;
        

        private bool _isWin = false;

        private SpriteEffects _baleogEffect, _ericEffect;
        

        public Olaf(Vector2 position)
        {
            this.Position = position;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            this.Type = ObjectTypes.Olaf;

            this.Rectangle = new Rectangle((int)Position.X - GameConsts.TileSize, (int)Position.Y - GameConsts.TileSize, GameConsts.TileSize * 2,GameConsts.TileSize * 2);

            this.Position.Y += GameConsts.TileSize;
            this._baleogPosition = new Vector2(this.Position.X - (int)(GameConsts.TileSize * 1.5), this.Position.Y);
            this._ericPosition = new Vector2(this.Position.X + (int)(GameConsts.TileSize * 1.2), this.Position.Y);

            _animationBaleog = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/PlayingGuitar"), 37, 0.5f, true);
            _animationOlaf = new Animation(contentManager.Load<Texture2D>("Animations/Olaf/PlayingDrums"), 64, 0.5f, true);
            _animationEric = new Animation(contentManager.Load<Texture2D>("Animations/Eric/PlayingGuitar"), 32, 0.5f, true);

            


           

            _olafPlayer = new AnimationPlayer();
            _olafPlayer.PlayAnimation(_animationOlaf);

            
            _ericEffect = SpriteEffects.FlipHorizontally;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isWin == false)
            {
                for (int index = 0; index < CurrentLevel.SceneObjects.Count; index++)
                {
                    Object obj = CurrentLevel.SceneObjects[index];
                    if (!this.Equals(obj))
                    {
                        Collision(obj);
                    }
                }
            }
        }
        /// <summary>
        /// Handles interactions with characters.
        /// If baleog was selected as primary, he'll stand on the left side.
        /// If not on the left side will stand eric(swap positions and spriteeffects for flipping)
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if (this.Rectangle.Intersects(obj.Rectangle) && (obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric))
            {
                
                if (obj.Type == ObjectTypes.Eric)
                {
                    Vector2 tmp = _baleogPosition;
                    _baleogPosition = _ericPosition;
                    _ericPosition = tmp;

                    SpriteEffects tmp2 = _baleogEffect;
                    _baleogEffect = _ericEffect;
                    _ericEffect = tmp2;
                }
                DestroyObject(obj);
                InputManager.ClearInput();
                Win();
            }
        }

        private void Win()
        {
            _isWin = true;
            _baleogPlayer = new AnimationPlayer();
            _ericPlayer = new AnimationPlayer();

            this.CurrentLevel.GamePlay.BackgroundMusic.Stop();
            

            _baleogPlayer.PlayAnimation(_animationBaleog);
            _ericPlayer.PlayAnimation(_animationEric);
            
            CreateObject(new YouWinScreen());
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _olafPlayer.Draw(gameTime,spriteBatch,this.Position,SpriteEffects.None);
            if (_isWin)
            {
                _baleogPlayer.Draw(gameTime,spriteBatch,_baleogPosition,_baleogEffect);
                _ericPlayer.Draw(gameTime,spriteBatch,_ericPosition,_ericEffect);
            }
        }
        
        public override void Dispose()
        {

        }

        
    }
}
