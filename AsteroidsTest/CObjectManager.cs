using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsTest
{
    public sealed class CObjectManager
    {
        private int m_iMaxInstances = 128;

        public CGameObject[] m_pGameObjectList;
        public int m_iGameObjects;

        public Texture2D m_txTexturePage;
        public SpriteBatch m_sbSpriteBatch;

        public int m_iScore = 0;

        public int m_iHiScore = 0;

        public int m_iOneUps = 5;

        public bool m_bHasRapidFire = false;
        public bool m_bHasMultiShot = false;
        public bool m_bRapidFireEnding = false;
        public bool m_bMultiShotEnding = false;

        public bool m_bGameOver = false;

        public bool m_bHasStarted = false;
        
        private CObjectManager()
        {
            this.m_iGameObjects = 0;

            m_pGameObjectList = new CGameObject[m_iMaxInstances];
        }
        
        public static CObjectManager Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly CObjectManager instance = new CObjectManager();
        }

        public void LoadContent(Texture2D texturePage, SpriteBatch spriteBatch)
        {
            m_txTexturePage = texturePage;
            m_sbSpriteBatch = spriteBatch;
        }

        private CGameObject AddInstance(string instanceType)
        {
            //this thing returns the correct object type
            if (instanceType.Equals("objPlayer"))
                return new CPlayer();
            else if (instanceType.Equals("objBullet"))
                return new CBullet();
            else if (instanceType.Equals("objAsteroid"))
                return new CAsteroid();
            else if (instanceType.Equals("objSmoke"))
                return new CSmoke();
            else if (instanceType.Equals("objExplosion"))
                return new CExplosion();
            else if (instanceType.Equals("objPowerUp"))
                return new CPowerUp();
            else if (instanceType.Equals("objShard"))
                return new CShard();
            else
                return new CHud();
            /*
            in the future:
             * index 0 = player
             * index 1 = boolet
             * index 2 = smol asteroid
             * index 3 = med asteroid
             * index 4 = BIG THE CATsteroid
             * index 5 = 1up or something lmao
            */
        }

        public void CreateInstance(string instanceType, float x, float y)
        {
            if (m_iGameObjects < m_iMaxInstances)
            {
                m_pGameObjectList[m_iGameObjects] = AddInstance(instanceType);
                m_pGameObjectList[m_iGameObjects].Spawn(x, y, m_iGameObjects);
                m_iGameObjects++;
            }
        }

        public void DestroyInstance(int index)
        {
            if (m_pGameObjectList[index] != null)
            {
                m_pGameObjectList[index].OnDestruction();
                
                m_pGameObjectList[index] = null;

                for (int i = index + 1; i < m_iMaxInstances; i++) //moving ever
                {
                    if (m_pGameObjectList[i] != null)
                    {
                        m_pGameObjectList[i - 1] = m_pGameObjectList[i];
                    }
                }

                m_pGameObjectList[m_iGameObjects - 1] = null; //making sure the last thing in the array is nothing

                m_iGameObjects = 0;
                for (int i = 0; i < m_iMaxInstances; i++) //indexing everything
                {
                    if (m_pGameObjectList[i] != null)
                    {
                        m_pGameObjectList[i].SetIndex(i);
                        m_iGameObjects++;
                    }
                }

                
                //Console.WriteLine("Removed object with index of " + index + " Gameobjects " + m_iGameObjects);
            }
            /*else
                Console.WriteLine("Tried to remove a nonexistent object with index of " + index);*/
        }

        public void Update()
        {
            if (m_iOneUps > 9)
                m_iOneUps = 9;
            else if (m_iOneUps < 0)
                m_iOneUps = 0;

            if (m_iScore > 99999999)
                m_iScore = 99999999;
            
            for (int i = 0; i < m_iMaxInstances; i++)
            {
                if (m_pGameObjectList[i] != null && m_pGameObjectList[i].m_bActive)
                    m_pGameObjectList[i].Update();
            }

            /*Console.WriteLine("New frame!");
            for (int i = 0; i < m_iMaxInstances; i++)
            {
                if (m_pGameObjectList[i] != null)
                    Console.WriteLine("Object at array slot " + i + " with an index of " + m_pGameObjectList[i].m_iIndex);
            }
            Console.WriteLine("Total objects " + m_iGameObjects);*/
        }

        public void Render ()
        {
            for (int i = 0; i < m_iMaxInstances; i++)
            {
                if (m_pGameObjectList[i] != null)
                    m_pGameObjectList[i].Render();
            }
        }
    }
}
