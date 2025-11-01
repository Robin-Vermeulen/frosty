
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.GameObjects;
using PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using PD_1_Project_FrostyGame.PD1.sprite;
using System.Collections.Generic;
using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.General
{
    public static class Factory
    {
        public static TopHatObject CreateTopHat()
        {
            Sprite sprite = new Sprite(GameSettings.TextureTopHat,
                new Vector2(GameSettings.Startposition.X - GameSettings.SizeOfObjects.X / 6, GameSettings.Startposition.Y - GameSettings.SizeOfObjects.Y),
                GameSettings.SizeOfObjects);

            return new TopHatObject(sprite);
        }
        public static HeadObject CreateHead()
        {
            Sprite sprite = new SpriteAnimation(GameSettings.TextureHead, 2, 1, 0, 1, 15, GameSettings.Startposition);
            return new HeadObject(sprite);
        }
        public static SnowballObject CreateSnowball(Vector2 position)
        {
            Sprite sprite = new Sprite(GameSettings.TextureSnowball, position);
            return new SnowballObject(sprite);
        }
        public static IceBlockObject CreateIceBlock(Vector2 position, List<GameObject> listOfSuroundingObjects)
        {
            Sprite sprite;
            if (listOfSuroundingObjects.Any(o => position.Y == o.Visualization.DestinationRectangle.Bottom && position.X == o.Position.X && !o.IsPowerUp))
                sprite = new Sprite(GameSettings.TextureUnderIceBlock, position);
            else
                sprite = new Sprite(GameSettings.TextureIceblock, position);
            return new IceBlockObject(sprite);
        }
        public static SpikesObject CreateSpike(Vector2 position, List<GameObject> listOfSuroundingObjects)
        {
            Sprite sprite;
            if (listOfSuroundingObjects.Any(o => position.Y == o.Visualization.DestinationRectangle.Bottom && position.X == o.Position.X && o.IsPlatform))
                sprite = new Sprite(GameSettings.TextureSpikes, position, spriteEffect: SpriteEffects.FlipVertically);
            else
                sprite = new Sprite(GameSettings.TextureSpikes, position);

            return new SpikesObject(sprite);
        }
        public static LavaObject CreateLava(Vector2 position)
        {
            Sprite sprite = new Sprite(GameSettings.TextureLava, position);
            return new LavaObject(sprite);
        }
        public static BirdObject CreateBird(Vector2 position)
        {
            Sprite crossHair = new Sprite(GameSettings.TextureCrossHair, position, rotationSpeed: 2);
            Sprite bird = new SpriteAnimation(GameSettings.TextureBird, 3, 1, 0, 2, 3, position, GameSettings.SizeOfObjects / 2, spriteEffect: SpriteEffects.FlipHorizontally);
            return new BirdObject(bird, crossHair);
        }
        public static GameObject CreateCoin(Vector2 position)
        {
            Sprite sprite;
            if (Logic.IsRandomChanceSuccessful(1, 90))
            {
                sprite = new SpriteAnimation(GameSettings.TexturePowerUp, 4, 1, 0, 3, 8, position);
                return new SoulSphere(sprite);
            }
            else
            {
                sprite = new SpriteAnimation(GameSettings.TextureCoin, 13, 1, 0, 12, 3, position);
                return new CoinObject(sprite);
            }
        }
        public static FlagObject CreateFlag(Vector2 position)
        {
            Sprite sprite = new Sprite(GameSettings.TextureFlag, position, new Vector2(GameSettings.SizeOfObjects.X * 2, GameSettings.SizeOfObjects.Y * 4));
            return new FlagObject(sprite);
        }
        public static SpriteAnimation CreateCoinSprite(Vector2 position)
        {
            return new SpriteAnimation(GameSettings.TextureCoin, 13, 1, 0, 12, 3, position);
        }
    }
}
