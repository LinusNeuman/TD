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
using Microsoft.Xna.Framework;

namespace TD
{ 
    public abstract class Tower
    {
        // Circular Range In Radius (half diameter)
        public float myRange;

        public Vector2 myPosition;
        public Vector2 myTurretPosition; // These two should be the same

        public Texture2D myBaseTexture;
        public Texture2D myTurretTexture;

        public Texture2D myRangeTexture;

        public float myAngleToAimFor;

        public Tower(
            Texture2D aBaseTexture,Texture2D aTurretTexture, Texture2D aRangeTexture,
            Vector2 aPosition, Vector2 aTurretPosition)
        {
            myBaseTexture = aBaseTexture;
            myTurretTexture = aTurretTexture;
            myRangeTexture = aRangeTexture;

            myPosition = aPosition;
            myTurretPosition = aTurretPosition;
        }

        public virtual void Update(float aDeltaTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myRangeTexture, myPosition, Color.White);
            spriteBatch.Draw(myBaseTexture, myPosition, Color.White);
            spriteBatch.Draw(myTurretTexture, myTurretPosition, Color.White);
        }
    }
}