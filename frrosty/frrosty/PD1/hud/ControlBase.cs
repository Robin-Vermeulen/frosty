

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PD_1_Project_FrostyGame.PD1.hud
{
    internal abstract class ControlBase
    {
        public Point Position { get; private set; }
        public Point Size { get; private set; }
        public Color BackgroundColor { get; protected set; }
        public Color StrokeColor { get; private set; }
        public int StrokeThickness { get; set; }
        public Texture2D Sprite;
        public Rectangle DestanationRectangle { get { return new Rectangle(Position, Size); } }
        /// <summary>
        /// The rectangle representing the sprite's position and size on the screen, derived from its TopLeft and DrawnSize properties.
        /// </summary>
        public Rectangle DestinationRectangle => new Rectangle(TopLeftPosition, Size);
        /// <summary>
        /// A rectangle where the top-left is positioned at the center of the Destination Rectangle. 
        /// Used when drawing the sprite with its origin centered.
        /// </summary>
        public Point TopLeftPosition => Position - (Size / new Point(2, 2));
        public Rectangle CenteredRectangle => new Rectangle(DestinationRectangle.Center, Size);
        public ControlBase(Point position, Texture2D sprite, Point? size = null, Color? backgroundColor = null, Color? strokeColor = null, int strokeThickness = 0)
        {
            Position = position;
            Size = size ?? new Point(1, 1);
            BackgroundColor = backgroundColor ?? Color.White;
            StrokeColor = strokeColor ?? Color.Black;
            StrokeThickness = strokeThickness;
            Sprite = sprite;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, DestanationRectangle, null, Color.White);

        }
    }
}
