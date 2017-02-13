using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Timer = The_Lost_Vikings.Engine.Timer;

namespace The_Lost_Vikings.Content.Characters.Misc
{
    /// <summary>
    /// Cat-guide
    /// </summary>
    class Cat : Object
    {
        private Rectangle _drawingRectangle;

        private SoundEffect _catSound;
        private SpriteFont _font;
        //Massive of all showed text bubbles
        private static readonly string[] TutStrings = new string[7]
        {
            "Use Q (Key)\nOr Y (GP) to\nswap",
            "Use J/X to\nAttack (Baleog)",
            "Heal...Meat!\n grab it!",
            "You can also\navoid enemies",
            "Try to avoid\n scissor guys",
            "You need a Key \n to pass further",
            "Oh! THere is \n Hurry up!"
        };
        //globalcounter for indexing current text bubble(++ when spawner spawns cat)
        private static int _globalCounter;

        //local counter for indexing text(every cat has it's own index)
        private int _localCounter;

        private Timer _lifeTimer;
        private Vector2 _fontPosition;

        public Cat(Vector2 position)
        {
            this.Position = position;

        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Type = ObjectTypes.Undefined;

            this._fontPosition = Position;
            this._fontPosition.Y -= GameConsts.TileSize;
            this._fontPosition.X -= GameConsts.TileSize - 3;


            this._catSound = contentManager.Load<SoundEffect>("Sounds/Misc/Meow");
            this.Texture = contentManager.Load<Texture2D>("Sprites/Obstacles/Cat");
            this._drawingRectangle = new Rectangle((int)Position.X - GameConsts.TileSize, (int)Position.Y - GameConsts.TileSize, GameConsts.TileSize * 3, GameConsts.TileSize * 3);
            this._localCounter = _globalCounter;
            this._font = contentManager.Load<SpriteFont>("Fonts/CatTuT");

            _globalCounter++;
            _catSound.Play();

            _lifeTimer = new Timer(GameConsts.CatShowTime) {IsActive = true};
        }

        public override void Update(GameTime gameTime)
        {
            if (_lifeTimer.Update(gameTime))
            {
                DestroyObject(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture,this._drawingRectangle,Color.White);
            spriteBatch.DrawString(this._font,TutStrings[_localCounter], this._fontPosition,Color.Black);
        }
        /// <summary>
        /// The Cat Counter will be also reseted with a HUD
        /// </summary>
        public static void Reset()
        {
            _globalCounter = 0;
        }

        public override void Dispose()
        {
            
        }
    }
}
