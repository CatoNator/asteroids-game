using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    class CPowerUp : CGameObject
    {
        public float m_fImage;

        private int m_iDespawnTimer;

        private bool m_bRenderSprite;

        public override void InstanceSpawn()
        {
            this.m_fDirection = myRandom.Next(360);

            this.m_fVelocity = 1;

            m_fHorSpeed = (float)(distDirX(m_fVelocity, degToRad(m_fDirection)));
            m_fVerSpeed = (float)(distDirY(m_fVelocity, degToRad(m_fDirection)));

            this.m_sInstanceType = "objPowerUp";

            m_iCollisionRadius = 8;

            float typeChance = myRandom.Next(3);

            if (typeChance < 1.5f)
                this.m_iSprInd = 12;
            else
                this.m_iSprInd = 13;

            this.m_fImage = 0;

            this.m_iDespawnTimer = 360;
        }

        public override void Update()
        {
            if (m_fImage < 9.4)
                m_fImage += 0.25f;
            else
                m_fImage = 0;

            //reflecting off the bounds
            if ((x > 793) || (x < 7))
                m_fHorSpeed = -m_fHorSpeed;
            if ((y > 474) || (y < 7))
                m_fVerSpeed = -m_fVerSpeed;
            
            //movement
            x += m_fHorSpeed;
            y += m_fVerSpeed;

            if (m_iDespawnTimer <= 0)
                CObjectManager.Instance.DestroyInstance(m_iIndex);
            else
                m_iDespawnTimer--;
        }

        public override void Render()
        {
            if (m_iDespawnTimer <= 90)
                m_bRenderSprite = !m_bRenderSprite;
            else
                m_bRenderSprite = true;
            
            if (m_bRenderSprite)
            {
                m_spSprite.Update(x, y, 0, m_iSprInd, (int)m_fImage, 0.25f);
                m_spSprite.Render();
            }
        }
    }
}
