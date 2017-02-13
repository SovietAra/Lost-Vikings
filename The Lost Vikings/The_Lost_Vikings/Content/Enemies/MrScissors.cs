using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{

    /// <summary>
    /// Uses the same logic as a Snail. (Look at Snail class to find out what every procedure and method does)
    /// The only different, that this class contais - it's immortal and do damage by touching a player.
    /// </summary>
    public class MrScissor : Object
    {
        private CharacterStates _currentState;
        private Animation _runAnimation;
        private AnimationPlayer _animationPlayer;
        public Direction MoveDirection;
        private Vector2 _speed;
        public const int FrameWidth = 31, FrameHeight = 25;
        private Timer _returnTimer;



        public MrScissor(Vector2 position)
        {
            Position = position;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            Type = ObjectTypes.MrScissors;

            Rectangle = new Rectangle((int) Position.X, (int) Position.Y, FrameWidth, FrameHeight);

            _animationPlayer = new AnimationPlayer();
            _runAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Enemies/Scissors"), FrameWidth, 0.08f, true);

            MoveDirection = Direction.Right;
            _animationPlayer.PlayAnimation(_runAnimation);
            _currentState = CharacterStates.Run;
            _returnTimer = new Timer(4f);
        }

        public override void Update(GameTime gameTime)
        {
            Physics();
            EnemyLogic(gameTime);
            Animationhandler();
        }

        private void Animationhandler()
        {
            switch (_currentState)
            {
                case CharacterStates.Run:
                    _animationPlayer.PlayAnimation(_runAnimation);
                    break;
            }
        }

        private void EnemyLogic(GameTime gameTime)
        {
            if (_returnTimer.Update(gameTime))
            {
                ReturnBack();
                _returnTimer.Reset();
            }
        }

        private void ReturnBack()
        {
            if (MoveDirection == Direction.Right)
            {
                MoveDirection = Direction.Left;
            }
            else
            {
                MoveDirection = Direction.Right;
            }
        }

        private void Physics()
        {
            Position += _speed;

            if (_speed.Y < 10)
                _speed.Y += GameConsts.GravitationVelocity;

            if (MoveDirection == Direction.Right)
            {
                _speed.X = GameConsts.MrScissorsSpeed;
            }
            else
            {
                _speed.X = -GameConsts.MrScissorsSpeed;
            }

            Rectangle.X = (int) Position.X;
            Rectangle.Y = (int) Position.Y;
            
            for (int index = 0; index < CurrentLevel.SceneObjects.Count; index++)
            {
                Object obj = CurrentLevel.SceneObjects[index];
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }


        public void Collision(Object obj)
        {
            if (obj.Type == ObjectTypes.Frame)
            {
                if (this.Rectangle.Intersects(((Frame)obj).Rect))
                {
                    this.Position -= _speed * 2;
                    ReturnBack();
                }
            }

            if (Rectangle.Intersects(obj.Rectangle))
            {
                if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric) && HUD.CurrentHealth > 0)
                {
                    HUD.CurrentHealth -= GameConsts.SnailDamage;
                }
                var newRectangle = obj.Rectangle;
                if (Rectangle.TouchTopOf(obj.Rectangle))
                {
                    Rectangle.Y = newRectangle.Y - Rectangle.Height + 1;
                    _speed.Y = 0f;
                }
                if (Rectangle.TouchLeftOf(newRectangle))
                {
                    Position.X = newRectangle.X - Rectangle.Width - 2;
                    ReturnBack();
                }
                if (Rectangle.TouchRightOf(newRectangle))
                {
                    Position.X = newRectangle.X + newRectangle.Width + 2 ;
                    ReturnBack();
                }
            }
        }

 
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (MoveDirection == Direction.Left)
                flip = SpriteEffects.FlipHorizontally;

            _animationPlayer.Draw(gameTime, spriteBatch, new Vector2(Rectangle.X + 12, Rectangle.Y + FrameHeight), flip);
        }

        public override void Dispose()
        {

        }
    }
}