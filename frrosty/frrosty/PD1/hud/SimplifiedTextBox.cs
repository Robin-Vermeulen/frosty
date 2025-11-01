
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;
using System;


namespace PD_1_Project_FrostyGame.PD1.hud
{
    internal class SimplifiedTextBox : TextControl
    {
        public string input { get; set; }
        private String _beginText;
        public SimplifiedTextBox(string text, SpriteFont font, Color textColor, Point position, Point size, Color backgroundColor, Color strokeColor, int strokeThickness, Texture2D sprite)
            : base(text, font, textColor, position, size, backgroundColor, strokeColor, strokeThickness, sprite)
        {
            _beginText = text;
        }

        public void Update()
        {
            input = UserInput.ReadInput(input);
            Text = $"{_beginText}\n{input}";
        }
    }
}
