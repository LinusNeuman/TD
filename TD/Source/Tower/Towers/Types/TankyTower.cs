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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    public class TankyTower : Tower
    {
        public TankyTower(Texture2D aBaseTexture, Texture2D aTurretTexture, Texture2D aRangeTexture, Vector2 aPosition, Vector2 aTurretPosition) : base(aBaseTexture, aTurretTexture, aRangeTexture, aPosition, aTurretPosition)
        {
            myRange = 1.2f;
            UpdateRange();
        }
    }
}