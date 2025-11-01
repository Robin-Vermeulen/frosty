using Microsoft.Xna.Framework;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.Screens;
using PD_1_Project_FrostyGame.PD1.sprite;
using System;


namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects
{
    public class Bullet : GameObject
    {
        public Vector2 velocety;
        public Bullet(Sprite sprite, Vector2 headPosition) : base(sprite)
        {
            IsEnemy = true;
            float directionX = headPosition.X - sprite.TopLeftPosition.X;
            float directionY = headPosition.Y - sprite.TopLeftPosition.Y;
            velocety = new Vector2((directionX) / MathF.Sqrt(MathF.Pow(directionX, 2) - MathF.Pow(directionY, 2)), (directionY) / MathF.Sqrt(MathF.Pow(directionX, 2) - MathF.Pow(directionY, 2)));
        }
        public override void Update()
        {
            if (!IsActive) return;

            if (IsOutOfBounds())
                IsActive = false;

            ApplyColorFilter();

            Visualization.Update();
            if (PlayScreen.Paused)
                Position += velocety * (GameSettings.BulletSpeed - GameSettings.WorldVelocety);
            else
                Position += velocety * (GameSettings.BulletSpeed + GameSettings.WorldVelocety);

        }

    }
}
