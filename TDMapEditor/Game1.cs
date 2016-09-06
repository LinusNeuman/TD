using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace TDMapEditor
{

    public class LevelData
    {
        public List<NodeData> Nodes = new List<NodeData>();
    }

    public class NodeData
    {
        public int myPositionX = 0;
        public int myPositionY = 0;
    }

    public class Node
    {
        Texture2D myTexture;
        public Vector2 myPosition;
        public int myNumber;

        public Node(Texture2D aTexture, Vector2 aPosition, int aNumber)
        {
            myTexture = aTexture;
            myPosition = aPosition;
            myNumber = aNumber;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont aSpriteFont)
        {
            spriteBatch.Draw(myTexture, myPosition, Color.White);
            spriteBatch.DrawString(aSpriteFont, myNumber.ToString(), new Vector2(myPosition.X + 64, myPosition.Y + 64), Color.Black);
        }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 myPosition;

        KeyboardState oldState;

        Texture2D NodeGfx;
        Texture2D SelectedNode;

        SpriteFont mySpriteFont;

        List<Node> myNodes = new List<Node>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            NodeGfx = Content.Load<Texture2D>("Tiles/Tile");

            mySpriteFont = Content.Load<SpriteFont>("Fonts/Poplar");

            SelectedNode = Content.Load<Texture2D>("Tiles/Selected");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
            {
                if (myPosition.X < 1920 - 128)
                {
                    myPosition.X += 128;
                }
            }
            if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
            {
                if (myPosition.X >= 0 + 128)
                {
                    myPosition.X -= 128;
                }
            }
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
            {
                if (myPosition.Y >= 0 + 128)
                {
                    myPosition.Y -= 128;
                }
            }
            if (newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
            {
                if (myPosition.Y < 1080 - 128)
                {
                    myPosition.Y += 128;
                }
            }
            if(newState.IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
            {
                myNodes.Add(new Node(NodeGfx, new Vector2(myPosition.X, myPosition.Y), myNodes.Count + 1));
            }

            if (newState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
            {
                // Save to json

                LevelData myLevelData = new LevelData();

                for (int i = 0; i < myNodes.Count; ++i)
                {
                    myLevelData.Nodes.Add(new NodeData());
                    myLevelData.Nodes[i].myPositionX = (int)myNodes[i].myPosition.X;
                    myLevelData.Nodes[i].myPositionY = (int)myNodes[i].myPosition.Y;
                }

                string myPath = "";
                myPath = Path.GetFullPath("../Debug/Levels/");
                myPath += "NewLevel.json";

                string fileName = "";

                if(Utility.SaveFile("Select a location in which to save the level", "NewLevel", ref fileName, Path.GetFullPath("../Debug/Levels/")) == true)
                {
                    Utility.SaveData((object)myLevelData, fileName);
                }
            }
            if (newState.IsKeyDown(Keys.D1) && !oldState.IsKeyDown(Keys.D1))
            {
                // load from json

                string fileName = "";

                if(Utility.LoadFile("Select a level", ref fileName, Path.GetFullPath("../Debug/Levels/"), "JSON (*.json)|*.json") == true)
                {
                    LevelData myLevelData = new LevelData();


                    object newLevelData = myLevelData;
                    Utility.LoadData(ref newLevelData, fileName);
                    myLevelData = (LevelData)newLevelData;

                    myNodes.Clear();

                    for (int i = 0; i < myLevelData.Nodes.Count; ++i)
                    {
                        myNodes.Add(new Node(NodeGfx, new Vector2(myLevelData.Nodes[i].myPositionX, myLevelData.Nodes[i].myPositionY), i));
                    }
                }
            }
            oldState = newState;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Color color = new Color(200,75,75);
            GraphicsDevice.Clear(color);

            spriteBatch.Begin();
            // TODO: Add your drawing code here

            for (int i = 0; i < myNodes.Count; ++i)
            {
                myNodes[i].Draw(spriteBatch, mySpriteFont);
            }

            spriteBatch.Draw(SelectedNode, new Vector2(myPosition.X, myPosition.Y), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
