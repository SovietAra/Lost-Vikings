using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// This game object is an obstacle on player's way.
    /// If player touch it, he'll die.
    /// Also can be flipped horizontally for placing on the ceiling
    /// Also all procedures are self explanatory
    /// </summary>
    class Spike : Object
    {
        private bool _isFlipped;
        private Rectangle _drawingRectangle;
        public Spike(Vector2 position, bool isFlipped)
        {
            this.Position = position;
            this._isFlipped = isFlipped;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Texture = contentManager.Load<Texture2D>("Sprites/GamePlay/Spike");
            this._drawingRectangle = new Rectangle((int) Position.X, (int) Position.Y, GameConsts.TileSize, GameConsts.TileSize);
            this.Rectangle = new Rectangle((int) Position.X + 5, (int) Position.Y + 3, GameConsts.TileSize - 10, GameConsts.TileSize - 6);
            this.Type = ObjectTypes.Spike;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Object obj in CurrentLevel.SceneObjects)
            {
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }
        /// <summary>
        /// This method handles interaction between player and Spikes
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if (this.Rectangle.Intersects(obj.Rectangle))
            {
                //Defining is this object is player and is he already alive
                if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric) && HUD.CurrentHealth > 0)
                {
                    HUD.CurrentHealth -= GameConsts.SpikesDamage;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture, this._drawingRectangle, null, Color.White, 0, Vector2.Zero, _isFlipped ?SpriteEffects.FlipVertically : SpriteEffects.None, 0);
        }

        public override void Dispose()
        {
            
        }
    }
}
