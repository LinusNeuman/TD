using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;

namespace TD
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, myPosition, Color.White);
        }
    }

    public class LevelPath
    {
        List<Node> myNodes = new List<Node>();

        Texture2D PathTexture;

        public List<Node> GetPath()
        {
            return myNodes;
        }

        public void Load(ContentManager content, int aLevelID, Context c)
        {
            PathTexture = content.Load<Texture2D>("Tiles/Tile");
            {
                LevelData myLevelData = new LevelData();


                object newLevelData = myLevelData;
                Utility.LoadLevel(ref newLevelData, aLevelID, c);
                myLevelData = (LevelData)newLevelData;

                for (int i = 0; i < myLevelData.Nodes.Count; ++i)
                {
                    myNodes.Add(new Node(PathTexture, new Vector2(myLevelData.Nodes[i].myPositionX, myLevelData.Nodes[i].myPositionY), i));
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < myNodes.Count; ++i)
            {
                myNodes[i].Draw(spriteBatch);
            }
        }
    }
}