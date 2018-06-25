using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsTest
{
    public class CGameObject
    {
        public bool m_bActive = true;
        
        public float x;
        public float y;

        public int m_iIndex;

        public int m_iSprInd;

        public float m_fHorSpeed;
        public float m_fVerSpeed;

        public float m_fDirection;
        public float m_fVelocity;

        public CSprite m_spSprite;

        public int m_iCollisionRadius;

        public Random myRandom = new Random();

        public string m_sInstanceType;

        public CGameObject()
        {
        }

        public void Spawn(float t_x, float t_y, int index)
        {
            this.x = t_x;
            this.y = t_y;
            this.m_fHorSpeed = 0;
            this.m_fVerSpeed = 0;

            this.m_iIndex = index;

            this.m_fDirection = 0;

            this.m_fVelocity = 0;

            m_spSprite = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            InstanceSpawn();
        }

        public virtual void InstanceSpawn()
        {
        }

        public virtual void OnDestruction()
        {
        }

        public float degToRad(float degrees)
        {
            return degrees * ((float)Math.PI) / 180.0f;
        }
        
        public double distDirX(float dist, float dir)
        {
            return (Math.Cos(dir) * dist);
        }

        public double distDirY(float dist, float dir)
        {
            return (-Math.Sin(dir) * dist);
        }

        public double pointDirection(float x1, float y1, float x2, float y2)
        {
            return Math.Atan2((double)(y2 - y1), (double)(x2 - x1));
        }

        public int InstanceNumber(String instanceType)
        {
            int instances = 0;

            for (int i = 0; i < CObjectManager.Instance.m_iGameObjects; i++)
            {
                //if the other object type is the one we're looking for
                if ((CObjectManager.Instance.m_pGameObjectList[i] != null)
                    && instanceType.Equals(CObjectManager.Instance.m_pGameObjectList[i].m_sInstanceType))
                    instances++;
            }

            return instances;
        }

        public void DeactivateInstances (bool notMe)
        {
            for (int i = 0; i < CObjectManager.Instance.m_iGameObjects; i++)
            {
                //if the other object type is the one we're looking for
                if (CObjectManager.Instance.m_pGameObjectList[i] != null)
                {
                    if (notMe && i != m_iIndex)
                        CObjectManager.Instance.m_pGameObjectList[i].m_bActive = false;
                }
            }
        }

        public void ActivateInstances()
        {
            for (int i = 0; i < CObjectManager.Instance.m_iGameObjects; i++)
            {
                //if the other object type is the one we're looking for
                if (CObjectManager.Instance.m_pGameObjectList[i] != null)
                {
                    CObjectManager.Instance.m_pGameObjectList[i].m_bActive = true;
                }
            }
        }

        public void SetIndex(int index)
        {
            this.m_iIndex = index;
        }

        public virtual void Update()
        {
        }

        public CGameObject CollisionCircle(string instanceType)
        {
            CGameObject collidedInstance = null;
            CGameObject otherInstance = null;

            //getting the object to collide with

            //looping through the object list
            for (int i = 0; i < CObjectManager.Instance.m_iGameObjects; i++)
            {
                //if the other object type is the one we're looking for
                if ((CObjectManager.Instance.m_pGameObjectList[i] != null)
                    && instanceType.Equals(CObjectManager.Instance.m_pGameObjectList[i].m_sInstanceType))
                {
                    //getting the reference
                    otherInstance = CObjectManager.Instance.m_pGameObjectList[i];

                    //measuring the distance
                    double dist = (float)Math.Sqrt(Math.Pow(this.x - otherInstance.x, 2) + Math.Pow(this.y - otherInstance.y, 2));

                    //if distance is smaller than the collision radiuses
                    if (dist <= (double)(this.m_iCollisionRadius + otherInstance.m_iCollisionRadius))
                        collidedInstance = otherInstance;
                }
            }

            return collidedInstance;
        }

        public virtual void Render()
        {
            m_spSprite.Update(x, y, m_fDirection, 0, 0, 0.0f);
            m_spSprite.Render();
        }
    }
}
