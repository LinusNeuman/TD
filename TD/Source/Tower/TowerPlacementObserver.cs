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

namespace TD
{
    public abstract class TowerPlacementObserver
    {
        public abstract void Update(TowerSlotData.eTowerType aChosenType, Vector2 aChosenPosition);

        protected TowerPlacementObserver() { }
    }
}