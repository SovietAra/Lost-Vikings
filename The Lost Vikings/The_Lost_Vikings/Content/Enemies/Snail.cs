using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Snail - An enemy that moves through the level ( Zone is limited by a Frames and timer). 
    /// Also this enemy can die ( it has a HealthBar) and shoot bullets -> Damage to a player
    /// 
    /// </summary>
    public class Snail : Object
    {
        private CharacterStates _currentState;
        public int CurrentHealth;
        private Animation _runAnimation;
        private AnimationPlayer _animationPlayer;
        public Direction Dir, MoveDirection;
        private Vector2 _speed, _pastPosition, _healthBarOffset;
        public const int FrameWidth = 32, FrameHeight = 25;
        private Texture2D _hpTexture;
        private Timer _returnTimer;
        private bool _hasJumped;


        public Snail(Vector2 position)
        {
            Position = position;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            Type = ObjectTypes.Snail;

            CurrentHealth = GameConsts.SnailHealth;

            Rectangle = new Rectangle((int) Position.X,(int) Position.Y,32,25);

            _animationPlayer = new AnimationPlayer();
            _runAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Enemies/SnailWalk"), 32, 0.5f, true);

            _healthBarOffset = new Vector2(0f,FrameHeight + 3f);

            _hpTexture = contentManager.Load<Texture2D>("Sprites/Misc/Rect");

            MoveDirection = Direction.Right;
            _animationPlayer.PlayAnimation(_runAnimation);
            _currentState = CharacterStates.Idle;
            _returnTimer = new Timer(4f);
            Attack();
        }

        public override void Update(GameTime gameTime)
        {
            Physics();
            EnemyLogic(gameTime);
            Animationhandler();
        }
        /// <summary>
        /// Handles an animations via snail's state machine
        /// </summary>
        private void Animationhandler()
        {
            switch (_currentState)
            {
                case CharacterStates.Idle:
                    _animationPlayer.PlayAnimation(_runAnimation);
                    break;
                case CharacterStates.Attack:
                    break;
                case CharacterStates.Run:
                    _animationPlayer.PlayAnimation(_runAnimation);
                    break;
            }
        }
        /// <summary>
        /// Random jumps, random shoots, returning by timer -- all of this is snail logic
        /// </summary>
        /// <param name="gameTime"></param>
        private void EnemyLogic(GameTime gameTime)
        {
            if (_returnTimer.Update(gameTime))
            {
                ReturnBack();
                _returnTimer.Reset();
            }
            if (HUD.Randomizer.Next(1,1000) == 1)
            {
                Jump();
            }
            if (HUD.Randomizer.Next(1, 300) == 1)
            {
                Attack();
            }
        }
       /// <summary>
       /// A snail is able to jump.
       /// </summary>
        private void Jump()
        {
            if (_hasJumped == false)
            {
                Position.Y -= 5f;
                _speed.Y = -2.5f;
                _hasJumped = true;
            }
        }
       /// <summary>
       /// Shooting a bullet
       /// </summary>
        private void Attack()
        {
            CreateObject(new Bullet(Position, MoveDirection == Direction.Right?Direction.Left : Direction.Right));
        }

        private void ReturnBack()
        {
            MoveDirection = MoveDirection == Direction.Right ? Direction.Left : Direction.Right;
        }

        /// <summary>
        /// Handling physics - fall, interactions with tiles and other obstacles
        /// </summary>
        private void Physics()
        {
            Position += _speed;

            if (_speed.Y < 10)
                _speed.Y += GameConsts.GravitationVelocity;

            if (MoveDirection == Direction.Right)
            {
                _speed.X = GameConsts.SnailSpeed;
            }
            else
            {
                _speed.X = -GameConsts.SnailSpeed;
            }

            Dir = CheckDirection(_pastPosition);

            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;

            _pastPosition = Position;

            for (int index = 0; index < CurrentLevel.SceneObjects.Count; index++)
            {
                Object obj = CurrentLevel.SceneObjects[index];
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }
        /// <summary>
        /// Check direction to flip sprite if need it
        /// </summary>
        /// <param name="otherPosition">Position to compare</param>
        /// <returns></returns>
        private Direction CheckDirection(Vector2 otherPosition)
        {
            if (otherPosition.X < Position.X)
            {
                return Direction.Right;
            }
            if (otherPosition.X > Position.X || _speed.X < 0)
            {
                return Direction.Left;
            }
            return this.Dir;
        }
        /// <summary>
        /// Interaction with obstacles like tiles players etc.
        /// </summary>
        /// <param name="obj"></param>
        public void Collision(Object obj)
        {
            //If a snail is hitted by a player, it will receive a damage to his HealthPoints and will be pushed back.
            if (obj.Type == ObjectTypes.Baleog)
            {
                Baleog tmp = (Baleog) obj;
                if (Rectangle.Intersects(tmp.AttackRectangle))
                {
                    Position.Y -= 10;
                    if (CheckDirection(tmp.Position) == Direction.Left)
                    {
                        Position.X -= 20;
                        
                    }
                    else
                    {
                        Position.X += 20;
                    }
                    CurrentHealth -= GameConsts.BaleogDamage;
                    if (CurrentHealth <=0)
                    {
                        DestroyObject(this);
                    }
                }
            }
            
            else
            {
                //Interaction with frames and other obstacles
                if (obj.Type == ObjectTypes.Frame)
                {
                    if (this.Rectangle.Intersects(((Frame)obj).Rect))
                    {
                        this.Position -= _speed * 2;
                        ReturnBack();
                    }
                }
                if (Rectangle.Intersects(obj.Rectangle)&& obj.Type != ObjectTypes.Snail && obj.Type != ObjectTypes.Eric && obj.Type != ObjectTypes.Baleog)
                {
                    var newRectangle = obj.Rectangle;
                    if (Rectangle.TouchTopOf(obj.Rectangle))
                    {
                        Rectangle.Y = newRectangle.Y - Rectangle.Height + 1;
                        _speed.Y = 0f;
                        _hasJumped = false;
                    }
                    if (Rectangle.TouchLeftOf(newRectangle))
                    {
                        Position.X = newRectangle.X - Rectangle.Width - 1;
                        ReturnBack();
                        
                    }
                    if (Rectangle.TouchRightOf(newRectangle))
                    {
                        Position.X = newRectangle.X + newRectangle.Width + 2;
                        ReturnBack();
                    }
                }
            }
            
        }
        /// <summary>
        /// Draw snail and it's healthbar
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (Dir == Direction.Left)
                flip = SpriteEffects.FlipHorizontally;

            _animationPlayer.Draw(gameTime, spriteBatch, new Vector2(Rectangle.X + 12, Rectangle.Y + FrameHeight), flip);
            HpDrawer.DrawHealthBar(_hpTexture,spriteBatch,Position + _healthBarOffset,CurrentHealth,GameConsts.SnailHealth);
        }

        public override void Dispose()
        {

        }
    }
}