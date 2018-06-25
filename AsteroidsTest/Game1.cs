using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace AsteroidsTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Texture2D m_txTexturePage;
        public Texture2D m_txBackTexture;

        private Random myRandom = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_txTexturePage = Content.Load<Texture2D>("TextureSheet");
            m_txBackTexture = Content.Load<Texture2D>("Background");

            /*CMusicPlayer.Instance.Explosion1 = Content.Load<SoundEffect>("Sounds/explosion1");
            //CMusicPlayer.Instance.PlayerDeath = CMusicPlayer.Instance.explosion1;
            CMusicPlayer.Instance.Explosion2 = Content.Load<SoundEffect>("Sounds/explosion2");
            CMusicPlayer.Instance.BlasterShot = Content.Load<SoundEffect>("Sounds/normalshot");
            CMusicPlayer.Instance.RapidShot = Content.Load<SoundEffect>("Sounds/rapidfireshot");
            CMusicPlayer.Instance.PowerUp = Content.Load<SoundEffect>("Sounds/powerup");
            //CMusicPlayer.Instance.MultiShot = Content.Load<SoundEffect>("Sounds/multishot");
            //CMusicPlayer.Instance.RapidFire = Content.Load<SoundEffect>("Sounds/rapidfire");*/

            string HiScoreStr = System.IO.File.ReadAllText("hiscr");

            CObjectManager.Instance.m_iHiScore = Int32.Parse(HiScoreStr);

            CMusicPlayer.Instance.LoadAudio();

            CObjectManager.Instance.LoadContent(m_txTexturePage, spriteBatch);

            CObjectManager.Instance.CreateInstance("objHUD", 0, 0);
            CObjectManager.Instance.CreateInstance("objPlayer", 400, 240);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            CMusicPlayer.Instance.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*objectCreationTick--;

            if (objectCreationTick <= 0)
            {
                CObjectManager.Instance.CreateInstance("objPlayer", 64 + myRandom.Next(64), 64 + myRandom.Next(64));
                objectCreationTick = 5+(int)myRandom.Next(15);
            }*/

            CObjectManager.Instance.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);

            Rectangle destRectangle = new Rectangle(0, 0, 800, 480);

            spriteBatch.Draw(m_txBackTexture, new Rectangle(0, 0, 800, 480), Color.White);

            CObjectManager.Instance.Render();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
