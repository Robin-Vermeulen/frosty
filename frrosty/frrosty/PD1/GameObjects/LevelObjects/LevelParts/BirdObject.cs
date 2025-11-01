
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.sprite;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts
{
    /// <summary>
    /// Gameobject that represents the enemy type bird
    /// </summary>
    public class BirdObject : GameObject
    {
        private Sprite _bird;
        public override Rectangle HitBoxRectangle => _bird.DestinationRectangle;
        /// <summary>
        /// Initializes a new instance of the bird class with the specified sprite as its visual representation 
        /// and a crossHair for alerting the player where the bird will hit the snowman
        /// </summary>
        /// <param name="bird">the sprite/sheet/animation that will be used to draw the bird on screen</param>
        /// <param name="CrossHair">the sprite/sheet/animation that will be used to draw the crosshair on screen</param>
        public BirdObject(Sprite bird, Sprite CrossHair) : base(CrossHair)
        {
            IsMoving = true;
            IsEnemy = true;
            _bird = bird;
            _bird.TopLeftPosition = _bird.TopLeftPosition + _bird.RenderSize / 2; //place it in the center of the grid
            PlaceBirdSoThatItIntersectsWithCrossHair(); //what a beauty of a method name
        }

        private void PlaceBirdSoThatItIntersectsWithCrossHair()
        {

            // Calculates the distance in pixels from the snowman's center to the right edge of the screen.
            float howFarFromSnowmanToRightSide = (GameSettings.screenWitdh - (GameSettings.Startposition.X + GameSettings.SizeOfObjects.X / 2));
            // Calculates the speed difference between the bird and the level.
            // The level moves at a speed of 1 * GameSettings.Speed, so subtracting 1 gives the relative speed difference.
            float speedDifference = GameSettings.BirdSpeedMultiplier - 1;
            // Determines the scaling factor that represents how much faster the bird moves compared to the level.
            float scalingFactor = GameSettings.BirdSpeedMultiplier / speedDifference;
            // Adjusts the bird's position to the right so that it intersects with the snowman 
            // at the same time as the crossHair does.
            _bird.TopLeftPosition = new Vector2(_bird.TopLeftPosition.X + (howFarFromSnowmanToRightSide / scalingFactor), _bird.TopLeftPosition.Y);
        }
        /// <summary>
        /// Draw's the bird and crosshair to the screen
        /// </summary>
        /// <param name="spriteBatch">Needed to draw the sprite</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _bird.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
        public override void Update()
        {
            _bird.Update();
            base.Update();
        }
        /// <summary>
        /// Adjusts the birds's position based on its multiplier, 
        /// and also move the croshair with the same speed as the levels
        /// </summary>
        protected override void UpdatePosition()
        {

            if (_bird.TopLeftPosition.X < GameSettings.screenWitdh)
                _bird.TopLeftPosition = new Vector2(_bird.TopLeftPosition.X - GameSettings.WorldVelocety * GameSettings.BirdSpeedMultiplier, _bird.TopLeftPosition.Y);
            else
                _bird.TopLeftPosition = new Vector2(_bird.TopLeftPosition.X - GameSettings.WorldVelocety, _bird.TopLeftPosition.Y);
            base.UpdatePosition();
        }
    }
}
