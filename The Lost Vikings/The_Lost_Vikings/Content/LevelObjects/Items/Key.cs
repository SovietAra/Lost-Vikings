using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content.Items
{
    /// <summary>
    /// A item that can be picked up by a player. If the item was already picked up, the object will dissapear and won't be showed up again (bool).
    /// Also, by a HUD reset the bool will set up to a false, so the key will appear again.
    /// The Key is needed to open the door
    /// </summary>
    class Key : Object
    {
        private AnimationPlayer _animationPlayer;
        private Animation _rotatingAnim;
        private Rectangle _rect;
        private SoundEffect _key;

        public Key(Vector2 position)
        {
            this.Position = position + new Vector2(16, 24);
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Type = ObjectTypes.Key;
            this._rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, 14, 14);
            _animationPlayer = new AnimationPlayer();
            
            _rotatingAnim = new Animation(contentManager.Load<Texture2D>("Animations/Items/Key"), 14, 0.9f, true);
            _key = contentManager.Load<SoundEffect>("Sounds/Items/Key/Key");
            _animationPlayer.PlayAnimation(_rotatingAnim);
        }

        public override void Update(GameTime gameTime)
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
        /// <summary>
        /// Interraction with a player
        /// </summary>
        /// <param name="obj"></param>
        void Collision(Object obj)
        {
            if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric) && this._rect.Intersects(obj.Rectangle) )
            {
                HUD.IsKeyPicked = true;
                _key.Play();
                DestroyObject(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(gameTime, spriteBatch, Position, SpriteEffects.None);
          
        }

        public override void Dispose()
        {

        }
    }
}
