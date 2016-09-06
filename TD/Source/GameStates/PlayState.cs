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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class PlayState : GameState
    {
        Level myLevel;

        PlayTabGUI myPlayTabGUI;

        EnemyManager myEnemyManager;

        int myCurrentWave = 1;
        int myMaxWaves;

        float myCurrentWaveTime = 0;

        // When level select is done, level select is supposed to make a new playstate with the level id
        // Level select is supposed to pass on the selected towers for the level
        public PlayState(int aLevelID, TowerSlotData someData)
        {
            myLevel = new Level(aLevelID);

            myPlayTabGUI = new PlayTabGUI(myLevel.GetLevelPath().GetPath(), someData);

            myEnemyManager = new EnemyManager(myLevel.GetLevelPath().GetPath());

            myMaxWaves = 10;
        }

        public override void Load(ContentManager content)
        {
            myLevel.SetContext(c);
            myLevel.Load(content);
            myPlayTabGUI.Load(content);

            myEnemyManager.Load(content);

            myEnemyManager.SendWave(myCurrentWave);
        }

        public override eStackReturnValue Update(float aDeltaTime, ProxyStateStack aStateStack)
        {
            myLevel.Update(aDeltaTime);
            myPlayTabGUI.Update(aDeltaTime);

            myEnemyManager.Update(aDeltaTime);

            myCurrentWaveTime += aDeltaTime;
            if(myCurrentWaveTime >= 2000) // This is not elapsed time but delta time, so... Should change all deltatimes to gameTime instead so that I can use elapsed time // 2000 frames is around 32 seconds
            {
                if(myCurrentWave >= myMaxWaves)
                {
                    // Done with this level!
                }
                else
                {
                    myCurrentWaveTime = 0;
                    myCurrentWave += 1;
                    myEnemyManager.SendWave(myCurrentWave);
                }
            }

            return eStackReturnValue.eStay;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            myLevel.Draw(spriteBatch);
            myPlayTabGUI.Draw(spriteBatch);

            myEnemyManager.Draw(spriteBatch);
        }

    }
}