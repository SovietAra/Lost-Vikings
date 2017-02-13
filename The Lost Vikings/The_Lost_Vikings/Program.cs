using System;
using Microsoft.Xna.Framework;

namespace The_Lost_Vikings
{
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            using (StateMachine game = new StateMachine())
            {
                game.Run();
            }
        }
    }
#endif
}

