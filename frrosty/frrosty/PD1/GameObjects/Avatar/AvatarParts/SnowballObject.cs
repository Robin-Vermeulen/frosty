
using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts
{

    /// <summary>
    /// Gameobject that represents a bodypart of the avatar, a collection of this will b
    /// </summary>
    public class SnowballObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the SnowBallObject class with the specified sprite as its visual representation
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the body parts on screen</param>
        public SnowballObject(Sprite sprite) : base(sprite)
        {
            IsPlatform = true;
            IsAffectedByGravity = true;
        }
    }
}
