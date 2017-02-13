using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Tile is game object with collision(can interact with player's and enemies' physics)
    /// And looks like rectangle 32x32 pixels with texture to create level's obstacles
    /// Also all procedures are self explanatory
    /// </summary>
    public class Tile : Object
    {
        
        public Tile(ObjectTypes tile, Vector2 position)
        {
            this.Type = tile;
            this.Position = position;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            switch (Type)
            {
                case ObjectTypes.GrassTile:
                    this.Texture = contentManager.Load<Texture2D>("Sprites/GamePlay/GrassGaming");
                    break;
                case ObjectTypes.DirtTile:
                    this.Texture = contentManager.Load<Texture2D>("Sprites/GamePlay/DirtGaming");
                    break;
            }
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, GameConsts.TileSize, GameConsts.TileSize);

        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Dispose()
        {

        }
    }
}