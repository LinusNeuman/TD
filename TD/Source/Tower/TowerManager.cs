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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

namespace TD
{
    public class TowerTexture
    {
        public Texture2D myBaseTexture;
        public Texture2D myTurretTexture;
    }

    public class TowerManager : TowerPlacementObserver
    {
        List<Tower> myTowers;
        List<TowerTexture> myTowerTextures;
        Texture2D myRangeTextureForTowers;

        public TowerManager()
        {
            myTowerTextures = new List<TowerTexture>();
            myTowers = new List<Tower>();
        }

        public void Load(ContentManager content)
        {
            myRangeTextureForTowers = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/Range");

            TowerTexture tankyTextures = new TowerTexture();
            tankyTextures.myBaseTexture = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/TankyBase");
            tankyTextures.myTurretTexture = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/TankyTurret");

            TowerTexture speedyTextures = new TowerTexture();
            speedyTextures.myBaseTexture = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/SpeedyBase");
            speedyTextures.myTurretTexture = content.Load<Texture2D>("Screens/PlayScreen/Map/Towers/SpeedyTurret");

            myTowerTextures.Add(tankyTextures);
            myTowerTextures.Add(speedyTextures);
        }

        public void Update(float aDeltaTime)
        {
            for (int i = 0; i < myTowers.Count; ++i)
            {
                myTowers[i].Update(aDeltaTime);
            }

            if (InputManager.GetInstance().GetState() == TouchLocationState.Pressed)
            {
                for (int i = 0; i < myTowers.Count; ++i)
                {
                    if (CollisionManager.CheckRectangleCollision(new RectangleCollider(InputManager.GetInstance().GetPosition(), new Vector2(20, 20)),
                                                                 new RectangleCollider(myTowers[i].myPosition, new Vector2(90, 90))) == true)
                    {
                        myTowers[i].myIsSelected = true;
                    }
                    else
                    {
                        myTowers[i].myIsSelected = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < myTowers.Count; ++i)
            {
                myTowers[i].Draw(spriteBatch);
            }
        }

        public override void Update(TowerSlotData.eTowerType aChosenType, Vector2 aChosenPosition)
        {
            switch (aChosenType)
            {
                case TowerSlotData.eTowerType.eSpeedy:
                    {
                        myTowers.Add(new SpeedyTower(myTowerTextures[1].myBaseTexture,
                                                     myTowerTextures[1].myTurretTexture,
                                                     myRangeTextureForTowers,
                                                     aChosenPosition,
                                                     aChosenPosition));
                    }
                    break;
                case TowerSlotData.eTowerType.eTanky:
                    {
                        myTowers.Add(new TankyTower(myTowerTextures[0].myBaseTexture, 
                                                    myTowerTextures[0].myTurretTexture, 
                                                    myRangeTextureForTowers, 
                                                    aChosenPosition, 
                                                    aChosenPosition));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}