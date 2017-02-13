using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Lost_Vikings.Content
{
    public class Door : Object
    {
        /// <summary>
        /// Has a simple function like a real door.
        /// When the door is closed, the player can't move through it ( Rectangle isn't null )
        /// To open the door, a bool "KeyIsUp" should be set on true. This happens when key is picked up.
        /// When the Door is opened, rectangle will set to empty, so the player can pass through.
        /// </summary>
        private Texture2D _texture2D;
        private Rectangle _drawingRectangle;
        private SoundEffect _doorOpen;

        public Door(Vector2 position)
        {
            this.Position = position;
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager, currentLevel);

            this.Type = ObjectTypes.Door;

            this._drawingRectangle = new Rectangle((int)Position.X,(int) Position.Y,18,64);

            this.Rectangle = _drawingRectangle;

            _texture2D = contentManager.Load<Texture2D>("Sprites/GamePlay/DoorClosed");
            _doorOpen = contentManager.Load<SoundEffect>("Sounds/Items/Door/DoorOpen");
        }

        public override void Update(GameTime gameTime)
        {
            for (int index = 0; index < CurrentLevel.SceneObjects.Count; index++)
            {
                Object obj = CurrentLevel.SceneObjects[index];
                if (!this.Equals(obj))
                {
                    Collision(obj);
                }
            }
        }

        private void Collision(Object obj)
        {
            if ((obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric) && HUD.IsKeyPicked && this.Rectangle.Intersects(obj.Rectangle))
            {
                Open();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture2D, _drawingRectangle,Color.White);
        }

        public void Open()
        {
            _texture2D = _contentManager.Load<Texture2D>("Sprites/GamePlay/DoorOpened");
            _drawingRectangle.Width = 32;
            _doorOpen.Play();
            Rectangle = Rectangle.Empty;
        }

        public override void Dispose()
        {
            
        }
    }
}