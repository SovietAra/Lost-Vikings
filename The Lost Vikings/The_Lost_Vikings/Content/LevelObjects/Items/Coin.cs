using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// A item that can be picked up by a player. If the item was already picked up, the object will dissapear
    /// This item will increase a player score by 1. ( As a typical Coin )
    /// </summary>
    public class Coin:Object
    {
        private AnimationPlayer _animationPlayer;
        private Animation _rotatingAnim;
        private Rectangle _rect;

        private SoundEffect _currentSound;
        
        public Coin(Vector2 position)
        {
            this.Position = position + new Vector2(16,24);
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Type = ObjectTypes.Coin;
            this._rect = new Rectangle((int)this.Position.X,(int)this.Position.Y,16,16);
            _animationPlayer = new AnimationPlayer();
            _rotatingAnim = new Animation(contentManager.Load<Texture2D>("Animations/Items/Coin"),16,0.1f,true);

            _currentSound = contentManager.Load<SoundEffect>("Sounds/Items/Coins/coin1");
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
        /// Interaction with a player
        /// </summary>
        /// <param name="obj"></param>
        void Collision(Object obj)
        {
            if (this._rect.Intersects(obj.Rectangle) &&(obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric))
            {
                HUD.Coins++;
                DestroyObject(this);
                _currentSound.Play();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(gameTime,spriteBatch,Position,SpriteEffects.None);
        }

        public override void Dispose()
        {

        }
    }
}
