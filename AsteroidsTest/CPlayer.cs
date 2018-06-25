using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsTest
{
    public class CPlayer : CGameObject
    {
        public float m_iImageIndex;

        public float m_iSprite;

        //private KeyboardState oldState;

        private int m_iSmokeCreationTick;

        private int m_iReload;

        private int m_iReloadMax;

        private bool m_bInvincible;

        private bool m_bRenderSprite = true;

        private int m_iInvincibilityTimer;

        private bool m_bDead;

        private int m_iRespawnTimer;

        private bool m_bMultiShot;

        private bool m_bRapidFire;

        private int m_iMultiShotTimer;

        private int m_iRapidFireTimer;
        
        public CPlayer()
        {

        }

        public override void InstanceSpawn()
        {
            this.m_fDirection = 0;

            this.m_fVelocity = 0;

            this.m_iImageIndex = 0;

            this.m_iSprite = 2;

            this.m_sInstanceType = "objPlayer";

            this.m_iSmokeCreationTick = 5;

            this.m_iCollisionRadius = 16;

            m_iReload = 0;

            m_iReloadMax = 5;

            m_bMultiShot = true;

            m_bRapidFire = false;

            m_iMultiShotTimer = 0;

            m_iRapidFireTimer = 0;

            m_bInvincible = false;

            m_iInvincibilityTimer = 0;

            m_bDead = false;

            m_iRespawnTimer = 0;

            /*
             * 0 = nothing
             * 1 = multishot
             * 2 = rapidfire
             * laser???
            */
        }

        public override void Update()
        {
            if (!m_bDead && CObjectManager.Instance.m_bHasStarted)
            {
                //input
                KeyboardState keyboardState = Keyboard.GetState();

                //turning
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    m_fDirection += 2;

                    if (m_iSprite > 0)
                        m_iSprite -= 0.25F;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    m_fDirection -= 2;

                    if (m_iSprite < 4)
                        m_iSprite += 0.25F;
                }
                else
                {
                    if (m_iSprite < 2)
                        m_iSprite += 0.25F;
                    else if (m_iSprite > 2)
                        m_iSprite -= 0.25F;
                }

                //movement forwards
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    if (m_iImageIndex < 3)
                        m_iImageIndex += 0.5F;
                    else
                        m_iImageIndex = 0;

                    m_fVelocity = 0.25F;

                    m_iSmokeCreationTick--;

                    if (m_iSmokeCreationTick <= 0)
                    {
                        CObjectManager.Instance.CreateInstance("objSmoke", x + (float)distDirX(10, degToRad(m_fDirection + 180)), y + (float)distDirY(10, degToRad(m_fDirection + 180)));
                        m_iSmokeCreationTick = 3;
                    }
                }
                else
                {
                    m_iImageIndex = 0;
                    m_fVelocity = 0;
                }

                //firing boolets
                if (keyboardState.IsKeyDown(Keys.LeftControl) && m_iReload <= 0)
                {
                    CObjectManager.Instance.CreateInstance("objBullet", x, y);
                    CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects-1].m_fDirection = this.m_fDirection;

                    if (m_bMultiShot)
                    {
                        CObjectManager.Instance.CreateInstance("objBullet", x, y);
                        CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = this.m_fDirection - 22.5f;

                        CObjectManager.Instance.CreateInstance("objBullet", x, y);
                        CObjectManager.Instance.m_pGameObjectList[CObjectManager.Instance.m_iGameObjects - 1].m_fDirection = this.m_fDirection + 22.5f;
                    }

                    if (m_bRapidFire)
                        CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_RAPIDSHOT);
                    else
                        CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_BLASTERSHOT);

                    m_iReload = m_iReloadMax;
                }
            }

            if (m_bRapidFire)
                m_iReloadMax = 5;
            else
                m_iReloadMax = 15;

            if (m_iReload > 0)
                m_iReload--;

            //capping the rotation
            if (m_fDirection >= 360)
                m_fDirection -= 360;

            if (m_fDirection < 0)
                m_fDirection += 360;

            //the sprite wobble thing
            /*if (m_iSprite < 4.5F)
                m_iSprite += 0.25F;
            else
                m_iSprite = 0;*/
            
            //acceleration
            m_fHorSpeed += (float)(distDirX(m_fVelocity, degToRad(m_fDirection)));
            m_fVerSpeed += (float)(distDirY(m_fVelocity, degToRad(m_fDirection)));

            //capping the speed
            float m_fMaxSpeed = 5;

            if (m_fHorSpeed > m_fMaxSpeed)
                m_fHorSpeed = m_fMaxSpeed;
            else if (m_fHorSpeed < -m_fMaxSpeed)
                m_fHorSpeed = -m_fMaxSpeed;

            if (m_fVerSpeed > m_fMaxSpeed)
                m_fVerSpeed = m_fMaxSpeed;
            else if (m_fVerSpeed < -m_fMaxSpeed)
                m_fVerSpeed = -m_fMaxSpeed;

            //deacceleration
            float deAccSpeed = 0.025f;

            if (m_fHorSpeed < deAccSpeed && m_fHorSpeed > -deAccSpeed)
                m_fHorSpeed = 0;
            else if (m_fHorSpeed > 0)
                m_fHorSpeed -= deAccSpeed;
            else if (m_fHorSpeed < 0)
                m_fHorSpeed += deAccSpeed;

            if (m_fVerSpeed < deAccSpeed && m_fVerSpeed > deAccSpeed)
                m_fVerSpeed = 0;
            else if (m_fVerSpeed > 0)
                m_fVerSpeed -= deAccSpeed;
            else if (m_fVerSpeed < 0)
                m_fVerSpeed += deAccSpeed;

            //moving it loel
            x += m_fHorSpeed;
            y += m_fVerSpeed;

            //wrapping the object
            if (x > 832)
                x = -32;
            else if (x < -32)
                x = 832;

            if (y > 512)
                y = -32;
            else if (y < -32)
                y = 512;

            //collisions
            if (!m_bInvincible && !m_bDead && CObjectManager.Instance.m_bHasStarted)
            {
                CGameObject colTest = CollisionCircle("objAsteroid");

                if (colTest != null)
                {
                    m_bDead = true;
                    m_iRespawnTimer = 120;

                    CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_PLAYERDEATH);

                    CObjectManager.Instance.CreateInstance("objExplosion", x - 16 + myRandom.Next(32), y - 16 + myRandom.Next(32));
                    CObjectManager.Instance.CreateInstance("objExplosion", x - 16 + myRandom.Next(32), y - 16 + myRandom.Next(32));
                    CObjectManager.Instance.CreateInstance("objExplosion", x - 16 + myRandom.Next(32), y - 16 + myRandom.Next(32));

                    CObjectManager.Instance.m_iOneUps--;
                }
            }

            //powerup
            if (!m_bDead && CObjectManager.Instance.m_bHasStarted)
            {
                CGameObject colTest = CollisionCircle("objPowerUp");

                if (colTest != null)
                {
                    CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_POWERUP);
                    
                    if (colTest.m_iSprInd == 12)
                    {
                        m_bMultiShot = true;
                        m_iMultiShotTimer = 600;
                        CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_MULTISHOT);

                        CObjectManager.Instance.m_bHasMultiShot = true;
                    }
                    else if (colTest.m_iSprInd == 13)
                    {
                        m_bRapidFire = true;
                        m_iRapidFireTimer = 600;
                        CMusicPlayer.Instance.PlaySound(CMusicPlayer.SFX_RAPIDFIRE);

                        CObjectManager.Instance.m_bHasRapidFire = true;
                    }

                    CObjectManager.Instance.DestroyInstance(colTest.m_iIndex);

                    CObjectManager.Instance.m_iScore += 100;
                }
            }

            if (m_iRespawnTimer <= 0 && m_bDead && CObjectManager.Instance.m_iOneUps > 0)
            {
                m_bInvincible = true;
                m_iInvincibilityTimer = 240;
                m_bDead = false;

                x = 400;
                y = 240;

                m_fHorSpeed = 0;
                m_fVerSpeed = 0;
                m_fVelocity = 0;

                m_iMultiShotTimer = 0;
                m_bMultiShot = false;
                CObjectManager.Instance.m_bHasMultiShot = false;

                m_iRapidFireTimer = 0;
                m_bRapidFire = false;
                CObjectManager.Instance.m_bHasRapidFire = false;
            }
            else
                m_iRespawnTimer--;

            if (m_iInvincibilityTimer <= 0 && m_bInvincible)
            {
                m_bInvincible = false;
            }
            else
                m_iInvincibilityTimer--;

            if (m_iMultiShotTimer <= 90)
                CObjectManager.Instance.m_bMultiShotEnding = true;
            else
                CObjectManager.Instance.m_bMultiShotEnding = false;

            if (m_iMultiShotTimer <= 0 && m_bMultiShot)
            {
                m_bMultiShot = false;
                CObjectManager.Instance.m_bHasMultiShot = false;
            }
            else
                m_iMultiShotTimer--;

            if (m_iRapidFireTimer <= 90)
                CObjectManager.Instance.m_bRapidFireEnding = true;
            else
                CObjectManager.Instance.m_bRapidFireEnding = false;

            if (m_iRapidFireTimer <= 0 && m_bRapidFire)
            {
                m_bRapidFire = false;
                CObjectManager.Instance.m_bHasRapidFire = false;
            }
            else
                m_iRapidFireTimer--;
        }

        public override void Render()
        {
            if (m_bInvincible)
                m_bRenderSprite = !m_bRenderSprite;
            else if (m_bDead || !CObjectManager.Instance.m_bHasStarted)
                m_bRenderSprite = false;
            else
                m_bRenderSprite = true;

            if (m_bRenderSprite)
            {
                m_spSprite.Update(x, y, m_fDirection, (int)m_iSprite, (int)m_iImageIndex, 0.5f);
                m_spSprite.Render();
            }
        }
    }
}
