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

namespace TD
{
    class StateStack
    {
        public StateStack()
        {
            myStates = new List<List<GameState>>();
            myProxy = new ProxyStateStack(this);
        }

        public void Init(SpriteBatch aSpriteBatchToCache)
        {
            mySpriteBatchCached = aSpriteBatchToCache;
        }

        public StateStack(StateStack aStateStack)
        {
            myStates = aStateStack.myStates;
            myProxy = new ProxyStateStack(this);
        }

        public void AddMainState(GameState aMainState)
        {
            List<GameState> tempMainState = new List<GameState>();
            myStates.Add(tempMainState);
            AddSubState(aMainState);
        }

        public void AddSubState(GameState aSubState)
        {
            myStates.Last().Add(aSubState);
        }

        public bool Update(float aDeltaTime)
        {
            switch(myStates.Last().Last().Update(aDeltaTime, myProxy))
            {
                case eStackReturnValue.eDeleteMainState:
                    {
                        PopAndDeleteMainState();
                        return myStates.Count() > 0;
                    }
                case eStackReturnValue.eDeleteCurrentSubstates:
                    {
                        PopAndDeleteCurrentSubstates();
                        return true;
                    }
                case eStackReturnValue.eDeleteSubstate:
                    {
                        return true;
                    }
                case eStackReturnValue.eStay:
                    {
                        return true;
                    }
            }
            return false;
        }

        public void Draw()
        {
            DrawState(myStates.Last(), myStates.Last().Count() - 1);
        }

        private void PopAndDeleteSubstate()
        {
            myStates.Last().RemoveAt(myStates.Last().Count() - 1);
        }

        private void PopAndDeleteCurrentSubstates()
        {
            for (int i = myStates.Last().Count - 1; i > 0; --i)
            {
                PopAndDeleteSubstate();
            }
        }

        private void PopAndDeleteMainState()
        {
            PopAndDeleteCurrentSubstates();
            myStates.Last().RemoveAt(0);
            myStates.RemoveAt(myStates.Count() - 1);
        }

        private void PopMainState()
        {
            myStates.RemoveAt(myStates.Count() - 1);
        }

        private void PopSubState()
        {
            myStates.Last().RemoveAt(myStates.Last().Count() - 1);
        }

        private void PopCurrentSubstates()
        {
            for (int i = myStates.Last().Count() - 1; i > 0; --i)
            {
                PopSubState();
            }
        }

        private void DrawState(List<GameState> aMainState, int anIndexToCheck)
        {
            if(aMainState[anIndexToCheck].GetShouldLetThroughRendering() == true && anIndexToCheck > 0)
            {
                DrawState(aMainState, anIndexToCheck - 1);
            }
            aMainState[anIndexToCheck].Draw(mySpriteBatchCached);
        }

        List<List<GameState>> myStates;
        ProxyStateStack myProxy;
        SpriteBatch mySpriteBatchCached;
    }
}