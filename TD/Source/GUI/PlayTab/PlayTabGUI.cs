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

using static TD.TowerSlotData;

namespace TD
{
    public class PlayTabGUI
    {
        Texture2D myBackground;

        List<TowerSlotGUI> mySlots;

        // We need to be able to check collision between the map and the placement of a new tower.
        List<Node> myMap;

        public PlayTabGUI(List<Node> aMap, TowerSlotData someData)
        {
            myMap = aMap;

            mySlots = new List<TowerSlotGUI>();


            // tanky = myPosition = new Vector2(1724, 237);
             // speedy =   myPosition = new Vector2(1724, 454);

            if (someData.mySelectedTowers.Count > 0)
            {
                for (int i = 0; i < someData.mySelectedTowers.Count; ++i)
                {
                    switch (someData.mySelectedTowers[i])
                    {
                        case eTowerType.eSpeedy:
                            mySlots.Add(new SpeedyTowerGUI(i));
                            mySlots[mySlots.Count - 1].Init(myMap, new Vector2(1724, i * 211 + 237));
                            break;
                        case eTowerType.eTanky:
                            mySlots.Add(new TankyTowerGUI(i));
                            mySlots[mySlots.Count - 1].Init(myMap, new Vector2(1724, i * 211 + 237));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void AttachForwarded(TowerPlacementObserver aObserver)
        {
            for (int i = 0; i < mySlots.Count; ++i)
            {
                mySlots[i].Attach(aObserver);
            }
        }

        public void Load(ContentManager content)
        {
            myBackground = content.Load<Texture2D>("Screens/PlayScreen/GUI/RightTab");

            for (int i = 0; i < mySlots.Count; ++i)
            {
                mySlots[i].Load(content);
            }
        }

        public void Update(float aDeltaTime)
        {
            for (int i = 0; i < mySlots.Count; ++i)
            {
                mySlots[i].Update(aDeltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myBackground, Vector2.Zero, Color.White);

            for (int i = 0; i < mySlots.Count; ++i)
            {
                mySlots[i].Draw(spriteBatch);
            }
        }
    }
}