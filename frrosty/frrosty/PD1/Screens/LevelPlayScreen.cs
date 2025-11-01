
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using PD_1_Project_FrostyGame.PD1.General;
using System.Collections.Generic;
using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.Screens
{

    public class LevelPlayScreen : PlayScreen
    {
        private List<Level> _listOfLevels = new List<Level>();

        public LevelPlayScreen() : base()
        {

        }
        public override void Initialize()
        {
            _listOfLevels.Clear();
            _listOfLevels.Add(new Level(Level.LevelSelection.StartLevel));

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            UserInput.Update();
            if (UserInput.HasPressedKey(Keys.P))
            {
                if (Paused)
                    Paused = false;
                else
                    Paused = true;
            }
            if (!Paused)
            {
                AllCoins.Clear();
                ListOfAllObjectsNearAvatar.Clear();
                Logic.CreateLevels(_listOfLevels);
                foreach (Level lv in _listOfLevels)
                {
                    AllCoins.Union(lv.ListOfLevelObjects.OfType<CoinObject>());
                    lv.update();
                }
                Logic.SyncCoins(AllCoins);
                ListOfAllObjectsNearAvatar = Logic.GetobjectsNearAvatar(_listOfLevels);
                Logic.removeLevels(_listOfLevels);
                base.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBackGround(spriteBatch);
            foreach (Level lv in _listOfLevels)
                lv.Draw(spriteBatch);
            DrawForGround(spriteBatch);
        }

    }
}
