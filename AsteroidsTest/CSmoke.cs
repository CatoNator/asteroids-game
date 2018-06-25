using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    class CSmoke : CGameObject
    {
        public float m_iImage;

        public override void InstanceSpawn()
        {
            m_iImage = 0;

            this.m_sInstanceType = "objSmoke";

            m_fDirection = myRandom.Next(360);
        }

        public override void Update()
        {
            if (m_iImage < 10.4)
                m_iImage += 0.25f;
            else
                CObjectManager.Instance.DestroyInstance(m_iIndex);
        }

        public override void Render()
        {
            m_spSprite.Update(x, y, m_fDirection, 7, (int)m_iImage, 0.25f);
            m_spSprite.Render();
        }
    }
}
