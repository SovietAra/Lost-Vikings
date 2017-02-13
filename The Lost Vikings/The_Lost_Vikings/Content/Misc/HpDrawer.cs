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
    /// Draws the HP bar of Alive Objects ( like Enemies and Players.)
    /// Also the HP are grouped by different colors. This depends on current HP of a Target.
    /// </summary>
    static class HpDrawer
    { 
        public static Color UltraLow = Color.Red,Low = Color.Orange,Medium = Color.Yellow, High = Color.GreenYellow, Full = Color.Green;

        public static void DrawHealthBar(Texture2D rectangle,SpriteBatch spriteBatch, Vector2 position, int currenthp, int allhp)
        {
            float hpPercents = (float)currenthp / allhp;
            Rectangle hpRectangle = new Rectangle((int) position.X,(int) position.Y,25,6);
            Color currentColor = Full;
            if (hpPercents >= 0 && hpPercents <= 0.2f)
            {
                currentColor = UltraLow;
            }
            if (hpPercents > 0.2f && hpPercents <= 0.4f)
            {
                currentColor = Low;
            }
            if (hpPercents > 0.4f && hpPercents <= 0.6f)
            {
                currentColor = Medium;
            }
            if (hpPercents > 0.6f && hpPercents <= 0.8f)
            {
                currentColor = High;
            }
            spriteBatch.Draw(rectangle, hpRectangle,currentColor);
        }

    }
}
