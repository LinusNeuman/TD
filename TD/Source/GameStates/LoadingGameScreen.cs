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
    class LoadingGameScreen : GameState
    {
        ContentManager myContentManager;

        public LoadingGameScreen()
        {
            
        }

       

        public override void Load(ContentManager content)
        {
            myContentManager = content;

            myBackgroundSprite = content.Load<Texture2D>("Screens/LoadingScreen/tempLoadingScreen");
        }

        public override eStackReturnValue Update(float aDeltaTime, ProxyStateStack aStateStack)
        {
            //while (TouchPanel.IsGestureAvailable)
            //{
            //    GestureSample gesture = TouchPanel.ReadGesture();

            //    if (gesture.GestureType == GestureType.Tap)
            //    {
            //        System.Console.WriteLine("TOUCH: VIA while loop");
            //        PlayState newState = new PlayState();
            //        newState.Load(myContentManager);
            //        aStateStack.AddMainState(newState);
            //    }
            //}

            if(InputManager.GetInstance().GetState() == TouchLocationState.Pressed)
            {
                Random rand = new Random();
                TowerSlotData tempData = new TowerSlotData();
                tempData.mySelectedTowers.Add(TowerSlotData.eTowerType.eTanky);
                tempData.mySelectedTowers.Add(TowerSlotData.eTowerType.eSpeedy);
                PlayState newState = new PlayState(rand.Next(1, 7), tempData);
                newState.SetContext(c);
                newState.Load(myContentManager);
                aStateStack.AddMainState(newState);
            }

            return eStackReturnValue.eStay;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myBackgroundSprite, Vector2.Zero, Color.White);
        }

        private Texture2D myBackgroundSprite;
    }
}