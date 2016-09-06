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
    public class ViewportCalculator
    {
        public Matrix mySpriteScale;
        public Viewport myViewport;
        public Vector2 myScreenSize;
        public Vector2 myVirtualScreenSize;

        public Vector2 myScale;
        public float myAspectRatio;

        private static ViewportCalculator myInstance;

        private ViewportCalculator() { }

        public static ViewportCalculator Instance
        {
            get
            {
                if(myInstance == null)
                {
                    myInstance = new ViewportCalculator();
                }
                return myInstance;
            }
        }

        public void Init(GraphicsDevice aGraphicsDevice)
        {
            myViewport = aGraphicsDevice.Viewport;

            myScreenSize = new Vector2(myViewport.Width, myViewport.Height);
            myVirtualScreenSize = new Vector2(1920, 1080);

            float screenscaleX =
                 (((float)myScreenSize.X / myVirtualScreenSize.X));
            float screenscaleY =
                (((float)myScreenSize.Y / myVirtualScreenSize.Y));
            mySpriteScale = Matrix.CreateScale(screenscaleX, screenscaleY, 1);

            myAspectRatio = aGraphicsDevice.Viewport.AspectRatio;

            UpdateScale();
        }

        public void UpdateScale()
        {
            if (myScreenSize.X == myVirtualScreenSize.X)
            {
                myScale.X = 1;
            }
            if (myScreenSize.Y == myVirtualScreenSize.Y)
            {
                myScale.Y = 1;
            }
            if (myScreenSize.X < myVirtualScreenSize.X)
            {
                myScale.X = myVirtualScreenSize.X / myScreenSize.X;
                myScale.Y = myVirtualScreenSize.Y / myScreenSize.Y;
            }
            if (myScreenSize.Y < myVirtualScreenSize.Y)
            {
                myScale.X = myVirtualScreenSize.X / myScreenSize.X;
                myScale.Y = myVirtualScreenSize.Y / myScreenSize.Y;
            }

            if (myScreenSize.X > myVirtualScreenSize.X)
            {
                myScale.X = myScreenSize.X / myVirtualScreenSize.X;
                myScale.Y = myScreenSize.Y / myVirtualScreenSize.Y;
            }

            if (myScreenSize.Y > myVirtualScreenSize.Y)
            {
                myScale.X = myScreenSize.X / myVirtualScreenSize.X;
                myScale.Y = myScreenSize.Y / myVirtualScreenSize.Y;
            }
        }
    }
}