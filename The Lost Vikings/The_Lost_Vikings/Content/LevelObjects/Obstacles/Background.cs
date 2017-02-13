using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Background. Fin.
    /// </summary>
    public class Background : Object
    {
        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            this.Type = ObjectTypes.Undefined;
            this.Texture = contentManager.Load<Texture2D>("Sprites/GamePlay/Background");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture, Vector2.Zero, Color.White);
        }

        public override void Dispose()
        {
            
        }
    }
}