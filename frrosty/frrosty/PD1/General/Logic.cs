using System;
using System.Collections.Generic;
using System.Linq;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;


namespace PD_1_Project_FrostyGame.PD1.General
{
    public static class Logic
    {
        private static CoinObject staticCoin = new CoinObject(Factory.CreateCoinSprite(new Vector2(0, 0))) { IsMoving = false};
        public static int PowerUpCounter { get; set; }
        public static int LevelCounter { get; set; }

        public static void Update()
        {

        }
        public static void ChangeVolume()
        {
            if (UserInput.ScrollwheelUp())
                GameSettings.Volume += GameSettings.Volumeincrement;
            else if (UserInput.Scrollwheeldown())
                GameSettings.Volume -= GameSettings.Volumeincrement;

            GameSettings.Volume = MathHelper.Clamp(GameSettings.Volume, 0, 1);
            //MediaPlayer.Volume = GameSettings.Volume;
        }

        internal static string GrabScores(int amountOfScores)
        {
            string text = "";
            if (GameSettings.ListOfScores != null)
            {
                PlayerScore.OrderScore();

                for (int i = 0;
                    i < GameSettings.ListOfScores.Take(amountOfScores).Count();
                    i++)
                {
                    text += $"{i + 1}| {GameSettings.ListOfScores[i]}";
                    if (i < amountOfScores - 1)
                        text += "\n";
                }
            }
            return text;
        }
        public static void checkIfBodyPartsAreFalling(List<GameObject> avatarParts, List<GameObject> ListOfAllLevels)
        {
            bool IsFaling;
           List<bool>listOfBools = ListOfAllLevels.Select(o => o.IsActive).ToList();
            foreach (GameObject obj in avatarParts)
            {
                IsFaling = true;
                foreach (GameObject frostyobject in obj.GetCollidingObjects(ListOfAllLevels.Union(avatarParts)).Where(e => e.IsPlatform))
                {
                    if (obj.HitBoxRectangle.Bottom < frostyobject.HitBoxRectangle.Center.Y - frostyobject.HitBoxRectangle.Size.Y / 4)
                    {
                        IsFaling = false;
                        obj.Position = new Vector2(obj.Position.X, frostyobject.Position.Y - obj.HitBoxRectangle.Size.Y);
                    }
                }
                obj.IsFalling = IsFaling;
            }
        }
        public static List<GameObject> GetobjectsNearAvatar(List<Level> ListOfAllLevels)
        {
            List<GameObject> objects = new List<GameObject>();
            foreach (Level level in ListOfAllLevels.Where(i => i.IsNearavatar))
            {
                foreach (GameObject frostyObject in Level.YieldGetAllObjects(level))
                {
                    if (frostyObject != null)
                        objects.Add(frostyObject);
                }
            }
            return objects;
        }
        /// <summary>
        /// checks if a part of the avatar got hit by a enemy
        /// </summary>
        public static void DetectIfgotHit(List<GameObject> avatarParts, List<GameObject> ListOfObjects)
        {
            foreach (GameObject obj in avatarParts)
            {
                if (obj.GetType() == typeof(TopHatObject))
                {
                    if (obj.GetCollidingObjects(ListOfObjects).OfType<GameObject>().Any(o => o.IsPlatform && !o.IsEnemy))
                    {
                        TopHatObject hat = obj as TopHatObject;
                        hat.IsTouchingPlatform = true;
                    }
                    else
                    {
                        TopHatObject hat = obj as TopHatObject;
                        hat.IsTouchingPlatform = false;
                    }

                }
                else
                {
                    foreach (GameObject frostyObject in obj.GetCollidingObjects(ListOfObjects))
                    {
                        if (frostyObject.IsPlatform)
                        {
                            if (obj.Position.X < frostyObject.Position.X && obj.HitBoxRectangle.Bottom > frostyObject.HitBoxRectangle.Center.Y - GameSettings.SizeOfObjects.Y / 2.5f)
                            {
                                obj.IsActive = false;
                                GameSettings.SoundSnow.Play(GameSettings.Volume, (float)Random.Shared.Next(-5, 10) / 100, 0.5f);
                            }
                        }
                        if (frostyObject.IsEnemy)
                        {
                            if (obj is SnowballObject && frostyObject is SpikesObject)
                            {
                                SnowballObject snowball = obj as SnowballObject;
                                snowball.IsFalling = false;
                                snowball.IsMoving = true;
                            }
                            else
                            {
                                obj.IsActive = false;
                                GameSettings.SoundSnow.Play(GameSettings.Volume, (float)Random.Shared.Next(-5, 10) / 100, 0.5f);
                            }
                        }
                    }
                }
            }
        }
        public static void PickUpPowerUps(List<GameObject> avatarParts, List<GameObject> ListOfAllLevels)
        {
            bool hitFlag = false;
            foreach (GameObject avatarObject in avatarParts)
            {
                if (avatarObject.GetType() != typeof(TopHatObject))
                {

                    foreach (GameObject powerUpObject in avatarObject.GetCollidingObjects(ListOfAllLevels).Where(p => p.IsPowerUp))
                    {
                        if (powerUpObject.GetType() == typeof(CoinObject)) //TO DO: move this to each object class
                        {
                            PickUpCoin(powerUpObject);
                        }
                        if (powerUpObject.GetType() == typeof(SoulSphere))
                        {
                            PickUpSphere(powerUpObject);
                        }
                        if (powerUpObject.GetType() == typeof(FlagObject))
                        {
                            hitFlag = true;
                            PassFlag(avatarParts, powerUpObject);
                        }
                    }
                }
            }
            if (hitFlag)
                avatarParts.RemoveAll(o => o is SnowballObject);
        }
        private static void PassFlag(List<GameObject> avatarParts, GameObject powerUpObject)
        {
            GameSettings.Score += 100 * avatarParts.Where(o => o.GetType() == typeof(SnowballObject)).Count() * GameSettings.scorerMultiplier;
            GameSettings.WorldVelocety += 0.5f * GameSettings.scale;
            GameSettings.FlagCount++;
            powerUpObject.IsActive = false;
        }

        private static void PickUpSphere(GameObject powerUpObject)
        {
            PowerUpCounter = 0;
            powerUpObject.IsActive = false;
            GameSettings.powerUpTime = GameSettings.durationPowerUp;
            GameSettings.isPowerUpAvitve = true;
            GameSettings.scorerMultiplier += 2;
            GameSettings.TextureActivePlayBackground = GameSettings.TexturePowerUpBackgroud;
            GameSettings.SoundPower.Play(Math.Clamp(GameSettings.Volume * 2, 0, 1), -0.5f, 0.5f);
            //GameSettings.WhereInSong = MediaPlayer.PlayPosition;
            //MediaPlayer.Stop();
            //MediaPlayer.Play(GameSettings.powerUppSong);//, new TimeSpan(0, 0, 38));

        }

        private static void PickUpCoin(GameObject powerUpObject)
        {
            CoinObject coin = powerUpObject as CoinObject;
            GameSettings.Coins++;
            GameSettings.Score += 50 * GameSettings.scorerMultiplier;
            coin.IsActive = false;
            GameSettings.SoundCoin.Play(GameSettings.Volume, (float)Random.Shared.Next(-5, 5) / 100, 0.5f);
        }
        public static void CreateLevels(List<Level> ListOfAllLevels)
        {
            while (ListOfAllLevels.Count < 4)
            {
                if (LevelCounter++ > GameSettings.ammountOflevelsBeforeFlag)
                {
                    LevelCounter = 0;
                    ListOfAllLevels.Add(new Level(Level.LevelSelection.Flag, new Vector2(ListOfAllLevels.Last().ListOfLevelObjects.MaxBy(o => o.Position.X).Visualization.DestinationRectangle.Right, 0)));
                }
                else
                {
                    ListOfAllLevels.Add(new Level(new Vector2(ListOfAllLevels.Last().ListOfLevelObjects.MaxBy(o => o.Position.X).Visualization.DestinationRectangle.Right, 0)));
                }
            }
        }
        public static void removeLevels(List<Level> ListOfAllLevels)
        {
            ListOfAllLevels.OfType<GameObject>().ToList().RemoveAll(o => LevelCounter-- > 0);
                ListOfAllLevels.RemoveAll(o => o.ListOfLevelObjects.OfType<IceBlockObject>().Max(x => x.Visualization.DestinationRectangle.Right) < 0);
            
        }
        public static void CheckIfPoweUpHasEnded()
        {
            if (GameSettings.isPowerUpAvitve)
            {
                if (PowerUpCounter++ >= GameSettings.durationPowerUp)
                {
                    DisablePowerUP();
                }
            }
        }

        private static void DisablePowerUP()
        {
            GameSettings.isPowerUpAvitve = false;
            GameSettings.scorerMultiplier = 1;
            //MediaPlayer.Stop();
            //MediaPlayer.Play(GameSettings.PlaySong);//, GameSettings.WhereInSong);
            GameSettings.TextureActivePlayBackground = GameSettings.TexturePlayBackgroud;
        }
        public static void UpdateScore(GameTime time)
        {
            GameSettings.Score += 0.05f * GameSettings.scorerMultiplier;
            GameSettings.Distance += GameSettings.WorldVelocety / 100;
            GameSettings.time = GameSettings.time.Add(time.ElapsedGameTime);
        }

        public static void removeterain(Terain terain)
        {
            terain.ListOfTerainSlices.RemoveAll(o => !o.ListOfTerainObjects[0].IsActive);
        }

        public static void AddTerain(Terain terain)
        {
            if (terain.ListOfTerainSlices.Count < (GameSettings.screenWitdh / GameSettings.SizeOfObjects.X) + GameSettings.SizeOfObjects.X)
            {
                terain.AddSlice(terain.ListOfTerainSlices.Last().Xcoordinate + (int)GameSettings.SizeOfObjects.X);
            }
        }
        /// <summary>
        /// uses a random to get a int between 0 and the range, if its lower than chance then you win
        /// </summary>
        /// <param name="chance">higher number higher chance of winning</param>
        /// <param name="range">higher number lower change of wining</param>
        /// <returns>returns true if the random picked lower than the changce</returns>
        public static bool IsRandomChanceSuccessful(int chance, int range)
        {
            return Random.Shared.Next(0, range) < chance;
        }
        public static void SyncCoins(List<CoinObject> listOfAllCoins) 
        {
            staticCoin.Update();
            foreach (CoinObject coin in listOfAllCoins) 
            {
                coin.Visualization.SpriteIndex = staticCoin.Visualization.SpriteIndex;
            }
        }
    }

}
