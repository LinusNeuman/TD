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

namespace TD
{
    public class TowerManager
    {
        List<Tower> myTowers;

        public TowerManager()
        {

        }

        public void Update(float aDeltaTime)
        {
            for (int i = 0; i < myTowers.Count; ++i)
            {
                myTowers[i].Update(aDeltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < myTowers.Count; ++i)
            {
                myTowers[i].Draw(spriteBatch);
            }
        }
    }
}