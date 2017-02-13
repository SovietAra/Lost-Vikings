using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings
{
    /// <summary>
    /// This class works together with a Animation class. They are both responsible for a animations.
    /// Using to draw Animation via spritebatch and changing current animation to play
    /// </summary>


    class AnimationPlayer
    {
        Animation animation;
        public Animation Animation
        {
            get { return animation; }
        }

        int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }

        private float timer;

        public Vector2 Origin
        {
            get { return new Vector2(animation.FrameWidth / 2, animation.FrameHeight); }
        }
        /// <summary>
        /// Sets current playing animation
        /// </summary>
        /// <param name="newAnimation">anim to set</param>
        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;

            animation = newAnimation;
            frameIndex = 0;
            timer = 0;
        }
        /// <summary>
        /// Used to draw current frame of animation
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="position"> Position to draw</param>
        /// <param name="spriteEffects">Used to flip current animation</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("There is no Animation selected");

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;

                if (animation.IsLooping)
                    frameIndex = (FrameIndex + 1) % animation.FrameCount;
                else frameIndex = Math.Min(frameIndex + 1, animation.FrameCount - 1);
            }

            Rectangle rectangle = new Rectangle(frameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);
            spriteBatch.Draw(Animation.Texture, position, rectangle, Color.White, 0f, Origin, 1f, spriteEffects, 0f);
        }
    }
}