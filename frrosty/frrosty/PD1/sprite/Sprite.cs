
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;



namespace PD_1_Project_FrostyGame.PD1.sprite
{
    /// <summary>
    /// Represents a image with configurable position, size, rotation, color overlay, and sprite effects.
    /// Provides methods for updating its rotation, changinging its size or center and rendering it to the screen.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The image representing the sprite that will be drawn on screen.
        /// </summary>
        public Texture2D SpriteImage { get; set; }
        /// <summary>
        /// The top-left position of the sprite on screen. This can be updated to move the sprite during gameplay.
        /// </summary>
        public Vector2 TopLeftPosition { get; set; }
        /// <summary>
        /// The size of the sprite when drawn on screen, where X is the width and Y is the height.
        /// </summary>
        public Vector2 RenderSize { get; set; }
        /// <summary>
        /// The rectangle representing the sprite's position and size on the screen, derived from its TopLeft and DrawnSize properties.
        /// </summary>
        public Rectangle DestinationRectangle => new Rectangle(TopLeftPosition.ToPoint(), RenderSize.ToPoint());
        /// <summary>
        /// A rectangle where the top-left is positioned at the center of the Destination Rectangle. 
        /// Used when drawing the sprite with its origin centered.
        /// </summary>
        public Rectangle CenteredRectangle => new Rectangle(DestinationRectangle.Center, RenderSize.ToPoint());
        /// <summary>
        /// The rotation angle of the sprite. 
        /// The getter returns the value in radians. 
        /// The setter expects the value in degrees.
        /// </summary>
        public float Rotation { get; private set; }
        /// <summary>
        /// The increment to the angle of the sprite. 
        /// The getter returns the value in radians. 
        /// The setter expects the value in degrees.        
        /// </summary>
        public float RotationIncrement { get; private set; }
        /// <summary>
        /// The color overlay applied to the sprite when rendered. Default is white (no color filter).
        /// </summary>
        public Color ColorOverlay { get; set; } //public so that you can change the color when getting a powerUp
        /// <summary>
        /// Specifies whether the sprite should be flipped horizontally, vertically, or not at all.
        /// </summary>
        public SpriteEffects SpriteEffect { get; private set; }
        /// <summary>
        /// The dimensions of the original image. 
        /// This is difrent from DrawnSize, which determines how large the sprite appears on screen.
        /// </summary>
        protected virtual Point ImageSize => new Point(SpriteImage.Width, SpriteImage.Height);
        /// <summary>
        /// Specifies the portion of the original image to draw. 
        /// If null, the entire image will be used. 
        /// Can be overridden in child classes to use specific regions of a image.
        /// </summary>
        public virtual Rectangle? SourceRectangle => null;

        /// <summary>
        /// will create a instance of the sprie class, only a Texture2D is required the rest are optional
        /// </summary>
        /// <param name="image">The image to use for the sprite.</param>
        /// <param name="topLeft">The initial top-left position of the sprite on screen. Default is (0, 0).</param>
        /// <param name="size">The initial size of the sprite when drawn. Default is the standard object size from GameSettings.</param>
        /// <param name="rotation">The initial rotation angle of the sprite, in degrees. Default is 0.</param>
        /// <param name="rotationSpeed">The rate at which the sprite rotates during updates, in degrees per update. Default is 0.</param>
        /// <param name="colorOverlay">The initial color filter to apply to the sprite. Default is white (no filter).</param>
        /// <param name="spriteEffect">Specifies if the sprite should be flipped horizontally or vertically. Default is no flip.</param>
        public Sprite(Texture2D image, Vector2? topLeft = null,
            Vector2? size = null, float rotation = 0, float rotationSpeed = 0,
            Color? colorOverlay = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            SpriteImage = image;
            SpriteEffect = spriteEffect;
            Rotation = MathHelper.ToRadians(rotation);
            RotationIncrement = MathHelper.ToRadians(rotationSpeed);

            TopLeftPosition = topLeft ?? Vector2.Zero;                      //if no position is given set it to 0,0
            RenderSize = size ?? GameSettings.SizeOfObjects;         //if no size is given set it to the default size
            ColorOverlay = colorOverlay ?? Color.White;             //if no color is given set it to white(nothing)
        }
        /// <summary>
        /// Updates the sprite's rotation using the specified RotationIncrement value.
        /// Can be overridden in derived classes to add custom behavior.

        /// </summary>
        public virtual void Update()
        {
            Rotation += RotationIncrement;
        }

        /// <summary>
        /// Renders the sprite to the screen using the specified SpriteBatch. 
        /// </summary>
        /// <param name="spriteBatch">Needs a SpriteBatch in order to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(ImageSize.X / 2f, ImageSize.Y / 2f);
            spriteBatch.Draw(SpriteImage, CenteredRectangle, SourceRectangle,       //CenteredRectangle Ensures the sprite is centered within its destination rectangle.
                ColorOverlay, Rotation, origin, SpriteEffect, 0);
        }
    }
}
