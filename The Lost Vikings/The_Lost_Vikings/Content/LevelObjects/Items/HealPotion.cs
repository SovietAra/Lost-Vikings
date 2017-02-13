using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Engine;

namespace The_Lost_Vikings.Content
{
    /// <summary>
    /// A item that can be picked up by a player. If the item was already picked up, the object will dissapear
    /// This item will heal a player up. ( As a typical HealPotion )
    /// The Procedures are selfexplanotory
    /// </summary>
    public class HealPotion : Object
    {

        private AnimationPlayer _animationPlayer;
        private Animation _anim;
        private Rectangle _rect;
        private SoundEffect _meat;

        public HealPotion(Vector2 position)
        {
            this.Position = position + new Vector2(15,25);
        }

        public override void Init(ContentManager contentManager, Level currentLevel)
        {
            base.Init(contentManager,currentLevel);

            this._animationPlayer = new AnimationPlayer();

            this.Type = ObjectTypes.HealPotion;

            this._rect = new Rectangle((int)this.Position.X - 10, (int)this.Position.Y - 20, 19, 19);

            this._anim = new Animation(contentManager.Load<Texture2D>("Animations/Items/HealPotion"),19,2f,true);
            _meat = contentManager.Load<SoundEffect>("Sounds/Items/HealthPotion/Meat");
            this._animationPlayer.PlayAnimation(this._anim);

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
        /// <summary>
        /// Interaction with a player
        /// </summary>
        /// <param name="obj"></param>
        private void Collision(Object obj)
        {
            if (this._rect.Intersects(obj.Rectangle) && (obj.Type == ObjectTypes.Baleog || obj.Type == ObjectTypes.Eric))
            {
                _meat.Play();
                HUD.CurrentHealth += GameConsts.HealPotionHpAmount;
                if (HUD.CurrentHealth > 100)
                {
                    HUD.CurrentHealth = 100;
                }
                DestroyObject(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(gameTime,spriteBatch,this.Position,SpriteEffects.None);
        }

        public override void Dispose()
        {
            
        }
    }
}