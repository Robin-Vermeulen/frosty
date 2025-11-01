

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PD_1_Project_FrostyGame.PD1.sprite
{
    public class SpriteAnimation : SpriteSheet
    {
        #region propperties
        private int _CurrentSpriteFrameCount;
        private int _startSpriteIndex;
        private int _endSpriteIndex;
        /// <summary>
        /// Wich sprite index to start the animation, top left is 0, goes from left to right going over every row
        /// </summary>
        public int StartSpriteIndex
        {
            get => _startSpriteIndex;
            private set
            {
                if (value >= 0 && value < _spriteSourcePoints.Count && value <= EndSpriteIndex)
                {
                    _startSpriteIndex = value;
                }
            }
        } 
        /// <summary>
        /// Wich sprite index to end the animation, top left is 0, goes from left to right going over every row
        /// </summary>
        public int EndSpriteIndex
        {
            get => _endSpriteIndex;
            private set
            {
                if (value >= 0 && value < _spriteSourcePoints.Count && value >= StartSpriteIndex)
                {
                    _endSpriteIndex = value;
                }
            }
        }


        /// <summary>
        /// how many frames it stays on each sprite of the animation
        /// </summary>
        public int FramesBetweenSprite { get; private set; }

        #endregion
        /// <summary>
        /// will create a instance of the SpriteAnimation class, will draw a sprite at a given location and size with a rotation(degrees) and rotating speed using given spriteSheet info and cycle trough sprites between the 2 given indexes
        /// </summary>
        /// <param name="spriteSheet">wich spritesheet will be used to draw on screen</param>
        /// <param name="Columns">how many collums the spriteSheet has</param>
        /// <param name="rows">how many rows the spriteSheet has</param>
        /// <param name="startSpriteIndex">Wich sprite index to start the animation, top left is 0, goes from left to right going over every row</param>
        /// <param name="endSpriteIndex">Wich sprite index to end the animation, top left is 0, goes from left to right going over every row</param>
        /// <param name="topLeft">what position to draw the sprite on</param>
        /// <param name="size">the width and height to draw the sprite</param>
        /// <param name="framesBetweenSprites">how many frames it stays on each sprite of the animation</param>
        /// <param name="rotation">what angle(degrees) the sprite will be rotated at when drawing</param>
        /// <param name="rotationSpeed">how much the sprite rotates each update</param>
        /// <param name="colorOverlay">what color filter to put over the sprite</param>
        /// <param name="spriteEffect">what effect to use when drawing(none, flip horizontal, flip vertical)</param>
        public SpriteAnimation(Texture2D spriteSheet, int Columns, int rows, int startSpriteIndex, int endSpriteIndex, int framesBetweenSprites,
        Vector2? topLeft = null, Vector2? size = null, float rotation = 0, float rotationSpeed = 0, Color? colorOverlay = null, SpriteEffects spriteEffect = SpriteEffects.None)
            : base(spriteSheet, Columns, rows, startSpriteIndex, topLeft, size, rotation, rotationSpeed, colorOverlay, spriteEffect)
        {
            FramesBetweenSprite = framesBetweenSprites;
            _startSpriteIndex = startSpriteIndex;
            _endSpriteIndex = endSpriteIndex;
        }
        /// <summary>
        /// updates the current sprite which will be drawn and its rotation
        /// </summary>
        public override void Update()
        {
            if (++_CurrentSpriteFrameCount >= FramesBetweenSprite)
            {
                if (SpriteIndex++ >= EndSpriteIndex) 
                    SpriteIndex = StartSpriteIndex;
                _CurrentSpriteFrameCount = 0;
            }
            base.Update();
        }
    }
}
