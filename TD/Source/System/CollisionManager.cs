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
    public class CircleCollider
    {
        public CircleCollider()
        {

        }
        public CircleCollider(Vector2 aPosition, float aRadius)
        {
            myPosition = aPosition;
            myRadius = aRadius;
        }

        public void SetPosition(Vector2 aPosition)
        {
            myPosition = aPosition;
        }
        public void SetRadius(float aRadius)
        {
            myRadius = aRadius;
        }

        public Vector2 GetPosition()
        {
            return myPosition;
        }
        public float GetRadius()
        {
            return myRadius;
        }

        private Vector2 myPosition;
        private float myRadius;
    }

    public class RectangleCollider
    {
        public RectangleCollider()
        {

        }
        public RectangleCollider(Vector2 aPosition, Vector2 aSize)
        {
            myPosition = aPosition;
            mySize = aSize;
        }

        public void SetPosition(Vector2 aPosition)
        {
            myPosition = aPosition;
        }
        public void SetSize(Vector2 aSize)
        {
            mySize = aSize;
        }

        public Vector2 GetPosition()
        {
            return myPosition;
        }
        public Vector2 GetSize()
        {
            return mySize;
        }

        private Vector2 myPosition;
        private Vector2 mySize;
    }

    public static class CollisionManager
    {
        public static bool CheckCircleCollision(CircleCollider aLeft, CircleCollider aRight)
        {
            Vector2 distanceVector = aLeft.GetPosition() - aRight.GetPosition();
            float distanceSquareRoot = (float)Math.Sqrt(distanceVector.X * distanceVector.X + distanceVector.Y * distanceVector.Y);

            if(distanceSquareRoot < aLeft.GetRadius() + aRight.GetRadius())
            {
                return true;
            }

            return false;
        }
        public static bool CheckRectangleCollision(RectangleCollider aLeft, RectangleCollider aRight)
        {
            Vector2 aLeftBottomRight = aLeft.GetPosition() + aLeft.GetSize();
            Vector2 aRightBottomRight = aRight.GetPosition() + aRight.GetSize();

            if(aLeftBottomRight.X < aRight.GetPosition().X || aLeft.GetPosition().X > aRightBottomRight.X)
            {
                return false;
            }

            if (aLeftBottomRight.Y < aRight.GetPosition().Y || aLeft.GetPosition().Y > aRightBottomRight.Y)
            {
                return false;
            }

            return true;
        }
    }
}