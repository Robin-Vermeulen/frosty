
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.General;


namespace PD_1_Project_FrostyGame.PD1.hud
{
    internal abstract class TextControl : ControlBase
    {
        public string Text { get; set; }
        public SpriteFont Font { get; private set; }
        public Color TextColor { get; private set; }
        public TextControl(string text, SpriteFont font, Color textColor, Point position, Point size, Color backgroundColor, Color strokeColor, int strokeThickness,Texture2D sprite)
            : base(position, sprite, size, backgroundColor, strokeColor, strokeThickness)
        {
            Text = text;
            Font = font;
            TextColor = textColor;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 textLenght = Font.MeasureString(Text);
            int textwitdh = (int)((Size.X / GameSettings.scale - textLenght.X * 2) / 2);
            int textHeight = (int)((Size.Y / GameSettings.scale - textLenght.Y * 2) / 2);
            Vector2 textCenter = new Vector2(textwitdh, textHeight);
            spriteBatch.DrawString(Font, Text, Position.ToVector2() + (textCenter), TextColor,0f,Vector2.Zero,2 * GameSettings.scale,SpriteEffects.None,0);
        }
    }
}
