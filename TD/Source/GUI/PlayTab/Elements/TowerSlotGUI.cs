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

    public abstract class TowerSlotGUI : TowerPlacementSubject
    {
        // We need to be able to check collision between the map and the placement of a new tower.
        protected List<Node> myMap;

        protected int mySlotID;
        protected TowerSlotData.eTowerType myTowerType;

        protected Texture2D myTexture;
        protected Texture2D myRangeTexture;
        protected Texture2D myIcon;

        protected float myRange;

        protected Vector2 myPosition;
        protected Vector2 myOriginalPosition;
        protected Vector2 myRangePosition;

        protected bool myIsDragged;

        protected bool myIsPlacementLegal; // To check if player can drop tower there.
                                           // To show player that they can't place tower.

        protected List<Vector2> myPlacedTowerPositions; // Player can't drop tower at already placed location

        protected abstract void TowerPlacementFinished(Vector2 aChosenPosition);

        public TowerSlotGUI(int aSlotID)
        {
            mySlotID = aSlotID;

            myIsPlacementLegal = true;

            myPlacedTowerPositions = new List<Vector2>();
        }

        public void Init(List<Node> aMap, Vector2 aPosition)
        {
            myMap = aMap;
            myPosition = aPosition;
            myOriginalPosition = myPosition;
        }

        public virtual void Load(ContentManager content)
        {
        }

        public void Update(float aDeltaTime)
        {
            if(InputManager.GetInstance().GetState() == TouchLocationState.Pressed)
            {
                if (myPosition == myOriginalPosition)
                {
                    Vector2 convertedSpace = InputManager.GetInstance().GetPosition();
                    convertedSpace *= ViewportCalculator.Instance.myScale;

                    if (convertedSpace.X >= myPosition.X && convertedSpace.X <= myPosition.X + myIcon.Width) // replace with InputManager get touch position
                    {
                        if (convertedSpace.Y >= myPosition.Y && convertedSpace.Y <= myPosition.Y + myIcon.Height)
                        {
                            myIsDragged = true;
                            myPosition = InputManager.GetInstance().GetPosition();
                            myRangePosition = new Vector2(myPosition.X - ((myRangeTexture.Width * myRange) / 2), myPosition.Y - ((myRangeTexture.Height * myRange) / 2));
                        }
                    }
                }
            }
            else if(InputManager.GetInstance().GetState() == TouchLocationState.Released)
            {
                // Player has chosen this location to place a tower.
                // Check if something is obstructing it, if not place a tower.
                if (myIsDragged == true)
                {
                    TowerPlacementFinished(InputManager.GetInstance().GetPosition());
                    myPlacedTowerPositions.Add(InputManager.GetInstance().GetPosition()); // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                    // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                    // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                    // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                    // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                    // MOVE ALL OF THIS NOT SPECIFIC CODE TO PARENT CLASS PLAYTABGUI.CS
                }

                myIsDragged = false;
                myPosition = myOriginalPosition;
            }
            else if (InputManager.GetInstance().GetState() == TouchLocationState.Moved)
            {
                if (myIsDragged == true)
                {
                    myPosition = InputManager.GetInstance().GetPosition();
                    myRangePosition = new Vector2(myPosition.X - ((myRangeTexture.Width * myRange) / 2), myPosition.Y - ((myRangeTexture.Height * myRange) / 2));

                    // Warning, may be performance intensive!!
                    CalculateTowerPlacement(myPosition);
                    // Warning, may be performance intensive!!
                }
            }
        }

        private void CalculateTowerPlacement(Vector2 aChosenPosition)                                                        
        {
            // Is the map in the way of the tower?
            for (int i = 0; i < myMap.Count; ++i)
            {
                if (CollisionManager.CheckRectangleCollision(new RectangleCollider(myMap[i].myPosition, new Vector2(128, 128)),
                                                            new RectangleCollider(aChosenPosition, new Vector2(90, 90))) == true)
                {
                    myIsPlacementLegal = false;
                    return;
                }
            }

            // Is there already a tower at chosen position?
            for (int i = 0; i < myPlacedTowerPositions.Count; ++i)
            {
                if (CollisionManager.CheckRectangleCollision(new RectangleCollider(myPlacedTowerPositions[i], new Vector2(90, 90)),
                                                            new RectangleCollider(aChosenPosition, new Vector2(90, 90))) == true)
                {
                    myIsPlacementLegal = false;
                    return;
                }
            }

            myIsPlacementLegal = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Should we render with position plus the size (half) of the image? Let's see..
            spriteBatch.Draw(myIcon, myOriginalPosition, Color.White);
            if(myIsDragged == true)
            {
                spriteBatch.Draw(myIcon, new Vector2(myPosition.X - (myIcon.Width/2), myPosition.Y - (myIcon.Height / 2)), Color.White);


                Color color;
                if (myIsPlacementLegal == true)
                {
                    color = Color.White;
                }
                else
                {
                    color = new Color(Color.Red, 0.3f);
                }
                spriteBatch.Draw(myRangeTexture, myRangePosition, null, color, 0, Vector2.Zero, 1.0f * myRange, SpriteEffects.None, 1.0f);
            }
        }
    }
}