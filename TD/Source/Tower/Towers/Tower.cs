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
using Microsoft.Xna.Framework.Input.Touch;

namespace TD
{ 
    public abstract class Tower
    {
        // Circular Range In Radius (half diameter)
        public float myRange;

        public Vector2 myRangePosition;
        public Vector2 myPosition;
        public Vector2 myPositionCentered;
        public Vector2 myTurretPosition; // These two should be the same

        public Texture2D myBaseTexture;
        public Texture2D myTurretTexture;

        public Texture2D myRangeTexture;

        public float myAngleToAimFor;

        public float myShootCooldown;

        public bool myIsSelected;

        public Tower(
            Texture2D aBaseTexture,Texture2D aTurretTexture, Texture2D aRangeTexture,
            Vector2 aPosition, Vector2 aTurretPosition)
        {
            myBaseTexture = aBaseTexture;
            myTurretTexture = aTurretTexture;
            myRangeTexture = aRangeTexture;

            myPosition = aPosition;

            myPositionCentered.X = aPosition.X - myBaseTexture.Width / 2;
            myPositionCentered.Y = aPosition.Y - myBaseTexture.Height / 2;

            UpdateRange();

            myTurretPosition = aTurretPosition;
        }

        public void UpdateRange()
        {
            myRangePosition = new Vector2(myPosition.X - ((myRangeTexture.Width * myRange) / 2), myPosition.Y - ((myRangeTexture.Height * myRange) / 2));
        }

        public virtual void Update(float aDeltaTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(myIsSelected == true)
            {
                spriteBatch.Draw(myRangeTexture, myRangePosition, null, Color.White, 0, Vector2.Zero, 1.0f * myRange, SpriteEffects.None, 1.0f);
            }
            spriteBatch.Draw(myBaseTexture, myPositionCentered, Color.White);
            spriteBatch.Draw(myTurretTexture, myPositionCentered, Color.White); // render with a pivot being in center of the base texture (position + basetexture.width / 2)
        }
    }
}