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
using Microsoft.Xna.Framework.Input.Touch;

namespace TD
{

    public class TowerSlotData
    {
        public enum eTowerType
        {
            eSpeedy,
            eTanky
        };

        public List<eTowerType> mySelectedTowers = new List<eTowerType>(4);
    }

    public abstract class TowerSlotGUI
    {
        // We need to be able to check collision between the map and the placement of a new tower.
        protected List<Node> myMap;

        protected int mySlotID;

        protected Texture2D myTexture;
        protected Texture2D myIcon;

        protected Vector2 myPosition;
        protected Vector2 myOriginalPosition;

        protected bool myIsDragged;


        public TowerSlotGUI(int aSlotID)
        {
            mySlotID = aSlotID;
        }

        public void Init(List<Node> aMap)
        {
            myMap = aMap;
        }

        public virtual void Load(ContentManager content)
        {

        }

        public void Update(float aDeltaTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if(tl.State == TouchLocationState.Pressed)
                {
                    if (myPosition == myOriginalPosition)
                    {
                        Vector2 convertedSpace = tl.Position;
                        convertedSpace *= ViewportCalculator.Instance.myScale;

                        if (convertedSpace.X >= myPosition.X && convertedSpace.X <= myPosition.X + myIcon.Width)
                        {
                            if (convertedSpace.Y >= myPosition.Y && convertedSpace.Y <= myPosition.Y + myIcon.Height)
                            {
                                myIsDragged = true;
                            }
                        }
                    }
                }
                else if(tl.State == TouchLocationState.Released)
                {
                    // Player has chosen this location to place a tower.
                    // Check if something is obstructing it, if not place a tower.
                    if(myIsDragged == true)
                    {

                    }

                    myIsDragged = false;
                    myPosition = myOriginalPosition;
                }
                else if(tl.State == TouchLocationState.Moved)
                {
                    if(myIsDragged == true)
                    {
                        myPosition = tl.Position;
                    }
                }
            }

            // Player is currently choosing a location to place a tower.
            if(myIsDragged == true)
            {
                // In some way, represent graphically how the player can place the tower.
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Should we render with position plus the size (half) of the image? Let's see..
            spriteBatch.Draw(myIcon, myOriginalPosition, Color.White);
            if(myIsDragged == true)
            {
                spriteBatch.Draw(myIcon, new Vector2(myPosition.X - (myIcon.Width/2), myPosition.Y - (myIcon.Height / 2)), Color.White);
            }
        }
    }
}