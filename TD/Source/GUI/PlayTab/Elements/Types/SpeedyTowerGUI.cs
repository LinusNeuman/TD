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
    public class SpeedyTowerGUI : TowerSlotGUI
    {
        public SpeedyTowerGUI(int aSlotID) : base(aSlotID)
        {
            myRange = 2.0f;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            myRangeTexture = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/Range");
            myTexture = content.Load<Texture2D>("Screens/PlayScreen/GUI/Towers/Speedy");
            myIcon = content.Load<Texture2D>("Screens/PlayScreen/GUI/Towers/SpeedyIcon");
        }

        protected override void TowerPlacementFinished(Vector2 aChosenPosition)
        {
            if (myIsPlacementLegal == true)
            {
                Notify(TowerSlotData.eTowerType.eSpeedy, aChosenPosition);
            }


            // Lastly reset this
            myIsPlacementLegal = true;
        }
    }
}