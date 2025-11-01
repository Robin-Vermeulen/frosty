using PD_1_Project_FrostyGame.PD1.GameObjects.Avatar;
using System;

using PD_1_Project_FrostyGame.PD1.Screens;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace PD_1_Project_FrostyGame.PD1.General
{
    public static class GameSettings
    {
        public static int BulletCoolDown = 60;
        public static int BulletSpeed = 5;
        #region PlaySettings
        public static List<PlayerScore> ListOfScores = new List<PlayerScore>();
        public static Point StartScreenTopScoresBoxSize { get { return new Point((int)(screenWitdh / 2f), screenWitdh / 6); } }
        public static Point StartScreenPlayButtonSize { get { return new Point(screenWitdh / 4, screenWitdh / 16); } }
        public static Point GameOverScreenAchievedScoreBoxSize { get { return new Point(screenWitdh / 4, (int)(screenHeight / 3.5f)); } }
        public static Point GameOverScreenRestartBoxSize { get { return new Point(screenWitdh / 4, screenHeight / 12); } }
        public static Point GameOverScreenTopScoresBoxSize { get { return new Point(screenWitdh / 2, screenHeight / 2); } }
        public static Point PlayScreenCoinBoxSize { get { return new Point((int)(SizeOfObjects.X * 5), (int)(SizeOfObjects.Y * 2f)); } }
        public static Point PlayScreenScoresBoxSize { get { return new Point((int)(SizeOfObjects.X * 5), (int)(SizeOfObjects.Y * 2f)); } }
        public static string ScoreFile { get; } = "../../../content/files/Scores.json";
        public static float Volumeincrement { get; } = 0.3f;
        public static bool IsLevelplatforms = false;
        public const int levelHeight = 15;
        public const int levelWithd = 15;           //how many objects high and wide
        public static TimeSpan WhereInSong;
        public static float scale { get; } = 1f;             //scalles all proportion, including window size
        public static Color PowerUpColor { get; } = Color.Red;
        public static float BirdSpeedMultiplier = 3f;

        public static float Volume { get; set; } = 0.5f;


        public static int ammountOflevelsBeforeFlag { get; } = 10;

        public static int screenWitdh { get; set; } = (int)((levelWithd * 2f) * SizeOfObjects.X);        //the widht and height of the screen
        public static int screenHeight { get; set; } = (int)(levelHeight * SizeOfObjects.Y);

        public static bool isPowerUpAvitve { get; set; } = false;
        public static float durationPowerUp { get; set; } = 10f * 60;
        public static int scorerMultiplier { get; set; } = 1;
        public static Vector2 SizeOfObjects { get => new Vector2(63f * scale, 63f * scale); }   //how big each object is the sprites given are 50x50 so i wouldnt change that
        public static bool isShowingHitboxes { get; } = false;
        #endregion
        #region avatarSettings
        public static AvatarObject avatar { get; set; }                        //avatar object
        public static Vector2 Startposition { get; } = new Vector2(100, screenHeight / 3 + SizeOfObjects.Y / 2); //where the head of the avatar starts
        public static int FlagCount { get; set; }
        public static float OriginalSpeed { get; } = 2.5f * scale;
        public static float WorldVelocety { get; set; } = OriginalSpeed;           //how fast everything moves left

        public static float fallSpeed { get; private set; } = 4 * scale;          //how fast everything moves down
        #endregion
        #region screens
        public static Screen activeScreen { get; set; }         //what screen is currently being shown
        public static StartScreen startScreen { get; set; }
        public static PlayScreen playScreen { get; set; }
        public static EndScreen endScreen { get; set; }

        #endregion
        #region Content
        public static String fileScore = "Content/Files/Scores.txt";

        public static Texture2D TextureIceblock { get; set; }                  //textures used to draw the sprites
        public static Texture2D TextureButton { get; set; }                  //textures used to draw the sprites
        public static Texture2D TextureSpikes { get; set; }
        public static Texture2D TextureHead { get; set; }
        public static Texture2D TextureSnowball { get; set; }
        public static Texture2D TextureTopHat { get; set; }
        public static Texture2D TextureLava { get; set; }
        public static Texture2D TextureBird { get; set; }
        public static Texture2D TextureCrossHair { get; set; }
        public static Texture2D TextureUnderIceBlock { get; set; }
        public static Texture2D TextureCoin { get; set; }
        public static Texture2D TextureFlag { get; set; }
        public static Texture2D TexturePowerUp { get; set; }
        public static Texture2D TexturePlayBackgroud { get; set; }
        public static Texture2D TextureGameOverBackground { get; set; }
        public static Texture2D TextureStartBackgroud { get; set; }
        public static Texture2D TexturePowerUpBackgroud { get; set; }
        public static Texture2D TextureActivePlayBackground { get; set; }

        public static SoundEffect SoundCoin { get; set; }
        public static SoundEffect SoundPop { get; set; }
        public static SoundEffect SoundSnow { get; set; }
        public static SoundEffect SoundPower { get; set; }
        public static SoundEffect SoundGameover { get; set; }
        public static Song PlaySong { get; set; }
        public static Song powerUppSong { get; set; }

        public static SpriteFont font { get; set; }
        #endregion
        #region score
        private static float _score = 0;
        public static float Score { get => _score; set => _score = value; }
        private static float _distance = 0;
        public static float Distance { get => _distance; set => _distance = value; }

        private static float _coins = 0;
        public static TimeSpan time {get;set;} = TimeSpan.Zero;
        public static float Coins { get => _coins; set => _coins = value; }
        public static int AmountOfScoresStart { get; set; } = 3;
        public static int AmountOfScoresEnd { get; set; } = 10;

        public static int buffer = screenWitdh / 200;
        public static float powerUpTime { get; set; } = durationPowerUp;
        public static Texture2D TextureBullet { get; set; }
        #endregion
    }
}
