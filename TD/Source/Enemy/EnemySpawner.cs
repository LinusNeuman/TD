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

namespace TD
{
    public class EnemySpawner
    {
        List<Enemy> myEnemies;

        Texture2D myEnemyTexture;
        List<Node> myMap;

        bool myIsSpawningEnemies = false;

        int myEnemiesLeftToSpawn = 0;

        float myTimeSinceSpawn = 30;

        public EnemySpawner(List<Node> aMap)
        {
            myEnemies = new List<Enemy>();

            myMap = aMap;
        }

        public void Load(ContentManager content)
        {
            myEnemyTexture = content.Load<Texture2D>("Enemies/Generic");
        }

        public void Update(float aDeltaTime)
        {
            for (int i = 0; i < myEnemies.Count; ++i)
            {
                myEnemies[i].Update(aDeltaTime);
                if(myEnemies[i].myHealth <= 0)
                {
                    // Take away one life!
                    myEnemies.RemoveAt(i);
                }
            }

            // Spawn enemies if neccesarry
            if (myIsSpawningEnemies == false)
            {
                return;
            }

            if(myEnemiesLeftToSpawn > 0 && myTimeSinceSpawn > 30) // 50 frames = around 450ms
            {
                myEnemies.Add(new Enemy(myMap, myEnemyTexture));
                --myEnemiesLeftToSpawn;
                myTimeSinceSpawn = 0;
            }
            else
            {
                myIsSpawningEnemies = false;
            }

            myTimeSinceSpawn += aDeltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < myEnemies.Count; ++i)
            {
                myEnemies[i].Draw(spriteBatch);
            }
        }

        public void SendWave(int whichWave)
        {
            myIsSpawningEnemies = true;

            myEnemiesLeftToSpawn += (5 + ((5 * whichWave) / 2));
        }
    }
}