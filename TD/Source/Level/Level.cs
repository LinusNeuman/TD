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

namespace TD
{
    public class Level
    {
        Texture2D myBackground;

        LevelPath myLevelPath;

        int myLevelID;

        Context c;

        public Level(int aLevelID)
        {
            myLevelID = aLevelID;

            myLevelPath = new LevelPath();
        }

        public LevelPath GetLevelPath()
        {
            return myLevelPath;
        }

        public void SetContext(Context aC)
        {
            c = aC;
        }

        public void Load(ContentManager content)
        {
            myBackground = content.Load<Texture2D>("Screens/PlayScreen/Background");

            myLevelPath.Load(content, myLevelID, c);
        }

        public void Update(float aDeltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myBackground, Vector2.Zero, Color.White);

            myLevelPath.Draw(spriteBatch);
        }
    }
}