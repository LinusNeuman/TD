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
using Microsoft.Xna.Framework;

namespace TD
{
    public class TankyTowerGUI : TowerSlotGUI
    {
        public TankyTowerGUI(int aSlotID) : base(aSlotID)
        {
            myPosition = new Vector2(1724, 237);
            myOriginalPosition = myPosition;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            myTexture = content.Load<Texture2D>("Screens/PlayScreen/GUI/Towers/Tanky");
            myIcon = content.Load<Texture2D>("Screens/PlayScreen/GUI/Towers/TankyIcon");
        }
    }
}