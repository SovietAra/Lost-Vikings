using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Invisible game object is used for change level when player interacts with it.
    /// It has three variations: Single(move to a higher level), Double(Jump to +2 ID), Reverse(return to previous level)
    /// </summary>
    public class Portal : Object
    {
        private Rectangle _rect;
        public Portal(Vector2 position, ObjectTypes portalType)
        {
            this.Position = position;

            this.Type = portalType;

            this._rect = new Rectangle((int)Position.X, (int)Position.Y, GameConsts.TileSize, GameConsts.TileSize);
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
        /// Handling interaction
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if ((obj.Type == ObjectTypes.Eric || obj.Type == ObjectTypes.Baleog) && this._rect.Intersects(obj.Rectangle))
            {
                //Handling action binded to portal type
                switch (Type)
                {

                    case ObjectTypes.SimplePortal:
                        HUD.ChangeLevel(1,this.CurrentLevel);
                        break;
                    case ObjectTypes.ReversePortal:
                        HUD.ChangeLevel(-1, this.CurrentLevel);
                        break;
                    case ObjectTypes.DoublePortal:
                        HUD.ChangeLevel(2, this.CurrentLevel);
                        break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public override void Dispose()
        {

        }
    }
}