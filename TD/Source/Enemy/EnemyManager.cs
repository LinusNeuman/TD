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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    public class EnemyManager
    {
        EnemySpawner myEnemySpawner;

        public EnemyManager(List<Node> aMap)
        { 
            myEnemySpawner = new EnemySpawner(aMap);
        }

        public void Load(ContentManager content)
        {
            myEnemySpawner.Load(content);
        }

        public void Update(float aDeltaTime)
        {
            myEnemySpawner.Update(aDeltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            myEnemySpawner.Draw(spriteBatch);
        }

        public void SendWave(int whichWave)
        {
            // Spawn enemies depending on which wave
            myEnemySpawner.SendWave(whichWave);
        }
    }
}