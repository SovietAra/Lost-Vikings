using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Zone where the Platform can move (Changes the direction when intersected)
    /// </summary>
    public class Frame : Object
    {
        public Rectangle Rect;

        public Frame(Vector2 position)
        {
            this.Position = position;
            
            this.Type = ObjectTypes.Frame;

            this.Rect = new Rectangle((int)Position.X, (int)Position.Y, GameConsts.TileSize, GameConsts.TileSize);
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