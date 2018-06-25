using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    class CExplosion : CGameObject
    {
        public float m_iImage;

        public int m_iSprite;

        public override void InstanceSpawn()
        {
            m_iImage = 0;

            m_iSprite = 8;

            this.m_sInstanceType = "objExplosion";

            m_fDirection = myRandom.Next(360);
        }

        public override void Update()
        {
            if (m_iImage < 8.4)
                m_iImage += 0.25f;
            else
                CObjectManager.Instance.DestroyInstance(m_iIndex);
        }

        public override void Render()
        {
            m_spSprite.Update(x, y, m_fDirection, m_iSprite, (int)m_iImage, 0.25f);
            m_spSprite.Render();
        }
    }
}
