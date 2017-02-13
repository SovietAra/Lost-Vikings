using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Invisible Tile. Has the same function as a simple tile...but it's invisible. That's all :) 
    /// </summary>
    public class Border : Object
    {
        public Rectangle Rect;

        public Border(Vector2 position)
        {
            this.Position = position;

            this.Type = ObjectTypes.Border;

            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, GameConsts.TileSize, GameConsts.TileSize);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void Dispose()
        {

        }
    }
}