using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PD_1_Project_FrostyGame.PD1.General;

using PD_1_Project_FrostyGame.PD1.hud;

using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.Screens
{
    public class EndScreen : Screen
    {
        private string _name;

        private Button _restartButtonLevels;
        private Button _restartButtonPlatforms;
        private SimplifiedTextBox _CurrentScoreTextBox;
        private SimplifiedTextBox _ScoreBoard;



        private bool hasSendSCcore = false;
        private Sprite _backgroundRectangle;

        public EndScreen(ContentManager content)
        {
            Content = content;


            Initialize();
            LoadContent();
        }
        public override void Initialize()
        {
            //GameSettings.SoundGameover.Play(GameSettings.Volume, 0, 0);

            CreateTextBoxes();

            _backgroundRectangle = new Sprite(GameSettings.TextureGameOverBackground, size: new Vector2(GameSettings.screenWitdh, GameSettings.screenHeight));

        }


        public override void LoadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            UserInput.Update();
            //_restartButtonPlatforms.Update();
            _restartButtonLevels.Update();
            _CurrentScoreTextBox.Update();
            //_name = _CurrentScoreTextBox.input;

            //if (UserInput.HasPressedKey(Keys.Enter) && !hasSendSCcore)
            //{
            //    hasSendSCcore = true;
            //    PlayerScore newScore = new PlayerScore(_name, (int)GameSettings.Score, (int)GameSettings.Distance, (int)GameSettings.Coins);
            //    GameSettings.ListOfScores = PlayerScore.AddScoreToList(newScore);
            //    GameSettings.ListOfScores = PlayerScore.OrderScore();

            //    Data.SerializeAndSend(GameSettings.ListOfScores, GameSettings.ScoreFile);

            //    _ScoreBoard.Text = Logic.GrabScores(GameSettings.AmountOfScoresEnd);
            //}

            if (_restartButtonLevels.IsPressed())
            {
                GameSettings.activeScreen = new LevelPlayScreen();
            }
            //if (_restartButtonPlatforms.IsPressed()) 
            //{
            //    GameSettings.activeScreen = new PlatformPlayScreen();
            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _backgroundRectangle.Draw(spriteBatch);

            _CurrentScoreTextBox.Draw(spriteBatch);

            _restartButtonLevels.Draw(spriteBatch);

        }
        private void CreateTextBoxes()
        {
            _ScoreBoard = new SimplifiedTextBox("", GameSettings.font, Color.Red,
                new Point(GameSettings.screenWitdh / 2 - GameSettings.GameOverScreenTopScoresBoxSize.X / 2, GameSettings.buffer),
                GameSettings.GameOverScreenTopScoresBoxSize, Color.Black, Color.Red, 3, GameSettings.TextureButton);

            _restartButtonLevels = new Button("Press here to restart", GameSettings.font, Color.Black,
                new Point(GameSettings.screenWitdh / 2 - GameSettings.GameOverScreenRestartBoxSize.X / 2, GameSettings.screenHeight - GameSettings.GameOverScreenRestartBoxSize.Y - GameSettings.buffer),
                new Point(GameSettings.GameOverScreenRestartBoxSize.X, GameSettings.GameOverScreenRestartBoxSize.Y), Color.White, Color.Black, 3, GameSettings.TextureButton);
            //_restartButtonPlatforms = new Button("Press here to play platforms", GameSettings.font, Color.Black,
            //    new Point(_restartButtonLevels.Position.X, _restartButtonLevels.Position.Y - GameSettings.GameOverScreenRestartBoxSize.Y - GameSettings.buffer),
            //    new Point(GameSettings.GameOverScreenRestartBoxSize.X, GameSettings.GameOverScreenRestartBoxSize.Y), Color.White, Color.Black, 3);

            _CurrentScoreTextBox = new SimplifiedTextBox($"your score: {(int)GameSettings.Score}\nyour distance:{(int)GameSettings.Distance}\nyour coints:{GameSettings.Coins}",
                GameSettings.font, Color.Red, new Point(GameSettings.screenWitdh / 2 - GameSettings.GameOverScreenAchievedScoreBoxSize.X / 2, GameSettings.buffer),
                new Point(GameSettings.GameOverScreenAchievedScoreBoxSize.X, GameSettings.GameOverScreenAchievedScoreBoxSize.Y), Color.White, Color.Red, 5, GameSettings.TextureButton);
        }
    }
}
