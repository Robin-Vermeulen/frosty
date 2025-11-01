using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the Power Up type bird
    /// </summary>
    public class CoinObject : GameObject
    {
        public new SpriteAnimation Visualization { get; set; } //hiding gameobjects sprite so that i can sync all the coins animation in the logic class, without needing to cast the sprite to animation each time
        public static int SpriteIndex { get; set; }
        /// <summary>
        /// Initializes a new instance of the Coin class with the specified sprite as its visual representation 
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the Coin on screen</param>
        public CoinObject(Sprite sprite) : base(sprite)
        {
            IsPowerUp = true;
            IsMoving = true;
            Visualization = (SpriteAnimation)sprite;
        }
    }
}
