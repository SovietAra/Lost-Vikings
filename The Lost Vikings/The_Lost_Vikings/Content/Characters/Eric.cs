using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// Works on the same way as Baleog. 
    /// The only difference is that Eric is faster/Can jump higher/Can't Attack.
    /// </summary>
    public class Eric : Object
    {
        private Vector2 _speed, _pastPosition, _healthBarOffset;
        private Animation _idleAnimation, _runAnimation, _jumpAnimation, _deathAnimation;
        private CharacterStates _currentState;
        public bool HasJumped;
        public Direction Dir;
        private Timer _deathAnimationTimer;
        public const int FrameWidth = 25, FrameHeight = 31;
        private AnimationPlayer _animationPlayer;
        private Texture2D _hpTexture;

        private SoundEffect _jump;

        public Eric(Vector2 vector2)
        {
            this.Position = vector2;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            Type = ObjectTypes.Eric;

            Rectangle = new Rectangle((int)(Position.X), (int)Position.Y, FrameWidth, FrameHeight);
            _healthBarOffset = new Vector2(0f, FrameWidth + 3f);

            _animationPlayer = new AnimationPlayer();

            _idleAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Eric/Idle"), 29, 0.5f, true);
            _runAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Eric/Walk"), 32, 0.2f, true);
            _jumpAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Eric/Jump"), 32, 0.5f, true);
            _deathAnimation = new Animation(contentManager.Load<Texture2D>("Animations/Eric/Death"), 32,0.5f,false);

            _jump = contentManager.Load<SoundEffect>("Sounds/Player/Jump");

            _hpTexture = contentManager.Load<Texture2D>("Sprites/Misc/Rect");

            _animationPlayer.PlayAnimation(_idleAnimation);

            InputManager.UpPressed += Jump;
            InputManager.Right += MoveRight;
            InputManager.Left += MoveLeft;
            InputManager.ChangeCharacterPressed += ChangeCharacter;
            InputManager.UpUp += IdleState;
            InputManager.LeftUp += IdleState;
            InputManager.RightUp += IdleState;

            _deathAnimationTimer = new Timer(3f) { IsActive = false };
        }

        private void ChangeCharacter()
        {
            DestroyObject(this);
            CreateObject(new Baleog(this.Position) {HasJumped = true});
        }

        private void Death()
        {
            _currentState = CharacterStates.Dead;
            _deathAnimationTimer.IsActive = true;
            _speed.X = 0;
            HUD.CurrentHealth = 0;
            InputManager.ClearInput();
        }

        private void DeathComplete()
        {
            CreateObject(new Gravestones(this.Position + new Vector2(0, -1), ObjectTypes.EricGravestone),true);
            DestroyObject(this);
            CreateObject(new YouDiedScreen());
        }


        private void IdleState()
        {
            _speed.X = 0f;
            _currentState = CharacterStates.Idle;
        }

        private void MoveLeft()
        {
            _speed.X = -GameConsts.EricSpeed;
            _pastPosition = Position;
            _currentState = CharacterStates.Run;

        }

        private void MoveRight()
        {

            _speed.X = GameConsts.EricSpeed;
            _pastPosition = Position;
            _currentState = CharacterStates.Run;

        }

        private void Jump()
        {
            if (HasJumped == false)
            {
                Position.Y -= GameConsts.EricJumpOffset;
                _speed.Y = GameConsts.EricJumpForce;
                HasJumped = true;
                _jump.Play();
            }
        }

        public override void Update(GameTime gameTime)
        {
            Physics();
            EricLogic(gameTime);
            Animationhandler();
        }

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


        void EricLogic(GameTime gameTime)
        {
            if (HUD.CurrentHealth <= 0)
            {
                Death();
            }
            if (_deathAnimationTimer.Update(gameTime))
            {
                DeathComplete();
            }
        }

        void Animationhandler()
        {
            switch (_currentState)
            {
                case CharacterStates.Idle:
                    _animationPlayer.PlayAnimation(_idleAnimation);
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

            //spriteBatch.Draw(_hpTexture, AttackRectangle, Color.Blue);
            HpDrawer.DrawHealthBar(_hpTexture, spriteBatch, Position + _healthBarOffset, HUD.CurrentHealth, GameConsts.PlayerHealth);
        }

        public override void Dispose()
        {
            InputManager.ClearInput();
        }
    }
}