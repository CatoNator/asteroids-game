using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsTest
{
    public class CSprite
    {
        public Texture2D txTexture;
        private SpriteBatch sbSpriteBatch;

        private float x;
        private float y;

        private float m_fRotation;

        public int m_iSpriteIndex;
        private int m_iSprites;

        private int m_iImageIndex;

        private float m_fLayerDepth;

        private int[] m_iSpriteTop; //the top edge of the strip
        private int[] m_iSpriteLeft; //the left edge of the strip
        private int[] m_iSpriteWidth; //the sprite's width
        private int[] m_iSpriteHeight; //the sprite's height
        private int[] m_iSpriteImages; //the amount of images in the strip
        private int[] m_iSpriteOriginX; //the sprite's origin, x direction
        private int[] m_iSpriteOriginY; //the sprite's origin, y direction

        public CSprite(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.txTexture = texture;
            this.sbSpriteBatch = spriteBatch;

            this.x = 0;
            this.y = 0;

            AllocateSprites();
        }

        private void AllocateSprites()
        {
            m_iSprites = 24;

            m_iSpriteIndex = 0;

            m_iImageIndex = 0;
            
            m_iSpriteTop = new int[m_iSprites];
            m_iSpriteLeft = new int[m_iSprites];
            m_iSpriteWidth = new int[m_iSprites];
            m_iSpriteHeight = new int[m_iSprites];
            m_iSpriteImages = new int[m_iSprites];
            m_iSpriteOriginX = new int[m_iSprites];
            m_iSpriteOriginY = new int[m_iSprites];

            //player ship straight
            m_iSpriteTop[0] = 0;
            m_iSpriteLeft[0] = 0;
            m_iSpriteWidth[0] = 48;
            m_iSpriteHeight[0] = 34;
            m_iSpriteImages[0] = 2;
            m_iSpriteOriginX[0] = 30;
            m_iSpriteOriginY[0] = 17;

            //player ship right slight
            m_iSpriteTop[1] = 35;
            m_iSpriteLeft[1] = 0;
            m_iSpriteWidth[1] = 48;
            m_iSpriteHeight[1] = 34;
            m_iSpriteImages[1] = 2;
            m_iSpriteOriginX[1] = 30;
            m_iSpriteOriginY[1] = 17;

            //player ship right heavy
            m_iSpriteTop[2] = 70;
            m_iSpriteLeft[2] = 0;
            m_iSpriteWidth[2] = 48;
            m_iSpriteHeight[2] = 34;
            m_iSpriteImages[2] = 2;
            m_iSpriteOriginX[2] = 30;
            m_iSpriteOriginY[2] = 17;

            //player ship left slight
            m_iSpriteTop[3] = 105;
            m_iSpriteLeft[3] = 0;
            m_iSpriteWidth[3] = 48;
            m_iSpriteHeight[3] = 34;
            m_iSpriteImages[3] = 2;
            m_iSpriteOriginX[3] = 30;
            m_iSpriteOriginY[3] = 17;

            //player ship left heavy
            m_iSpriteTop[4] = 140;
            m_iSpriteLeft[4] = 0;
            m_iSpriteWidth[4] = 48;
            m_iSpriteHeight[4] = 34;
            m_iSpriteImages[4] = 2;
            m_iSpriteOriginX[4] = 30;
            m_iSpriteOriginY[4] = 17;

            //bullet
            m_iSpriteTop[5] = 175;
            m_iSpriteLeft[5] = 0;
            m_iSpriteWidth[5] = 24;
            m_iSpriteHeight[5] = 12;
            m_iSpriteImages[5] = 1;
            m_iSpriteOriginX[5] = 12;
            m_iSpriteOriginY[5] = 6;

            //font
            m_iSpriteTop[6] = 188;
            m_iSpriteLeft[6] = 0;
            m_iSpriteWidth[6] = 7;
            m_iSpriteHeight[6] = 13;
            m_iSpriteImages[6] = 9;
            m_iSpriteOriginX[6] = 0;
            m_iSpriteOriginY[6] = 0;

            //smoke
            m_iSpriteTop[7] = 202;
            m_iSpriteLeft[7] = 0;
            m_iSpriteWidth[7] = 26;
            m_iSpriteHeight[7] = 30;
            m_iSpriteImages[7] = 10;
            m_iSpriteOriginX[7] = 9;
            m_iSpriteOriginY[7] = 13;

            //explosion
            m_iSpriteTop[8] = 233;
            m_iSpriteLeft[8] = 0;
            m_iSpriteWidth[8] = 36;
            m_iSpriteHeight[8] = 39;
            m_iSpriteImages[8] = 8;
            m_iSpriteOriginX[8] = 18;
            m_iSpriteOriginY[8] = 19;

            //big asteroid
            m_iSpriteTop[9] = 0;
            m_iSpriteLeft[9] = 145;
            m_iSpriteWidth[9] = 64;
            m_iSpriteHeight[9] = 64;
            m_iSpriteImages[9] = 1;
            m_iSpriteOriginX[9] = 32;
            m_iSpriteOriginY[9] = 32;

            //med asteroid
            m_iSpriteTop[10] = 65;
            m_iSpriteLeft[10] = 145;
            m_iSpriteWidth[10] = 32;
            m_iSpriteHeight[10] = 32;
            m_iSpriteImages[10] = 1;
            m_iSpriteOriginX[10] = 16;
            m_iSpriteOriginY[10] = 16;

            //smol asteroid
            m_iSpriteTop[11] = 175;
            m_iSpriteLeft[11] = 71;
            m_iSpriteWidth[11] = 16;
            m_iSpriteHeight[11] = 16;
            m_iSpriteImages[11] = 0;
            m_iSpriteOriginX[11] = 8;
            m_iSpriteOriginY[11] = 8;

            //multishot coin
            m_iSpriteTop[12] = 98;
            m_iSpriteLeft[12] = 145;
            m_iSpriteWidth[12] = 15;
            m_iSpriteHeight[12] = 15;
            m_iSpriteImages[12] = 9;
            m_iSpriteOriginX[12] = 7;
            m_iSpriteOriginY[12] = 7;

            //rapidfire coin
            m_iSpriteTop[13] = 114;
            m_iSpriteLeft[13] = 145;
            m_iSpriteWidth[13] = 15;
            m_iSpriteHeight[13] = 15;
            m_iSpriteImages[13] = 9;
            m_iSpriteOriginX[13] = 7;
            m_iSpriteOriginY[13] = 7;

            //shard
            m_iSpriteTop[14] = 192;
            m_iSpriteLeft[14] = 71;
            m_iSpriteWidth[14] = 8;
            m_iSpriteHeight[14] = 8;
            m_iSpriteImages[14] = 8;
            m_iSpriteOriginX[14] = 4;
            m_iSpriteOriginY[14] = 4;

            //"up"
            m_iSpriteTop[15] = 175;
            m_iSpriteLeft[15] = 49;
            m_iSpriteWidth[15] = 15;
            m_iSpriteHeight[15] = 12;
            m_iSpriteImages[15] = 0;
            m_iSpriteOriginX[15] = 0;
            m_iSpriteOriginY[15] = 0;

            //"push start"
            m_iSpriteTop[16] = 65;
            m_iSpriteLeft[16] = 210;
            m_iSpriteWidth[16] = 81;
            m_iSpriteHeight[16] = 8;
            m_iSpriteImages[16] = 0;
            m_iSpriteOriginX[16] = 40;
            m_iSpriteOriginY[16] = 4;

            //game over
            m_iSpriteTop[17] = 130;
            m_iSpriteLeft[17] = 145;
            m_iSpriteWidth[17] = 67;
            m_iSpriteHeight[17] = 16;
            m_iSpriteImages[17] = 1;
            m_iSpriteOriginX[17] = 0;
            m_iSpriteOriginY[17] = 8;

            //catonator logo
            m_iSpriteTop[18] = 147;
            m_iSpriteLeft[18] = 145;
            m_iSpriteWidth[18] = 72;
            m_iSpriteHeight[18] = 33;
            m_iSpriteImages[18] = 0;
            m_iSpriteOriginX[18] = 0;
            m_iSpriteOriginY[18] = 0;

            //"hiscore"
            m_iSpriteTop[19] = 175;
            m_iSpriteLeft[19] = 88;
            m_iSpriteWidth[19] = 36;
            m_iSpriteHeight[19] = 8;
            m_iSpriteImages[19] = 0;
            m_iSpriteOriginX[19] = 0;
            m_iSpriteOriginY[19] = 0;

            //hiscore font
            m_iSpriteTop[20] = 181;
            m_iSpriteLeft[20] = 145;
            m_iSpriteWidth[20] = 6;
            m_iSpriteHeight[20] = 8;
            m_iSpriteImages[20] = 9;
            m_iSpriteOriginX[20] = 0;
            m_iSpriteOriginY[20] = 0;

            //paused
            m_iSpriteTop[21] = 74;
            m_iSpriteLeft[21] = 210;
            m_iSpriteWidth[21] = 47;
            m_iSpriteHeight[21] = 8;
            m_iSpriteImages[21] = 0;
            m_iSpriteOriginX[21] = 23;
            m_iSpriteOriginY[21] = 4;

            //game logo
            m_iSpriteTop[22] = 284;
            m_iSpriteLeft[22] = 0;
            m_iSpriteWidth[22] = 228;
            m_iSpriteHeight[22] = 38;
            m_iSpriteImages[22] = 0;
            m_iSpriteOriginX[22] = 114;
            m_iSpriteOriginY[22] = 19;

            //"new hi score!"
            m_iSpriteTop[23] = 273;
            m_iSpriteLeft[23] = 0;
            m_iSpriteWidth[23] = 84;
            m_iSpriteHeight[23] = 10;
            m_iSpriteImages[23] = 0;
            m_iSpriteOriginX[23] = 42;
            m_iSpriteOriginY[23] = 5;
        }

        public float degToRad(float degrees)
        {
            return degrees * ((float)Math.PI) / 180.0f;
        }

        public void Update(float x, float y, float rot, int spriteIndex, int imageIndex, float layerDepth)
        {
            this.x = x;
            this.y = y;

            this.m_fRotation = -degToRad(rot);

            this.m_iSpriteIndex = spriteIndex;

            this.m_fLayerDepth = layerDepth;

            if (imageIndex > m_iSpriteImages[m_iSpriteIndex])
                this.m_iImageIndex = m_iSpriteImages[m_iSpriteIndex];
            else
                this.m_iImageIndex = imageIndex;
        }

        public void Render()
        {
            //int w = txTexture.Width;
            //int h = txTexture.Height;

            Vector2 Location = new Vector2(x, y);
            Rectangle sourceRectangle = new Rectangle(m_iSpriteLeft[m_iSpriteIndex]+(m_iSpriteWidth[m_iSpriteIndex]*m_iImageIndex), m_iSpriteTop[m_iSpriteIndex], m_iSpriteWidth[m_iSpriteIndex], m_iSpriteHeight[m_iSpriteIndex]);
            Rectangle destRectangle = new Rectangle((int)x, (int)y, m_iSpriteWidth[m_iSpriteIndex], m_iSpriteHeight[m_iSpriteIndex]);
            Vector2 Origin = new Vector2(m_iSpriteOriginX[m_iSpriteIndex], m_iSpriteOriginY[m_iSpriteIndex]);

            sbSpriteBatch.Draw(txTexture, destRectangle, sourceRectangle, Color.White, m_fRotation, Origin, SpriteEffects.None, m_fLayerDepth);
        }
    }
}
