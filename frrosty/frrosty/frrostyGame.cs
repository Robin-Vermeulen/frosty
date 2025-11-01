using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.Screens;
using System;

namespace frrosty
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class frrostyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public frrostyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GameSettings.screenWitdh;
            _graphics.PreferredBackBufferHeight = GameSettings.screenHeight;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Data.contentManager = Content;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine(Data.contentManager.RootDirectory);
            GameSettings.TextureStartBackgroud = Data.LoadTexture2D("Graphics/startBackground");
            GameSettings.TextureHead = Data.LoadTexture2D("Graphics/frosty-head-spritesheet");
            GameSettings.TextureIceblock = Data.LoadTexture2D("Graphics/IceBlock");
            GameSettings.TextureSpikes = Data.LoadTexture2D("Graphics/spikes");
            GameSettings.TextureSnowball = Data.LoadTexture2D("Graphics/snowball");
            GameSettings.TextureTopHat = Data.LoadTexture2D("Graphics/TopHat");
            GameSettings.TextureBird = Data.LoadTexture2D("Graphics/Bird_spritesheet");
            GameSettings.TextureCoin = Data.LoadTexture2D("Graphics/Coin_spriteSheet");
            GameSettings.TextureFlag = Data.LoadTexture2D("Graphics/Flag");
            GameSettings.TextureLava = Data.LoadTexture2D("Graphics/Lava");
            GameSettings.TexturePlayBackgroud = Data.LoadTexture2D("Graphics/Playbackground");
            GameSettings.TexturePlayBackgroud = Data.LoadTexture2D("Graphics/Playbackground");
            GameSettings.TexturePowerUpBackgroud = Data.LoadTexture2D("Graphics/powerUppBackground");
            GameSettings.TexturePowerUp = Data.LoadTexture2D("Graphics/PowerUpp");
            GameSettings.TextureCrossHair = Data.LoadTexture2D("Graphics/Warning");
            GameSettings.TextureUnderIceBlock = Data.LoadTexture2D("Graphics/grond_onder");
            GameSettings.TextureGameOverBackground = Data.LoadTexture2D("Graphics/GameOverScreen");
            GameSettings.TextureBullet = Data.LoadTexture2D("Graphics/bullet");
            GameSettings.TextureButton = Data.LoadTexture2D("Graphics/button");
            GameSettings.font = Data.LoadSpriteFont("Fonts/buttonFont");

            GameSettings.SoundCoin = Data.LoadSoundEffect("Graphics/Fonts/Sound/Coin");
            GameSettings.SoundPop = Data.LoadSoundEffect("Graphics/Fonts/Sound/pop");
            GameSettings.SoundSnow = Data.LoadSoundEffect("Graphics/Fonts/Sound/snow");
            GameSettings.SoundPower = Data.LoadSoundEffect("Graphics/Fonts/Sound/powerUp");
            //GameSettings.SoundGameover = Data.LoadSoundEffect("Graphics/Fonts/Sound/GameOverSound2");

            //GameSettings.powerUppSong = Data.LoadSong("Graphics/Fonts/Sound/SongPowerUp");
            //GameSettings.PlaySong = Data.LoadSong("Graphics/Fonts/Sound/song");

            GameSettings.activeScreen = new StartScreen(); ;
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            Logic.ChangeVolume();

            GameSettings.activeScreen.Update(gameTime);

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            GameSettings.activeScreen.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
