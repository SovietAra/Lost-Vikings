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
    /// Simple object for a Gravestone
    /// Describes it's logic and spawn ( When the player is dead)
    /// </summary>
    class Gravestones : Object
    {
        private Texture2D _currentTexture;

        public Gravestones(Vector2 position, ObjectTypes gravestoneType)
        {
            this.Position = position;
            this.Type = gravestoneType;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            switch (Type)
            {
                case ObjectTypes.BaleogGravestone:
                    _currentTexture = contentManager.Load<Texture2D>("Animations/Baleog/BaleogGravestone");
                    break;
                case ObjectTypes.EricGravestone:
                    _currentTexture = contentManager.Load<Texture2D>("Animations/Eric/Gravestone");
                    break;
            }

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this._currentTexture,this.Position,Color.White);
        }

        public override void Dispose()
        {
            
        }
    }
}
