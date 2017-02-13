using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings
{
    // Creating a Object = Button. This one is used as a Buttons in Main Menu. 

    class Button
    {
        public Texture2D Texture;
        public Texture2D SelectedTexture;
        public SpriteFont Font;
        public bool IsSelected;
        public Rectangle Rectangle;
        public string Text;
       
        public Button(ContentManager contentManager,Rectangle rectangle, string text)
        {
            Texture = contentManager.Load<Texture2D>("Button");
            SelectedTexture = contentManager.Load<Texture2D>("SelectedButton");
            Font = contentManager.Load<SpriteFont>("Fonts/ButtonFont");
            Text = text;
            Rectangle = rectangle;
            IsSelected = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (IsSelected)
                spriteBatch.Draw(SelectedTexture, this.Rectangle, Color.White);

            else
                spriteBatch.Draw(Texture, this.Rectangle, Color.White);
            int x = Rectangle.X;
            int y = Rectangle.Y;
            spriteBatch.DrawString(Font, Text, new Vector2(x,y) + new Vector2(Texture.Width / 2f, SelectedTexture.Height / 2f) - Font.MeasureString(Text) / 2f, Color.Black);
        }
    }
}