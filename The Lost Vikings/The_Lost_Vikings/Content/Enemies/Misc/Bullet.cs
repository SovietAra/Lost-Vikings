using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// The class that creates a bullet. Bullet will be always shooted to a player position.X 
    /// The bullet works on the same way as a Enemy. 
    /// All Procedures and Methods are self explanatory.
    /// </summary>
    class Bullet : Object
    {
        private Direction _dir;
        private Texture2D _texture2D;
        private Rectangle _bulletRect;
        private Vector2 _speed;
        private const float BulletSpeed = 2f;
        public Bullet(Vector2 startPosition, Direction dir)
        {

            this._dir = dir;
            this.Position = startPosition;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Type = ObjectTypes.Bullet;

            this.Position.Y += 12f;
            this._bulletRect= new Rectangle((int)this.Position.X, (int)this.Position.Y, 11,9);
            _texture2D = contentManager.Load<Texture2D>("Sprites/Misc/bullet");


            if (_dir == Direction.Left)
            {
                this._speed.X = BulletSpeed;
            }
            else
            {
                this._speed.X = -BulletSpeed;
            }

        }

        public override void Update(GameTime gameTime)
        {
            Physics();
        }

        private void Physics()
        {
            Position += _speed;

            this._bulletRect.X = (int) Position.X;
            this._bulletRect.Y = (int) Position.Y;

            for (int index = 0; index < CurrentLevel.SceneObjects.Count; index++)
            {
                Object obj = CurrentLevel.SceneObjects[index];
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }
        private void Collision(Object obj)
        {
            if (obj.Type != ObjectTypes.Snail && this._bulletRect.Intersects(obj.Rectangle))
            {
                if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric) && HUD.CurrentHealth > 0)
                {
                    HUD.CurrentHealth -= GameConsts.SnailDamage;
                }
                DestroyObject(this);       
            }            
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (this._dir == Direction.Right)
            {
                flip = SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(this._texture2D, _bulletRect, null, Color.White, 0, Vector2.Zero, flip, 0);
        }

        public override void Dispose()
        {
            
        }
    }
}
