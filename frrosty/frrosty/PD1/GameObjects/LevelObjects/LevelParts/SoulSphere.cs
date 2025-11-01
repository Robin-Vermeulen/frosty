using PD_1_Project_FrostyGame.PD1.sprite;


namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the powerUp type SoulSphere
    /// </summary>
    internal class SoulSphere : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the SoulShere object with the specified sprite as its visual representation 
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the Sphere on screen</param>
        public SoulSphere(Sprite sprite) : base(sprite) 
        {
            IsPowerUp = true;
            IsMoving = true;
        }
    }
}
