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
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace TD
{
    public class InputManager
    {
        public static InputManager GetInstance()
        {
            if(myInstance == null)
            {
                myInstance = new InputManager();
            }

            return myInstance;
        }

        public static void CreateInstance()
        {
            myInstance = new InputManager();
        }

        public void Update()
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                myTouchLocationState = tl.State;
                myPosition = tl.Position;
            }
        }

        public Vector2 GetPosition()
        {
            return myPosition;
        }

        public TouchLocationState GetState()
        {
            return myTouchLocationState;
        }

        private TouchLocationState myTouchLocationState;
        private Vector2 myPosition;

        private static InputManager myInstance;
        private InputManager() { }
    }
}