using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the envirement type Iceblock
    /// </summary>
    public class IceBlockObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the IceBlock object with the specified sprite as its visual representation 
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the block on screen</param>
        public IceBlockObject(Sprite sprite) : base(sprite) 
        {
            IsPlatform = true;
            IsMoving = true;
        }
    }
}
