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

namespace TD
{

    enum eStackReturnValue
    {
        eStay,
        eDeleteMainState,
        eDeleteSubstate,
        eDeleteCurrentSubstates,
    };

    abstract class GameState
    {
        public Context c;

        public GameState()
        {
            myLetThroughRender = false;
        }

        public void SetContext(Context aC)
        {
            c = aC;
        }

        public virtual void Init()
        {

        }

        public virtual void Load(ContentManager content)
        {

        }

        public abstract eStackReturnValue Update(float aDeltaTime, ProxyStateStack aStateStack);

        public abstract void Draw(SpriteBatch spriteBatch);

        public bool GetShouldLetThroughRendering()
        {
            return myLetThroughRender;
        }

        protected bool myLetThroughRender;
    }
}