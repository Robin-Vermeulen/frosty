using System;
using System.Collections.Generic;

using PD_1_Project_FrostyGame.PD1.GameObjects.Avatar;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.hud;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;

using PD_1_Project_FrostyGame.PD1.GameObjects;
using PD_1_Project_FrostyGame.PD1.sprite;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PD_1_Project_FrostyGame.PD1.Screens
{
    public abstract class PlayScreen : Screen
    {
        private SimplifiedTextBox _scoreAndDistanceTextBox;
        private SimplifiedTextBox _coinsTextBox;

        private Sprite _background;
        private Sprite _background2;

        protected AvatarObject _avatar;
        public static bool Paused = false;
        protected List<GameObject> ListOfAllObjectsNearAvatar { get; set; } = new List<GameObject>();
        protected List<CoinObject> AllCoins { get; set; } = new List<CoinObject>();
        public PlayScreen()
        {
            Initialize();
            LoadContent();
        }
        public override void Initialize()
        {
            GameSettings.Score = 0;
            GameSettings.Coins = 0;
            GameSettings.Distance = 0;
            GameSettings.time = TimeSpan.Zero;
            GameSettings.WorldVelocety = GameSettings.OriginalSpeed;
            GameSettings.scorerMultiplier = 1;
            GameSettings.isPowerUpAvitve = false;
            Logic.LevelCounter = 0;
            if (MediaPlayer.State != MediaState.Playing)
                //MediaPlayer.Play(GameSettings.PlaySong);//, new TimeSpan(0, 0, 5));

            GameSettings.TextureActivePlayBackground = GameSettings.TexturePlayBackgroud;
            _background = new Sprite(GameSettings.TextureActivePlayBackground, size: new Vector2(GameSettings.screenWitdh, GameSettings.screenHeight));
            _background2 = new Sprite(GameSettings.TextureActivePlayBackground, new Vector2(_background.DestinationRectangle.Right, _background.TopLeftPosition.Y),
                new Vector2(GameSettings.screenWitdh, GameSettings.screenHeight));

            MakeTextBoxes();
            _avatar = new AvatarObject();
        }

        private void MakeTextBoxes()
        {
            _scoreAndDistanceTextBox = new SimplifiedTextBox($"score: {(int)GameSettings.Score}  X{GameSettings.scorerMultiplier}\n{(int)GameSettings.Distance}m",
                            GameSettings.font, Color.Black, new Point(GameSettings.screenWitdh / 2 - GameSettings.PlayScreenScoresBoxSize.X / 2, GameSettings.buffer),
                            new Point((int)(GameSettings.PlayScreenScoresBoxSize.X), GameSettings.PlayScreenScoresBoxSize.Y), Color.White, Color.Black, 3, GameSettings.TextureButton);

            _coinsTextBox = new SimplifiedTextBox($"coins: {GameSettings.Coins}", GameSettings.font, Color.Black,
                new Point(GameSettings.screenWitdh / 2 - GameSettings.PlayScreenCoinBoxSize.X / 2,
                GameSettings.screenHeight - GameSettings.PlayScreenCoinBoxSize.Y - GameSettings.buffer), new Point(GameSettings.PlayScreenCoinBoxSize.X, GameSettings.PlayScreenCoinBoxSize.Y), Color.White, Color.Black, 3, GameSettings.TextureButton);
        }

        public override void LoadContent()
        {


        }
        public override void Update(GameTime gameTime)
        {
            _avatar.Update();
            if (GameSettings.isPowerUpAvitve)
                GameSettings.powerUpTime--;

            if (!_avatar.HeadObject.IsActive)
            {
                //MediaPlayer.Pause();
                GameSettings.activeScreen = GameSettings.endScreen = new EndScreen(Content);
            }
            _background.SpriteImage = GameSettings.TextureActivePlayBackground;
            _background2.SpriteImage = GameSettings.TextureActivePlayBackground;

            Logic.checkIfBodyPartsAreFalling(_avatar.ListOfAllAvatarParts, ListOfAllObjectsNearAvatar);
            Logic.DetectIfgotHit(_avatar.ListOfAllAvatarParts, ListOfAllObjectsNearAvatar);
            Logic.PickUpPowerUps(_avatar.ListOfAllAvatarParts, ListOfAllObjectsNearAvatar);
            Logic.UpdateScore(gameTime);

            Logic.CheckIfPoweUpHasEnded();
            UpdateScoreAndCoinText();
            MoveBackground();

        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBackGround(spriteBatch);

            DrawForGround(spriteBatch);
        }

        protected void DrawBackGround(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _background2.Draw(spriteBatch);
        }

        protected void DrawForGround(SpriteBatch spriteBatch)
        {
            _avatar.Draw(spriteBatch);


            _scoreAndDistanceTextBox.Draw(spriteBatch);
            _coinsTextBox.Draw(spriteBatch);
            if (GameSettings.isPowerUpAvitve)
            {
                float totalDuration = GameSettings.durationPowerUp;
                float remainingTime = GameSettings.powerUpTime;

                // Ensure remainingTime does not go below 0
                remainingTime = Math.Max(0f, remainingTime);

                // Calculate the proportion of the circle remaining
                float timeRatio = remainingTime / totalDuration;

                // Start and end angles for the arc
                float startAngle = MathHelper.ToRadians(90);
                float fullCircleAngle = MathHelper.ToRadians(360);

                // Calculate the current end angle based on the remaining time
                float currentEndAngle = startAngle + (timeRatio * fullCircleAngle);

                // Draw the arc

            }
        }

        private void MoveBackground()
        {
            if (_background.DestinationRectangle.Right < 0) //if outside the screen move it to the right side of the next background
                _background.TopLeftPosition = new Vector2(_background2.DestinationRectangle.Right - 1, _background.TopLeftPosition.Y);
            else
                _background.TopLeftPosition = new Vector2(_background.TopLeftPosition.X - 1, _background.TopLeftPosition.Y);
            if (_background2.DestinationRectangle.Right < 0)
                _background2.TopLeftPosition = new Vector2(_background.DestinationRectangle.Right - 1, _background2.TopLeftPosition.Y);
            else
                _background2.TopLeftPosition = new Vector2(_background2.TopLeftPosition.X - 1, _background2.TopLeftPosition.Y);
        }

        private void UpdateScoreAndCoinText()
        {
            _scoreAndDistanceTextBox.Text = $"score: {(int)GameSettings.Score}  X{GameSettings.scorerMultiplier}\n{(int)GameSettings.Distance}m";
            _coinsTextBox.Text = $"coins: {GameSettings.Coins}\nTime: {GameSettings.time.Minutes}:{GameSettings.time.Seconds}";
        }
    }
}
