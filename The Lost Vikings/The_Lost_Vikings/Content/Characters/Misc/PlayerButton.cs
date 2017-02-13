using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    class PlayerButton : Object
    {
        /// <summary>
        /// Buttons the YouDiedScreen. Works on the same way as MenuBUttons
        /// </summary>
        public bool IsSelected;

        private SpriteFont _font;
        private string _text;
        public PlayerButton(Vector2 position, string text)
        {
            this.Position = position;
            _text = text;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);
            this._font = contentManager.Load<SpriteFont>("Fonts/HUD");
            this.Type = ObjectTypes.Undefined;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(this._font,this._text,this.Position, IsSelected ? Color.DarkOrange :Color.Black);
        }


        public override void Update(GameTime gameTime)
        {

        }


        public override void Dispose()
        {
            
        }
    }
}
