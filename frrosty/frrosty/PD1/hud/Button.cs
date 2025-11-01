
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;
using System;
using System.Linq;

namespace PD_1_Project_FrostyGame.PD1.hud
{
    internal class Button : TextControl
    {
        private Color _backgroundColor;
        public Button(string text, SpriteFont font, Color textColor, Point position, Point size, Color backgroundColor, Color strokeColor, int strokeThickness, Texture2D sprite)
            : base(text, font, textColor, position, size, backgroundColor, strokeColor, strokeThickness, sprite) 
        {
            _backgroundColor = backgroundColor;
        }

        public void Update() 
        {
            if (DestanationRectangle.Contains(UserInput.CurrentMouseState.Position))
                
                BackgroundColor = Color.Gray;
            else BackgroundColor = _backgroundColor;
        }
        public bool IsPressed()
        {
            if (DestanationRectangle.Contains(UserInput.CurrentMouseState.Position) && UserInput.HasPressedLeftMouseButton()) return true;
            return false;
        }
    }
}
