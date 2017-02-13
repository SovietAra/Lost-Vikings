using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace The_Lost_Vikings.Content.Credits
{
    /// <summary>
    /// Simple class for the showed text on credits state
    /// </summary>
    class TextLine
    {
            public Vector2 Position;
            public string Text;
            
            public TextLine(Vector2 thePosition, string theText)
            {
                Position = thePosition;
                Text = theText;
            }       
    }
}
