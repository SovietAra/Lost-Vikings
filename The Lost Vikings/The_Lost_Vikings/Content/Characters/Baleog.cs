using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    public class Baleog : Object
    {   
        /// <summary>
        /// Works on the same way as Snail. Almost all methods and procedures are described in Snail.
        /// The difference are
        /// 1) InputManager
        /// 2) A melee attack
        /// </summary>
        private Vector2 _speed, _pastPosition, _healthBarOffset;
        private Animation _idleAnimation, _runAnimation, _attackAnimation, _jumpAnimation, _deathAnimation;
        public Rectangle AttackRectangle;
        private AnimationPlayer _animationPlayer;
        private Timer _attackCooldown, _attackDuration, _deathAnimationTimer;
        private Texture2D _hpDrawer;
        private CharacterStates _currentState;
        public Direction Dir;
        public const int FrameWidth = 25, FrameHeight = 31;
        private bool _isAttackCooldown;
        public bool HasJumped;
        private int _attackRectangleOffset;

        private SoundEffect _swordHit, _jump;

        public Baleog(Vector2 vector2)
        {
            this.Position = vector2;
        }
       
        
        /// Base initializing of Baleog (Play Character)
        /// <param name="contentManager">WWWWW</param>
        /// <param name="currentLevel"></param>
        public override void Init(ContentManager contentManager, Level currentLevel)
        { 
            base.Init(contentManager, currentLevel);
            Type = ObjectTypes.Baleog;

            Rectangle = new Rectangle((int)(Position.X), (int)Position.Y, FrameWidth, FrameHeight);
            AttackRectangle = new Rectangle(Rectangle.X, Rectangle.Y + 20, FrameWidth / 4, FrameHeight / 4);
            _healthBarOffset = new Vector2(0f, FrameWidth + 3f);

            // Init the Animation
            _animationPlayer = new AnimationPlayer();

            _idleAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/Idle"), 29, 0.5f, true);
            _runAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/Walk"), 32, 0.2f, true);
            _attackAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/Attack"), 35, 0.2f, true);
            _jumpAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/Jump"), 32, 0.5f, true);
            _deathAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Baleog/Death"), 32, 0.3f, false);

            _hpDrawer = contentManager.Load<Texture2D>("Sprites/Misc/Rect");
            
            _animationPlayer.PlayAnimation(_idleAnimation);

            // Init the Controls
            InputManager.UpPressed += Jump;
            InputManager.AttackPressed += Attack;
            InputManager.Right += MoveRight;
            InputManager.Left += MoveLeft;
            InputManager.ChangeCharacterPressed += ChangeCharacter;
            InputManager.UpUp += IdleState;
            InputManager.LeftUp += IdleState;
            InputManager.RightUp += IdleState;

            _swordHit = contentManager.Load<SoundEffect>("Sounds/Player/BaleogHit");
            _jump = contentManager.Load<SoundEffect>("Sounds/Player/Jump");

            _attackDuration = new Timer(0.8f) {IsActive = false};
            _attackCooldown = new Timer(1f) {IsActive = false};
            _deathAnimationTimer = new Timer(3f) {IsActive = false};
        }

        private void ChangeCharacter()
        {
            DestroyObject(this);
            CreateObject(new Eric(this.Position) {HasJumped = true});
        }

        /// <summary>
        /// Simple atack method. If a player has already attacked, he has to wait until the animation is done completly ( cooldown )
        /// </summary>
        private void Attack()
        {
            if (_isAttackCooldown == false && (_currentState != CharacterStates.InAir || _currentState != CharacterStates.Attack))
            {
               _speed.X = 0;
               _currentState = CharacterStates.Attack;
               if (Dir == Direction.Left)
               {
                   _attackRectangleOffset -= 20;
               }
               if (Dir == Direction.Right)
               {
                   _attackRectangleOffset += 20;
               }
                _isAttackCooldown = true;
                _attackDuration.IsActive = true;
                _attackCooldown.IsActive = true;
                _swordHit.CreateInstance().Play();
            }
        }
       

        /// <summary>
        /// If a HP of a character is zero, the player will lose the control of his character and a death animation will be played on the screen
        /// Also the timer for the DeathAnimation will set to a active
        /// </summary>
        private void Death()
        { 
            _currentState = CharacterStates.Dead;
            _deathAnimationTimer.IsActive = true;
            _speed.X = 0;
            HUD.CurrentHealth = 0;
            InputManager.ClearInput();
        }
        /// <summary>
        /// When the DeathTimer is over, gravestone will be spawned and the Game Over screen object will be created 
        /// </summary>
        private void DeathComplete()
        {
            CreateObject(new Gravestones(this.Position + new Vector2(0,-1), ObjectTypes.BaleogGravestone),true);
            DestroyObject(this);
            CreateObject(new YouDiedScreen());
           
        }


        /// All states of a player + animations

        private void IdleState()
        {
            if (_currentState != CharacterStates.Dead)
            {
                _speed.X = 0f;
                _currentState = CharacterStates.Idle;
            }
            
        }

        private void MoveLeft()
        {
            if (_currentState != CharacterStates.Attack)
            {
                _speed.X = -GameConsts.BaleogSpeed;
                _pastPosition = Position;
                _currentState = CharacterStates.Run;
            }
        }

        private void MoveRight()
        {
            if (_currentState != CharacterStates.Attack)
            {
                _speed.X = GameConsts.BaleogSpeed;
                _pastPosition = Position;
                _currentState = CharacterStates.Run;
            }
        }

        private void Jump()
        {
            if (HasJumped == false)
            {
                Position.Y -= GameConsts.BaleogJumpOffset;
                _speed.Y = GameConsts.BaleogJumpForce;
                HasJumped = true;
                _jump.Play();

            }
        }

        public override void Update(GameTime gameTime)
        {
            Physics();
            BaleogLogic(gameTime);
            Animationhandler();
        }

        /// <summary>
        /// Handles player's physics 
        /// </summary>
        void Physics()
        {
            Position += _speed;

            if (_speed.Y < 10)
                _speed.Y += GameConsts.GravitationVelocity;

            Dir = CheckDirection();

            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;


            foreach (Object obj in CurrentLevel.SceneObjects)
            {
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }

       /// <summary>
       /// The whole logic of baleog
       /// </summary>
       /// <param name="gameTime"></param>
        void BaleogLogic(GameTime gameTime)
        {

            if (_attackCooldown.Update(gameTime))
            {
                EndOfCoolDown();
            }
            if (_attackDuration.Update(gameTime))
            {
                EndOfAttackDuration();
            }

            AttackRectangle.X = Rectangle.X + FrameWidth / 2 + _attackRectangleOffset;
            AttackRectangle.Y = Rectangle.Y + FrameHeight / 2;
            _attackRectangleOffset = 0;
            if (HUD.CurrentHealth <= 0)
            {
                Death();
            }
            if (_deathAnimationTimer.Update(gameTime))
            {
                DeathComplete();
            }
        }

        /// <summary>
        /// Animations
        /// </summary>
        void Animationhandler()
        {
            switch (_currentState)
            {
                case CharacterStates.Idle:
                    _animationPlayer.PlayAnimation(_idleAnimation);
                    break;
                case CharacterStates.Attack:
                    _animationPlayer.PlayAnimation(_attackAnimation);
                    break;
                case CharacterStates.Dead:
                    _animationPlayer.PlayAnimation(_deathAnimation);
                    break;
                case CharacterStates.InAir:
                    _animationPlayer.PlayAnimation(_jumpAnimation);
                    break;
                case CharacterStates.Run:
                    _animationPlayer.PlayAnimation(_runAnimation);
                    break;
            }
        }

        // Timers

        void EndOfCoolDown()
        {
            _isAttackCooldown = false;
            _attackCooldown.IsActive = false;
            _attackCooldown.Reset();
        }

        void EndOfAttackDuration()
        {
            _currentState = CharacterStates.Idle;
            
            _attackDuration.IsActive = false;
            _attackDuration.Reset();
        }

        /// <summary>
        /// Interaction with the Objects on the Level
        /// </summary>
        /// <param name="obj"></param>
        public void Collision(Object obj)
        {
            if (Rectangle.Intersects(obj.Rectangle))
            {
                var newRectangle = obj.Rectangle;
                if (Rectangle.TouchBottomOf(newRectangle))
                {
                    _speed.Y = 1f;
                }
                if (Rectangle.TouchTopOf(obj.Rectangle))
                { 
                    Rectangle.Y = newRectangle.Y - Rectangle.Height + 1;
                    _speed.Y = 0f;
                    HasJumped = false;
                }
                if (Rectangle.TouchLeftOf(newRectangle))
                {
                    Position.X = newRectangle.X - Rectangle.Width - 1;
                }
                if (Rectangle.TouchRightOf(newRectangle))
                {
                    Position.X = newRectangle.X + newRectangle.Width + 1;
                }

            }
        }

        private Direction CheckDirection()
        {
            if (_pastPosition.X < Position.X)
            {
                return Direction.Right;
            }
            if (_pastPosition.X > Position.X || _speed.X < 0)
            {
                return Direction.Left;
            }
            return this.Dir;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteEffects flip = SpriteEffects.None;
            
            if (Dir == Direction.Left)
                flip = SpriteEffects.FlipHorizontally;

            _animationPlayer.Draw(gameTime, spriteBatch, new Vector2(Rectangle.X + 12, Rectangle.Y + FrameHeight), flip);
            Content.HpDrawer.DrawHealthBar(_hpDrawer,spriteBatch,Position + _healthBarOffset,HUD.CurrentHealth, GameConsts.PlayerHealth);
        }

        public override void Dispose()
        {
            InputManager.ClearInput();
        }
    }
}