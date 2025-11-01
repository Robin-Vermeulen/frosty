using Microsoft.Xna.Framework;
using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the envirement type Spikes
    /// </summary>
    public class SpikesObject : GameObject
    {
        /// <summary>
        /// hit box of the spike object
        /// </summary>
        public override Rectangle HitBoxRectangle   //i want the snowballs to apear to be stuck IN the spikes not ON it so i halfed the hitbox
        {
            get => new Rectangle(
            (int)(Position.X + Size.X / 4),
            (int)(Position.Y + Size.Y / 4),
            (int)Size.X / 2, (int)Size.Y / 2);
        }

        /// <summary>
        /// Initializes a new instance of the Spike object with the specified sprite as its visual representation 
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the Spikes on screen</param>
        public SpikesObject(Sprite sprite) : base(sprite)
        {
            IsEnemy = true;
            IsPlatform = false;
            IsMoving = true;

        }
    }
}
