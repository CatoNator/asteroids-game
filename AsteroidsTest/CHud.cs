using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsTest
{
    public class CHud : CGameObject
    {
        public CSprite[] m_sprScoreDigit;

        public CSprite[] m_sprHiScoreDigit;

        public CSprite m_sprHiScore;

        public CSprite m_sprOneUpMeter;

        public CSprite m_sprUpSymbol;

        public CSprite m_sprMultiShotIcon;

        public CSprite m_sprRapidFireIcon;

        public CSprite m_sprPaused;

        public CSprite m_sprGameOver;

        private bool m_bGameOver = false;

        private int m_iScoreDigits;

        public int[] m_iScoreDigit;

        public int[] m_iHiScoreDigit;

        public int m_iScoreToAdd;

        private int m_iCreationTicks;

        private const int m_iMaxAsteroids = 25;

        private bool m_bRenderMultiShot = false;

        private bool m_bRenderRapidFire = false;

        private bool m_bPaused = false;

        private KeyboardState oldState;

        public override void InstanceSpawn()
        {
            this.m_iScoreDigits = 8;

            this.m_sprScoreDigit = new CSprite[m_iScoreDigits];

            this.m_sprHiScoreDigit = new CSprite[m_iScoreDigits];

            this.m_iScoreDigit = new int[m_iScoreDigits];

            this.m_iHiScoreDigit = new int[m_iScoreDigits];

            this.m_iScoreToAdd = 100000000;

            m_iCreationTicks = 240;

            this.m_sInstanceType = "objHUD";

            m_sprOneUpMeter = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprUpSymbol = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprMultiShotIcon = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprRapidFireIcon = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprHiScore = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprPaused = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            m_sprGameOver = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

            for (int i = 0; i < m_iScoreDigits; i++)
            {
                m_iScoreDigit[i] = 0;
                m_sprScoreDigit[i] = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);

                m_iHiScoreDigit[i] = 0;
                m_sprHiScoreDigit[i] = new CSprite(CObjectManager.Instance.m_txTexturePage, CObjectManager.Instance.m_sbSpriteBatch);
            }

            SetHiScore();

            CMusicPlayer.Instance.Play(CMusicPlayer.MENU_SONG);
        }

        private int ScoreCounterValue()
        {
            int value = 0;

            for (int i = 0; i < m_iScoreDigits; i++)
            {
                value += m_iScoreDigit[i]*((int)Math.Pow(10, i));
            }
            
            return value;
        }

        private int HiScoreCounterValue()
        {
            int value = 0;

            for (int i = 0; i < m_iScoreDigits; i++)
            {
                value += m_iHiScoreDigit[i] * ((int)Math.Pow(10, i));
            }

            return value;
        }

        private void SetHiScore()
        {
            while(HiScoreCounterValue() < CObjectManager.Instance.m_iHiScore)
            {
                m_iHiScoreDigit[0]++;
                
                for (int i = 0; i < m_iScoreDigits - 1; i++)
                {
                    if (m_iHiScoreDigit[i] > 9)
                    {
                        m_iHiScoreDigit[i + 1] += (int)Math.Floor(m_iHiScoreDigit[i] / 10.0f);
                        m_iHiScoreDigit[i] = m_iHiScoreDigit[i] % 10;
                    }
                }
            }
        }

        public override void Update()
        {
            if (ScoreCounterValue() < CObjectManager.Instance.m_iScore)
            {
                m_iScoreDigit[0] += 50;
            }

            for (int i = 0; i < m_iScoreDigits-1; i++)
            {
                if (m_iScoreDigit[i] > 9)
                {
                    m_iScoreDigit[i + 1] += (int)Math.Floor(m_iScoreDigit[i]/10.0f);
                    m_iScoreDigit[i] = m_iScoreDigit[i]%10;
                }
            }

            if (!m_bPaused && !m_bGameOver && CObjectManager.Instance.m_bHasStarted)
            {
                m_iCreationTicks--;

                if (m_iCreationTicks <= 0)
                {
                    if (InstanceNumber("objAsteroid") < m_iMaxAsteroids && !CObjectManager.Instance.m_bGameOver)
                        CObjectManager.Instance.CreateInstance("objAsteroid", -32 + myRandom.Next(864), -31);

                    m_iCreationTicks = 240;
                }
            }

            if (CObjectManager.Instance.m_iOneUps <= 0 && !m_bGameOver)
            {
                m_bGameOver = true;

                CMusicPlayer.Instance.Play(CMusicPlayer.GAMEOVER_SONG);

                if (CObjectManager.Instance.m_iScore > CObjectManager.Instance.m_iHiScore)
                    System.IO.File.WriteAllText("hiscr", CObjectManager.Instance.m_iScore.ToString());
            }

            //input
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && !CObjectManager.Instance.m_bHasStarted)
            {
                CObjectManager.Instance.m_bHasStarted = true;

                CMusicPlayer.Instance.Play(CMusicPlayer.GAME_SONG);
            }

            //pausing
            if (oldState.IsKeyUp(Keys.P) && keyboardState.IsKeyDown(Keys.P))
            {
                if (!m_bPaused)
                {
                    DeactivateInstances(true);
                    m_bPaused = true;
                }
                else if (m_bPaused)
                {
                    ActivateInstances();
                    m_bPaused = false;
                }
            }

            oldState = keyboardState;
        }

        public override void Render()
        {
            m_sprHiScore.Update(10, 10, 0, 19, 0, 1.0f);
            m_sprHiScore.Render();

            for (int i = 0; i < m_iScoreDigits; i++)
            {
                m_sprHiScoreDigit[i].Update(47 + ((m_iScoreDigits - 1) * 6) - (i * 6), 10, 0, 20, m_iHiScoreDigit[i], 1.0f);
                m_sprHiScoreDigit[i].Render();
            }
            
            for (int i = 0; i < m_iScoreDigits; i++)
            {
                m_sprScoreDigit[i].Update(10 + ((m_iScoreDigits-1) * 8) - (i * 8), 20, 0, 6, m_iScoreDigit[i], 1.0f);
                m_sprScoreDigit[i].Render();
            }

            m_sprOneUpMeter.Update(10, 35, 0, 6, CObjectManager.Instance.m_iOneUps, 1.0f);
            m_sprOneUpMeter.Render();

            m_sprUpSymbol.Update(18, 35, 0, 15, 0, 1.0f);
            m_sprUpSymbol.Render();

            if (CObjectManager.Instance.m_bHasMultiShot && CObjectManager.Instance.m_bMultiShotEnding)
                m_bRenderMultiShot = !m_bRenderMultiShot;
            else if (CObjectManager.Instance.m_bHasMultiShot)
                m_bRenderMultiShot = true;
            else
                m_bRenderMultiShot = false;

            if (CObjectManager.Instance.m_bHasRapidFire && CObjectManager.Instance.m_bRapidFireEnding)
                m_bRenderRapidFire = !m_bRenderRapidFire;
            else if (CObjectManager.Instance.m_bHasRapidFire)
                m_bRenderRapidFire = true;
            else
                m_bRenderRapidFire = false;

            if (m_bRenderMultiShot)
            {
                m_sprMultiShotIcon.Update(17, 57, 0, 12, 0, 1.0f);
                m_sprMultiShotIcon.Render();
            }

            if (m_bRenderRapidFire)
            {
                m_sprRapidFireIcon.Update(33, 57, 0, 13, 0, 1.0f);
                m_sprRapidFireIcon.Render();
            }

            if (m_bPaused)
            {
                m_sprPaused.Update(400, 240, 0, 21, 0, 1.0f);
                m_sprPaused.Render();
            }
            
            if (!CObjectManager.Instance.m_bHasStarted)
            {
                //game logo
                m_sprGameOver.Update(400, 190, 0, 22, 0, 1.0f);
                m_sprGameOver.Render();
                
                //push start
                m_sprGameOver.Update(400, 300, 0, 16, 0, 1.0f);
                m_sprGameOver.Render();

                //my logo
                m_sprGameOver.Update(10, 480-44, 0, 18, 0, 1.0f);
                m_sprGameOver.Render();
            }
            else if (m_bGameOver)
            {
                m_sprGameOver.Update(329, 240, 0, 17, 0, 1.0f);
                m_sprGameOver.Render();

                m_sprGameOver.Update(404, 240, 0, 17, 1, 1.0f);
                m_sprGameOver.Render();

                if (CObjectManager.Instance.m_iScore > CObjectManager.Instance.m_iHiScore)
                {
                    m_sprGameOver.Update(400, 260, 0, 23, 1, 1.0f);
                    m_sprGameOver.Render();
                }
            }
        }
    }
}
