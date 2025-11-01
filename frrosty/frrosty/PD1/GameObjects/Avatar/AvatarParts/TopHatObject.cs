using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts
{
    /// <summary>
    /// Gameobject that represents the hat of the avatar
    /// </summary>
    public class TopHatObject : GameObject
    {
        /// <summary>
        /// determines wheter the hat is intersecting with a platform, wich determance wheter or not the avatar can add body parts
        /// </summary>
        public bool IsTouchingPlatform { get; set; }
        public TopHatObject(Sprite sprite) : base(sprite) 
        {
            IsAffectedByGravity = true;
        }
    }
}
