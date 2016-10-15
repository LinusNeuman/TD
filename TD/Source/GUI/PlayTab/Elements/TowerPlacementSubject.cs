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
    public class TowerPlacementSubject
    {
        public void Attach(TowerPlacementObserver aObserver)
        {
            myObservers.Add(aObserver);
        }
        public void Detach(TowerPlacementObserver aObserver)
        {
            myObservers.Remove(aObserver);
        }
        public void Notify(TowerSlotData.eTowerType aChosenTowerType, Vector2 aChosenPosition)
        {
            for (int i = 0; i < myObservers.Count; ++i)
            {
                myObservers[i].Update(aChosenTowerType, aChosenPosition);
            }
        }

        protected TowerPlacementSubject()
        {
            myObservers = new List<TowerPlacementObserver>();
        }

        private List<TowerPlacementObserver> myObservers;
    }
}