
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the power Up type flag
    /// </summary>
    public class FlagObject : GameObject
    {
        /// <summary>
        /// hitbox of the flag wich covers whole screen verticly to ensure you can miss it
        /// </summary>
        public override Rectangle HitBoxRectangle => new Rectangle((int)Position.X,(int)Position.Y - GameSettings.screenHeight,
            (int)Size.X, GameSettings.screenHeight * 2);
        /// <summary>
        /// Initializes a new instance of the flag object with the specified sprite as its visual representation 
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw the flag on screen</param>
        public FlagObject(Sprite sprite) : base(sprite)
        {
            Position = Position + GameSettings.SizeOfObjects - Size; //make sure the flag pole is fully above ground since it is bigger than other objects
            IsPowerUp = true;
            IsMoving = true;
        }
        /// <summary>
        /// Draw's the flag's visual component to the screen
        /// </summary>
        /// <param name="spriteBatch">Needed to draw the sprite</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!IsActive)
                Visualization.Draw(spriteBatch);  //to make sure the flag stays onscreen even after IsActive is false
            base.Draw(spriteBatch);
        }
        /// <summary>
        /// moves the object, and updates the visual component. even after inactive
        /// </summary>
        public override void Update()
        {
            if (!IsActive)
            {
                UpdatePosition();    //make sure the flag stays on screen even when not active
                ApplyColorFilter();
            }
            base.Update();
        }
    }
}
