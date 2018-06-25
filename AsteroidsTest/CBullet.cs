using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    public class CBullet : CGameObject 
    {
        public float m_fRotation;

        public int m_iPower;

        public override void InstanceSpawn()
        {
            this.m_fDirection = 0;

            this.m_fVelocity = 8;

            this.m_fRotation = 0;

            this.m_sInstanceType = "objBullet";

            this.m_iCollisionRadius = 10;

            m_iPower = 0;
        }

        public override void Update()
        {
            m_fRotation += 10;
            
            //capping the rotation
            if (m_fRotation >= 360)
                m_fRotation -= 360;

            if (m_fRotation < 0)
                m_fRotation += 360;

            //movement
            m_fHorSpeed = (float)(distDirX(m_fVelocity, degToRad(m_fDirection)));
            m_fVerSpeed = (float)(distDirY(m_fVelocity, degToRad(m_fDirection)));

            x += m_fHorSpeed;
            y += m_fVerSpeed;

            //destroying the bullet outside the bounds
            if ((x > 848) || (x < 0) || (y > 480) || (y < 0))
                CObjectManager.Instance.DestroyInstance(m_iIndex);

            //collisions
            CGameObject colTest = CollisionCircle("objAsteroid");

            if (colTest != null)
            {
                CObjectManager.Instance.DestroyInstance(colTest.m_iIndex);
                CObjectManager.Instance.DestroyInstance(m_iIndex);
            }
        }
        
        public override void Render()
        {
            m_spSprite.Update(x, y, m_fRotation, 5, m_iPower, 0.25f);
            m_spSprite.Render();
        }
    }
}
