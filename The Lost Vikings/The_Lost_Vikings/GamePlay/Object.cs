using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Content;

namespace The_Lost_Vikings
{
    /// <summary>
    /// Main abstract class of Gameplay (almost all active/passive object in Gameplay)
    /// All object are structed under different parameters - like Rectangle ( Using for a GamePhysics), ObjectType,Texture & etc.
    /// </summary>
    public abstract class Object
    {
        public ObjectTypes Type;
        public Rectangle Rectangle;
        public Vector2 Position;
        public Texture2D Texture;
        protected ContentManager _contentManager;
        public Level CurrentLevel;
        public bool IsPaused;
        public virtual void Init(ContentManager contentManager, Level currentLevel)
        {
            CurrentLevel = currentLevel;
            _contentManager = contentManager;
        }
        
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);


        public abstract void Dispose();
        
        /// <summary>
        /// Removing the Object from a current Levellist of Objects
        /// </summary>
        /// <param name="obj"> Object for remove </param>
        public void DestroyObject(Object obj)
        {
            if (obj != null)
            {
                CurrentLevel.SceneObjects.Remove(obj);
                obj.Dispose();
            }

        }
        /// <summary>
        /// Creating an Object and placing it to a Level
        /// </summary>
        /// <param name="obj"> Object to insert to a Levelist</param>
        /// <param name="inBegin"> Order for a creation of objects. True = begin of List, False = end of list</param>
        public void CreateObject(Object obj, bool inBegin = false)
        {
            if (obj != null)
            {
                if (inBegin)
                {
                    //Inserting the objects infront of Background but behind other objects
                    CurrentLevel.SceneObjects.Insert(1,obj);
                }
                else
                {
                    CurrentLevel.SceneObjects.Add(obj);
                }
                
                obj.Init(_contentManager, CurrentLevel);
            }
        }
    }
}