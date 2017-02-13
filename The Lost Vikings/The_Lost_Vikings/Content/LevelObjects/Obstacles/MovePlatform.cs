using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Has the same function as tile with only difference = > it's moving through the level.
    /// Frame describes zone for a movement.
    /// Also those platforms has 2 Move directions - vertical && horizontal
    /// </summary>
    class MovePlatform : Object
    {
        private Vector2 _speed;
        public MovePlatform(Vector2 position, bool isHorizontal)
        {
            this.Type = ObjectTypes.GrassPlatform;
            this.Position = position;

            if (isHorizontal)
            {
                _speed = new Vector2(GameConsts.MovingPlatformSpeed,0);
            }
            else
            {
                _speed = new Vector2(0, GameConsts.MovingPlatformSpeed);
            }
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Texture = contentManager.Load<Texture2D>("Sprites/GamePlay/GrassGaming");
            
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, GameConsts.TileSize, GameConsts.TileSize);
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;

            this.Position += _speed;

            foreach (Object obj in CurrentLevel.SceneObjects)
            {
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }
        /// <summary>
        /// Interaction with player(can stand on it)
        /// Mirroring direction when interact with tile or frame
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric)&& this.Rectangle.Intersects(obj.Rectangle))
            {
                if (_speed.Y > 0)
                {
                    obj.Position.Y += 1f;
                }
                else
                {
                    obj.Position += _speed;
                }
                
            }
            if ((obj.Type == ObjectTypes.GrassTile || obj.Type == ObjectTypes.DirtTile) && this.Rectangle.Intersects(obj.Rectangle))
            {
                _speed = _speed * -1;
                this.Position += _speed * 2;
            }
            if (obj.Type == ObjectTypes.Frame)
            {
                if (this.Rectangle.Intersects(((Frame)obj).Rect))
                {
                    _speed = _speed * -1;
                    this.Position += _speed * 2;
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Dispose()
        {
            
        }
    }
}
