using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content.Characters.Misc
{
    /// <summary>
    /// Invisible object that will spawn cat-guide when player interact with spawner
    /// </summary>
    public class CatSpawner : Object
    {
        private Rectangle _rect;
        public CatSpawner(Vector2 position)
        {
            this.Position = position;

            this.Type = ObjectTypes.Undefined;

            this._rect = new Rectangle((int) Position.X - GameConsts.TileSize,(int) Position.Y - GameConsts.TileSize, GameConsts.TileSize * 3, GameConsts.TileSize * 3);
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
        /// Handles interactions with characters(spawn cat and self-destroy)
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if (this._rect.Intersects(obj.Rectangle) && (obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric))
            {
                CreateObject(new Cat(this.Position),true);
                
                DestroyObject(this);
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