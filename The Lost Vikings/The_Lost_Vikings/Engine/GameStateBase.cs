using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings
{
    /// <summary>
    /// This interface contains signatures of methods in all Game states to call they are in state machine
    /// </summary>
    interface IGameState
    {
        //Interface Class for my States
        void Init(ContentManager contentManager);
        
        GameState Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Dispose();
    }
}
