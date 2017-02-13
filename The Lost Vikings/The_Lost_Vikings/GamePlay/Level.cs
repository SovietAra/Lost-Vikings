using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using The_Lost_Vikings.Content;
using The_Lost_Vikings.Content.Characters.Misc;
using The_Lost_Vikings.Content.Items;

namespace The_Lost_Vikings
{
    /// <summary>
    /// The  list of all Level. This one is used as a container for the whole List of Objects. 
    /// This Class is used for a declaring and handle the Objects on the level
    /// </summary>
    public class Level
    {
        public List<Object> SceneObjects;
        public GamePlay GamePlay;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">All loaded Objects (By loading lvl from a file) </param>
        public Level(List<Object> i)
        {
            SceneObjects = i;
        }
        /// <summary>
        /// Call current Init methods in all Objects of the level
        /// </summary>
        /// <param name="contentManager">Contentmanager for all loaded files</param>
        /// <param name="gamePlay"> current instance of Gameplay </param>
        public void Init(ContentManager contentManager, GamePlay gamePlay)
        {
            GamePlay = gamePlay;
            for (int index = 0; index < SceneObjects.Count; index++)
            {
                Object obj = SceneObjects[index];
                obj.Init(contentManager, this);
            }
        }
        /// <summary>
        /// Call current Update methods in all Objects of the level
        /// </summary>
        /// <param name="gameTime">current instance of Gameplay</param>
        public void Update(GameTime gameTime)
        {
            for (int index = 0; index < SceneObjects.Count; index++)
            {
                Object obj = SceneObjects[index];
                obj.Update(gameTime);
            }
        }
        
        /// <summary>
        /// Call current Draw methods in all Objects of the level.
        /// This methods are used to draw the objects
        /// </summary>
        /// <param name="gameTime">current instance of Gameplay</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int index = 0; index < SceneObjects.Count; index++)
            {
                Object obj = SceneObjects[index];
                obj.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Call current Dispose methods in all Objects of the level.
        /// This methods are used to put actions before destroying the objects
        /// </summary>
        /// <param name="gameTime">current instance of Gameplay</param>
        public void Dispose()
        {
            for (int index = 0; index < SceneObjects.Count; index++)
            {
                Object sceneObject = SceneObjects[index];
                sceneObject.Dispose();
            }
            SceneObjects.Clear();
        }


        /// <summary>
        /// All Levels are loaded from a .lvl files that are placed in a Content/Levels folder (txt, but with another extension).
        /// </summary>
        /// <param name="filename">The name of needed file</param>
        /// <returns></returns>
        public static Level GetFromFile(string filename)
        {
            List<Object> tmpObjects = new List<Object>() {new Background()}; //Background. Is behind every object on the screen. 
            Level tmpLevel = new Level(tmpObjects);

            StreamReader readStream = new StreamReader(File.Open(filename, FileMode.Open));
            List<Object> alive = new List<Object>();
            

            // Reading per line from a file via character and creating an all objects for a current level
            for (int y = 0; y < GameConsts.ScreenHeight / GameConsts.TileSize; y++)
            {
                string e = readStream.ReadLine();
                for (int x = 0; x < GameConsts.ScreenWidth / GameConsts.TileSize; x++)
                {

                    char tmpChar = e[x];

                    /*Those Characters may be used to create a Level
                     * 
                     * tmpObjects = Objects that are placed on the backfront. (Infront of Background) 
                     * Alive = Objects that are placed on the frontline.
                     * 
                     * @ = Grass Tile
                     * # = Dirt Tile
                     * B = Baleog
                     * S = Snail
                     * M = MrScissor
                     * P = Spike
                     * O = Flipped Spike
                     * C = Coin
                     * H = Horizontal Platform
                     * V = Vertical Plaftorm
                     * I = Frame
                     * F = Border
                     * U = Healing Potion
                     * + = Change Location in a Plus direction (Lvl ID) 
                     * - = Change Location in a minus direction (Lvl ID)
                     * D = Change location in a plus plus direction (Lvl ID)
                     * K = Key ( If not already picked up )
                     * G = Door
                     * A = CatSpawners
                     * W = Olaf                   
                     * */

                    switch (tmpChar)
                    {
                        case '@':
                            tmpObjects.Add(new Tile(ObjectTypes.GrassTile, new Vector2 (x*GameConsts.TileSize, y*GameConsts.TileSize)));
                            break;
                        case '#':
                            tmpObjects.Add(new Tile(ObjectTypes.DirtTile, new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'B':
                            alive.Add(new Baleog(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'S':
                            alive.Add(new Snail(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'M':
                            alive.Add(new MrScissor(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'P':
                            tmpObjects.Add(new Spike(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize),false));
                            break;
                        case 'O':
                            tmpObjects.Add(new Spike(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize), true));
                            break;
                        case 'C':
                            alive.Add(new Coin(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'H':
                            tmpObjects.Add(new MovePlatform(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize),true));
                            break;
                        case 'V':
                            tmpObjects.Add(new MovePlatform(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize), false));
                            break;
                        case 'I':
                            tmpObjects.Add(new Frame(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'F':
                            tmpObjects.Add(new Border(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'U':
                            tmpObjects.Add(new HealPotion(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case '+':
                            tmpObjects.Add(new Portal(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize),ObjectTypes.SimplePortal));
                            break;
                        case '-':
                            tmpObjects.Add(new Portal(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize), ObjectTypes.ReversePortal));
                            break;
                        case 'D':
                            tmpObjects.Add(new Portal(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize), ObjectTypes.DoublePortal));
                            break;
                        case 'K':
                            if (HUD.IsKeyPicked == false)
                            {
                                tmpObjects.Add(new Key(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            }
                            break;
                        case 'G':
                            tmpObjects.Add(new Door(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'A':
                            alive.Add(new CatSpawner(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                        case 'W':
                            alive.Add(new Olaf(new Vector2(x * GameConsts.TileSize, y * GameConsts.TileSize)));
                            break;
                    }
                }
            }
            // Displaying a HUD
            alive.Add(new HUD());
            // Adding all Alive infront of tmpObjects
            tmpObjects.AddRange(alive);
            //Close the file
            readStream.Close();
            return tmpLevel;
        }
    }
}
