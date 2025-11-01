using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts
{
    /// <summary>
    /// Gameobject that represents the head of the avatar. it is used for the location to spawn new snowballs,
    /// and if destroyed will end the game
    /// </summary>
    public class HeadObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the HeadObject class with the specified sprite as its visual representation
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the head on screen</param>
        public HeadObject(Sprite sprite) : base(sprite) 
        {
            IsPlatform = true;
            IsAffectedByGravity = true;
        }
    }
}
