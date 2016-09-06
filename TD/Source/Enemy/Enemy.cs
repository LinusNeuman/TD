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
    public class Enemy
    {
        private Texture2D myTexture;

        private Vector2 myWaypoint;
        private Vector2 myPosition;
        private int myCurrentWaypoint;
        private int myMaxNumberOfWaypoints;

        public int myHealth = 100;

        private float myWalkSpeed = 0.1f;

        List<Node> myMap;

        private bool myIsFadingOut;

        private Color myColor = Color.White;

        public Enemy(List<Node> aMap, Texture2D aTexture)
        {
            myTexture = aTexture;

            if(aMap[0].myPosition.X == 0)
            {
                myPosition.X = -64;
                myPosition.Y = aMap[0].myPosition.Y + 64;
            }
            else if(aMap[0].myPosition.Y == 0)
            {
                myPosition.Y = -64;
                myPosition.X = aMap[0].myPosition.X + 64;
            }
            if (aMap[0].myPosition.X == 1792)
            {
                myPosition.X = 1792 + 64;
                myPosition.Y = aMap[0].myPosition.Y + 64;
            }
            else if (aMap[0].myPosition.Y == 1024)
            {
                myPosition.Y = 1024+64;
                myPosition.X = aMap[0].myPosition.X + 64;
            }

            myWaypoint = new Vector2(aMap[1].myPosition.X + 64, aMap[1].myPosition.Y + 64);
            myCurrentWaypoint = 1;
            myMaxNumberOfWaypoints = aMap.Count;
            myMap = aMap;
        }

        private void FadeDown()
        {
            if(myColor.A >= 5)
            {
                myColor.A -= 5;
                myColor.R -= 5;
                myColor.G -= 5;
                myColor.B -= 5;
            }
            else
            {
                myHealth = 0;
                // Take one life away from player
            }
        }

        public void Update(float aDeltaTime)
        {
            if(myHealth <= 0)
            {
                return;
            }

            if(myIsFadingOut == true)
            {
                FadeDown();
            }

            float distanceX = (myPosition.X) - (myWaypoint.X);
            float distanceY = (myPosition.Y) - (myWaypoint.Y);

            if(distanceY >= -1.0f && distanceY <= 1.0f)
            {
                if (myPosition.X < myWaypoint.X)
                {
                    myPosition.X += myWalkSpeed * aDeltaTime;
                }
                else if (myPosition.X > myWaypoint.X)
                {
                    myPosition.X -= myWalkSpeed * aDeltaTime;
                }
            }
            else
            {
                if (myPosition.Y < myWaypoint.Y)
                {
                    myPosition.Y += myWalkSpeed * aDeltaTime;
                }
                else if (myPosition.Y > myWaypoint.Y)
                {
                    myPosition.Y -= myWalkSpeed * aDeltaTime;
                }
            }
           

            Console.WriteLine("My Distance X: " + distanceX + " Y: " + distanceY);

            if (distanceX >= -1.0f && distanceX <= 1.0f && distanceY >= -1.0f && distanceY <= 1.0f)
            {
                ++myCurrentWaypoint;
                if(myCurrentWaypoint >= myMaxNumberOfWaypoints)
                {
                    // Start fading out..
                    myIsFadingOut = true;
                }
                else
                {
                    myWaypoint = new Vector2(myMap[myCurrentWaypoint].myPosition.X + 64, myMap[myCurrentWaypoint].myPosition.Y + 64);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, new Vector2(myPosition.X - (myTexture.Width/2), myPosition.Y - (myTexture.Height/2)), myColor);
        }
    }
}