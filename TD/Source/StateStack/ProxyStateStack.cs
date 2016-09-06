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

namespace TD
{
    class ProxyStateStack
    {
        public ProxyStateStack(StateStack aStateStack)
        {
            myStateStack = aStateStack;
        }

        public void AddMainState(GameState aMainState)
        {
            myStateStack.AddMainState(aMainState);
        }

        public void AddSubState(GameState aSubState)
        {
            myStateStack.AddSubState(aSubState);
        }

        private StateStack myStateStack;
    }
}