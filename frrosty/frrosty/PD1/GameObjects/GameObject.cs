
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.sprite;
using System.Collections.Generic;

namespace PD_1_Project_FrostyGame.PD1.GameObjects
{
    /// <summary>
    /// Represents a base class for all in-game objects, managing visualization, position, collision detection, and basic movement behavior.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Specifies whether the object is currently falling due to downwards.
        /// </summary>
        public bool IsFalling { get; set; }
        /// <summary>
        /// Specifies whether the object is currently moving left
        /// </summary>
        public bool IsMoving { get; set; }
        /// <summary>
        /// Indicates whether this object is currently active and should be updated and drawn
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// Determines whether the object should be treated as an enemy for game logic purposes
        /// </summary>
        public bool IsEnemy { get; protected set; }
        /// <summary>
        /// Indicates whether the object can act as a platform that other objects can stand on
        /// </summary>
        public bool IsPlatform { get; protected set; }
        /// <summary>
        /// Determines whether the object is affected by gravity
        /// </summary>
        public bool IsAffectedByGravity { get; protected set; }
        /// <summary>
        /// Determines whether the object should be treated as a power-up in game logic
        /// </summary>
        public bool IsPowerUp { get; protected set; }
        /// <summary>
        /// The visual representation of the object, such as a sprite, spritesheet, or animation
        /// </summary>
        public virtual Sprite Visualization { get; private set; }
        /// <summary>
        /// Defines the rectangle used for hit detection. Defaults to the destination rectangle of the object's visualization
        /// </summary>
        public virtual Rectangle HitBoxRectangle { get => Visualization.DestinationRectangle; }

        /// <summary>
        /// The top-left position of the object. Used to determine the object's location on the screen
        /// </summary>
        public Vector2 Position { get => Visualization.TopLeftPosition; set => Visualization.TopLeftPosition = value; }
        /// <summary>
        /// Specifies the size of the object when drawn on the screen
        /// </summary>
        public Vector2 Size { get => Visualization.RenderSize; set => Visualization.RenderSize = value; }

        /// <summary>
        /// Initializes a new instance of the GameObject class with the specified sprite as its visual representation
        /// </summary>
        /// <param name="sprite">the sprite/sheet/animation that will be used to draw on screen</param>
        public GameObject(Sprite sprite)
        {
            Visualization = sprite;
        }
        /// <summary>
        /// Updates the object's state, checks for out-of-bounds conditions,
        /// moves the object, and updates the visual component.
        /// </summary>
        public virtual void Update()
        {
            if (!IsActive) return;

            if (IsOutOfBounds())
                IsActive = false;

            UpdatePosition();
            ApplyColorFilter();

            Visualization.Update();
        }
        /// <summary>
        /// Applies a color filter to the object's visualization based on the current power-up state
        /// </summary>
        protected void ApplyColorFilter()
        {
            if (GameSettings.isPowerUpAvitve)
                Visualization.ColorOverlay = GameSettings.PowerUpColor;
            else
                Visualization.ColorOverlay = Color.White;
        }
        /// <summary>
        /// Adjusts the object's position based on its falling and movement states
        /// </summary>
        protected virtual void UpdatePosition()
        {
            if (IsFalling)
            {
                Position = new Vector2(Position.X, Position.Y + GameSettings.fallSpeed);

            }
            if (IsMoving) 
            {
                Position = new Vector2(Position.X - GameSettings.WorldVelocety,Position.Y);
            }
        }


        /// <summary>
        /// Determines whether the object is outside the bounds of the screen, excluding the right side for level placement
        /// </summary>
        /// <returns>true if the object is out of screen, false if it is still in screen</returns>
        public virtual bool IsOutOfBounds()
        {
            Rectangle position = Visualization.DestinationRectangle;                  //getting the hitboxex calculates them each time,
                                                                                    //so asking for each side would do the same calculation 4 times
            if (position.Right < 0 || position.Bottom < 0 || position.Top > GameSettings.screenHeight) //looks wheter or not the object is outside the screen, excluding the right side 
                return true;                                                                    //since thats where levels will be placed
            else
                return false;
        }
        /// <summary>
        /// Draw's the object's visual component to the screen
        /// </summary>
        /// <param name="spriteBatch">Needed to draw the sprite</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                Visualization.Draw(spriteBatch);
                if (GameSettings.isShowingHitboxes)
                {
                    DrawHitboxes(spriteBatch);
                }
            }
        }
        /// <summary>
        /// Renders the object's hitbox as an overlay for debugging purposes when hitboxes are enabled.
        /// </summary>
        /// <param name="spriteBatch">Needed to draw the rectangle</param>
        protected void DrawHitboxes(SpriteBatch spriteBatch)
        {
            //getting the hotbox calculates it each time it is called,
            //so asking for x,y,widht,height would do the same calculation 4 times 
            //so i put it in a local variable to prevent unececary calculations
            Rectangle hitbox = HitBoxRectangle;

        }
        /// <summary>
        /// Checks whether this object is colliding with another game object
        /// </summary>
        /// <param name="gameobject">the gameObject you want to check for collision with</param>
        /// <returns>true if the object given intersects with this object</returns>
        public bool CheckIfCollidingWith(GameObject gameobject)
        {
            if (gameobject.IsActive)
                return HitBoxRectangle.Intersects(gameobject.HitBoxRectangle);
            else
                return false;
        }
        /// <summary>
        /// Returns a list of all game objects that are currently colliding with this object
        /// </summary>
        /// <param name="listOfObjects">the list of objects you want to check for collision with</param>
        /// <returns>yield returns each object that intersects with this object 1 at a time</returns>
        public IEnumerable<GameObject> GetCollidingObjects(IEnumerable<GameObject> listOfObjects)
        {
            foreach (GameObject gameObject in listOfObjects)
                if (CheckIfCollidingWith(gameObject) && !Equals(gameObject))    //make sure that you dont get false positives back by cheking a object agains itself
                    yield return gameObject;                                            

        }
    }
}
