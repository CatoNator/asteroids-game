using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    public class CAsteroid : CGameObject
    {
        public float m_fRotation;

        public int m_iImage;

        private float m_fRotationSpeed;

        public override void InstanceSpawn()
        {
            this.m_fDirection = myRandom.Next(360);

            this.m_fVelocity = 1;

            this.m_fRotation = 0;

            this.m_fRotationSpeed = myRandom.Next(10)-5;

            this.m_sInstanceType = "objAsteroid";

            m_iCollisionRadius = 32;

            this.m_iSprInd = 9;

            /*
             * 9 = bigg steroid
             * 10 = med aste
             * 11 = smål roid
            */

            this.m_iImage = (int)myRandom.NextDouble();
        }

        public override void OnDestruction()
        {
            if (!CObjectManager.Instance.m_bGameOver)
            {
                float powerUpChance = myRandom.Next(20);

                if (powerUpChance <= 1)
                    CObjectManager.Instance.CreateInstance("objPowerUp", x, y);

                float randDir = myRandom.Next(360);

                if (m_iSprInd == 9) //big
                {
                    CObjectManager.Instance.CreateInstance("objAsteroid", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_iSprInd = 10;
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir;
                    //m_fDirection

                    CObjectManager.Instance.CreateInstance("objAsteroid", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_iSprInd = 10;
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 180;

                    CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_EXPLOSION2);

                    CObjectManager.Instance.m_iScore += 100;
                }
                else if (m_iSprInd == 10) //med
                {
                    CObjectManager.Instance.CreateInstance("objExplosion", x, y);

                    CObjectManager.Instance.CreateInstance("objAsteroid", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_iSprInd = 11;
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir;

                    CObjectManager.Instance.CreateInstance("objAsteroid", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_iSprInd = 11;
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 120;

                    CObjectManager.Instance.CreateInstance("objAsteroid", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_iSprInd = 11;
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 240;

                    CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_EXPLOSION2);

                    CObjectManager.Instance.m_iScore += 500;
                }
                else
                {
                    CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_EXPLOSION1);

                    CObjectManager.Instance.CreateInstance("objExplosion", x, y);

                    CObjectManager.Instance.CreateInstance("objShard", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir;

                    CObjectManager.Instance.CreateInstance("objShard", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 72;

                    CObjectManager.Instance.CreateInstance("objShard", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 144;

                    CObjectManager.Instance.CreateInstance("objShard", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 216;

                    CObjectManager.Instance.CreateInstance("objShard", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = randDir + 288;

                    CObjectManager.Instance.m_iScore += 1000;
                }
            }
        }

        public override void Update()
        {
            m_fRotation += m_fRotationSpeed;

            //capping the rotation
            if (m_fRotation >= 360)
                m_fRotation -= 360;

            if (m_fRotation < 0)
                m_fRotation += 360;

            //the sizes
            if (m_iSprInd == 9) //big
                m_iCollisionRadius = 32;
            else if (m_iSprInd == 10) //med
                m_iCollisionRadius = 16;
            else if (m_iSprInd == 11) //smol
                m_iCollisionRadius = 8;

            //movement
            m_fHorSpeed = (float)(distDirX(m_fVelocity, degToRad(m_fDirection)));
            m_fVerSpeed = (float)(distDirY(m_fVelocity, degToRad(m_fDirection)));

            x += m_fHorSpeed;
            y += m_fVerSpeed;

            if (!CObjectManager.Instance.m_bGameOver)
            {
                //wrapping the object
                if (x > 832)
                    x = -32;
                else if (x < -32)
                    x = 832;

                if (y > 512)
                    y = -32;
                else if (y < -32)
                    y = 512;
            }
            else if ((x > 848) || (x < 0) || (y > 480) || (y < 0))
                CObjectManager.Instance.DestroyInstance(m_iIndex);
        }

        public override void Render()
        {
            m_spSprite.Update(x, y, m_fRotation, m_iSprInd, 0, 0.0f);
            m_spSprite.Render();
        }
    }
}
