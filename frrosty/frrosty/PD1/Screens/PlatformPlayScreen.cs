
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PD_1_Project_FrostyGame.PD1.GameObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.sprite;
using System.Collections.Generic;
using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.Screens
{
    //this is scuffed as fuck but whatever
    public class PlatformPlayScreen : PlayScreen
    {
        private List<PlatForm> _ListOfplatForms = new List<PlatForm>();
        private List<GameObject> _ListOfBullets = new List<GameObject>();
        private Terain _randomT;
        private PlatForm _platForm1;
        private PlatForm _platForm2;
        private int _bulletTimer;


        public PlatformPlayScreen() : base()
        {

        }
        public override void Initialize()
        {
            _randomT = new Terain();
            _randomT = new Terain();
            _ListOfplatForms.Add(new PlatForm());
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            UserInput.Update();
            if (_ListOfBullets != null)
            {
                foreach (GameObject bullet in _ListOfBullets)
                {
                    bullet.Update();
                }
            }
            CheckCollisionBullets();
            GenerateBullets();
            if (!_avatar.HeadObject.IsActive)
            {
                //MediaPlayer.Pause();
                GameSettings.activeScreen = GameSettings.endScreen = new EndScreen(Content);
            }
            if (UserInput.HasPressedKey(Keys.P))
            {
                if (Paused)
                    Paused = false;
                else
                    Paused = true;
            }
            if (!Paused)
            {

                if (_ListOfplatForms.MaxBy(k => k.ListOfObjects.First().Position.X).ListOfObjects.First().Position.X < GameSettings.screenWitdh / 2 && _ListOfplatForms.Count < 3)
                {
                    _ListOfplatForms.Add(new PlatForm());
                }

                ListOfAllObjectsNearAvatar.Clear();
                foreach (VerticalSliceOFTerain slice in _randomT.ListOfTerainSlices)
                {
                    AllCoins = AllCoins.Union(slice.ListOfTerainObjects.OfType<CoinObject>()).ToList();
                    ListOfAllObjectsNearAvatar = ListOfAllObjectsNearAvatar.Union(slice.ListOfTerainObjects).ToList();
                }
                _randomT.Update();
                Logic.AddTerain(_randomT);
                Logic.removeterain(_randomT);
                foreach (PlatForm platforn in _ListOfplatForms)
                {
                    platforn.Update();
                }
                foreach(PlatForm platform in _ListOfplatForms) 
                {
                ListOfAllObjectsNearAvatar = ListOfAllObjectsNearAvatar.Union(platform.ListOfObjects).ToList();

                }
                Logic.SyncCoins(AllCoins);

                _ListOfBullets.RemoveAll(o => !o.IsActive);
                _ListOfplatForms.RemoveAll(o => !o.ListOfObjects.MaxBy(i => i.Position.X).IsActive);
                base.Update(gameTime);
            }
        }

        private void CheckCollisionBullets()
        {
            foreach (GameObject avatarPart in _avatar.ListOfAllAvatarParts)
            {
                foreach (GameObject Bullet in _ListOfBullets)
                {
                    if (avatarPart.CheckIfCollidingWith(Bullet))
                    {
                        avatarPart.IsActive = false;
                        Bullet.IsActive = false;
                    }
                }
            }
        }

        private void GenerateBullets()
        {
            if (_bulletTimer++ > GameSettings.BulletCoolDown && UserInput.HasPressedLeftMouseButton())
            {
                _bulletTimer = 0;
                foreach (PlatForm platform in _ListOfplatForms)
                {
                    if (platform.ListOfObjects.Max(o => o.Position.X) > _avatar.HeadObject.Position.X)
                    {
                        Sprite bullet = new Sprite(GameSettings.TextureBullet, platform.ListOfObjects.MaxBy(o => o.Position.X).Position, new Vector2(15, 15));
                        _ListOfBullets.Add(new Bullet(bullet, _avatar.HeadObject.Position));
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBackGround(spriteBatch);
            _randomT.Draw(spriteBatch);
            foreach (PlatForm platfom in _ListOfplatForms)
            {
                platfom.Draw(spriteBatch);
            }
            DrawForGround(spriteBatch);
            if (_ListOfBullets != null)
            {
                foreach (GameObject bullet in _ListOfBullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }
        }
    }
}
