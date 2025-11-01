using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.hud;
using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.Screens
{
    public class StartScreen : Screen
    {
        private Button _playButtonLevels;
        //private Button _playButtonPlatforms;

        //private SimplifiedTextBox _scoreTextBox;

        private Sprite _background;
        //private AvatarObject _avatar;


        public StartScreen()
        {
            Initialize();
            LoadContent();
        }
        public override void Initialize()
        {
            //MediaPlayer.Play(GameSettings.PlaySong);
            //MediaPlayer.IsRepeating = true;

            _background = new Sprite(GameSettings.TextureStartBackgroud, size: new Vector2(GameSettings.screenWitdh, GameSettings.screenHeight));

            GameSettings.ListOfScores = Data.ReadAndDesiriliazeScores(GameSettings.ScoreFile);

            CreateTextBoxes();

            //_scoreTextBox.Text = Logic.GrabScores(GameSettings.AmountOfScoresStart);
            //_avatar = new AvatarObject();
            //_avatar.HeadObject.Position = new Vector2(GameSettings.Startposition.X, 50);
            //_avatar.TopHatObject.Position = new Vector2(GameSettings.Startposition.X - GameSettings.SizeOfObjects.X / 6, 0);
        }


        public override void LoadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            UserInput.Update();
            //_avatar.Update();
            _playButtonLevels.Update();
            //_playButtonPlatforms.Update();
            if (_playButtonLevels.IsPressed())
                GameSettings.activeScreen = new LevelPlayScreen();
            //if (_playButtonPlatforms.IsPressed())
            //    GameSettings.activeScreen = new PlatformPlayScreen();
            //if (!_avatar.HeadObject.IsActive)
            //{
            //    _avatar = new AvatarObject();
            //    _avatar.HeadObject.Position = new Vector2(GameSettings.Startposition.X, 50);
            //    _avatar.TopHatObject.Position = new Vector2(GameSettings.Startposition.X - GameSettings.SizeOfObjects.X / 6, 0);
            //}
            //foreach (GameObject obj in _avatar.ListOfAllAvatarParts)
            //    obj.IsFalling = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            //_scoreTextBox.Draw(spriteBatch);
            _playButtonLevels.Draw(spriteBatch);
            //_playButtonPlatforms.Draw(spriteBatch);
            //_avatar.Draw(spriteBatch);
        }
        private void CreateTextBoxes()
        {
            //_scoreTextBox = new SimplifiedTextBox("", GameSettings.font, Color.Black,
            //    new Point(GameSettings.buffer, GameSettings.screenHeight - GameSettings.StartScreenTopScoresBoxSize.Y - GameSettings.buffer),
            //    GameSettings.StartScreenTopScoresBoxSize, Color.White, Color.Black, 3, GameSettings.TextureButton);
            
            _playButtonLevels = new Button("PLAY", GameSettings.font, Color.Black,
                new Point(GameSettings.buffer, GameSettings.screenHeight - GameSettings.StartScreenPlayButtonSize.Y - GameSettings.buffer),
                GameSettings.StartScreenPlayButtonSize, Color.White, Color.Black, 3, GameSettings.TextureButton);

            //_playButtonPlatforms = new Button("PLAY RandomPlatforms", GameSettings.font, Color.Black,
            //    new Point(_playButtonLevels.DestanationRectangle.Left, _playButtonLevels.DestanationRectangle.Top - GameSettings.StartScreenPlayButtonSize.Y - GameSettings.buffer),
            //    GameSettings.StartScreenPlayButtonSize, Color.White, Color.Black, 3);
        }
    }
}
