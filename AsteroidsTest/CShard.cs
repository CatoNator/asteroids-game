using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    class CShard : CGameObject
    {
        public float m_iImage;

        private int m_iDestructionTimer;

        private bool m_bRenderSprite = true;

        public override void InstanceSpawn()
        {
            m_iImage = 0;

            this.m_sInstanceType = "objShard";

            m_fDirection = 0;

            m_fVelocity = 1;

            m_iDestructionTimer = 30;
        }

        public override void Update()
        {
            if (m_iImage < 8.4)
                m_iImage += 0.25f;
            else
                m_iImage = 0;

            if (m_iDestructionTimer <= 0)
                CObjectManager.Instance.DestroyInstance(m_iIndex);
            else
                m_iDestructionTimer--;

            m_fHorSpeed = (float)(distDirX(m_fVelocity, degToRad(m_fDirection)));
            m_fVerSpeed = (float)(distDirY(m_fVelocity, degToRad(m_fDirection)));

            x += m_fHorSpeed;
            y += m_fVerSpeed;
        }

        public override void Render()
        {
            m_bRenderSprite = !m_bRenderSprite;

            if (m_bRenderSprite)
            {
                m_spSprite.Update(x, y, 0, 14, (int)m_iImage, 0.25f);
                m_spSprite.Render();
            }
        }
    }
}
