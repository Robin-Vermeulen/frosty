

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;


namespace PD_1_Project_FrostyGame.PD1.sprite
{
    /// <summary>
    /// Represents a sprite sheet, which allows for rendering individual sprites from a single image containing a grid of sprites.
    /// Inherits from the Sprite class and adds support for selecting a induvidual sprite from a spritesheet.
    /// </summary>
    public class SpriteSheet : Sprite
    {
        /// <summary>
        /// Stores the top-left positions of each sprite in the sprite sheet.
        /// This list is precomputed for performance and remains constant.
        /// </summary>
        protected List<Point> _spriteSourcePoints = new List<Point>();

        /// <summary>
        /// The index of the sprite to render from the sprite sheet.
        /// The top-left sprite is at index 0, and the index increments left-to-right and top-to-bottom.
        /// </summary>
        public int SpriteIndex { get; set; }
        private int _rows;
        /// <summary>
        /// The number of rows in the sprite sheet.
        /// </summary>
        public int Rows
        {
            get { return _rows; }
            private set
            {
                if (value >= 1)
                    _rows = value;
                else
                    Debug.WriteLine($"Invalid Row: {value}. Must be above 1");
            }
        }
        private int _columns;
        /// <summary>
        /// The number of columns in the sprite sheet.
        /// </summary>
        public int Columns
        {
            get { return _columns; }
            private set
            {
                if (value >= 1)
                    _columns = value;
                else
                    Debug.WriteLine($"Invalid Columns: {value}. Must be above 1");
            }
        }

        /// <summary>
        /// The dimensions of an individual sprite in the sprite sheet.
        /// This value is derived from the overall image size divided by the number of rows and columns.
        /// </summary>
        protected override Point ImageSize => new Point(SpriteImage.Width / Columns, SpriteImage.Height / Rows);
        /// <summary>
        /// The rectangle that picks the portion of the sprite sheet to render.
        /// The rectangle is determined based on the current SpriteIndex.
        /// </summary>
        public override Rectangle? SourceRectangle => new Rectangle(_spriteSourcePoints[SpriteIndex], ImageSize);

        /// <summary>
        /// Initializes a new instance of the SpriteSheet class, representing a grid of sprites within a single image.
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet image containing a grid of sprites.</param>
        /// <param name="columns">The number of columns in the sprite sheet.</param>
        /// <param name="rows">The number of rows in the sprite sheet.</param>
        /// <param name="spriteIndex">The index of the sprite to display initially (starting at 0).</param>
        /// <param name="topLeft">The top-left position of the sprite on screen. Default is (0, 0).</param>
        /// <param name="size">The size of the sprite when drawn on screen. Default is the standard object size from GameSettings.</param>
        /// <param name="rotation">The initial rotation angle of the sprite, in degrees. Default is 0.</param>
        /// <param name="rotationSpeed">The rate of rotation during updates, in degrees per update. Default is 0.</param>
        /// <param name="colorOverlay">The color filter to apply to the sprite. Default is white (no filter).</param>
        /// <param name="spriteEffect">Specifies whether the sprite should be flipped horizontally or vertically. Default is no flip.</param>
        public SpriteSheet(Texture2D spriteSheet, int columns, int rows, int spriteIndex, Vector2? topLeft = null,
            Vector2? size = null, float rotation = 0, float rotationSpeed = 0, Color? colorOverlay = null,
            SpriteEffects spriteEffect = SpriteEffects.None)
            : base(spriteSheet, topLeft, size, rotation, rotationSpeed, colorOverlay, spriteEffect)
        {
            Columns = columns;
            Rows = rows;
            PrecomputeSourcePoints();
            SpriteIndex = spriteIndex;
        }

        /// <summary>
        /// Precomputes the top-left positions of all sprites in the sprite sheet.
        /// This method calculates the coordinates for each sprite and stores them in _spriteSourcePoints.
        /// </summary>
        private void PrecomputeSourcePoints()
        {
            for (int i = 0; i < Rows * Columns; i++)
            {
                _spriteSourcePoints.Add(new Point(i % Columns * SpriteImage.Width / Columns,
                    i / Columns * (SpriteImage.Height / Rows)));
            }
        }
    }
}
