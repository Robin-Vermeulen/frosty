using Microsoft.Xna.Framework.Input;
using System;

using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.General
{
    public static class UserInput
    {
        public static MouseState CurrentMouseState { get; set; } = new MouseState();

        private static MouseState _previusMouseState = new MouseState();

        private static KeyboardState _currentKeyboardState = new KeyboardState();
        private static KeyboardState _previusKeyboardState = new KeyboardState();

        private static int _counterForHolding = 0;
        private static int _treshholdForHolding = 30;
        public static void Update()
        {
            _previusMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            _previusKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        public static bool HasPressedKey(Keys key)
        {
            if (_previusKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key)) return true;
            return false;
        }
        public static bool HasPressedLeftMouseButton()
        {
            if (_previusMouseState.LeftButton == ButtonState.Pressed && CurrentMouseState.LeftButton == ButtonState.Released) return true;
            return false;
        }

        public static bool IsHoldingKey(Keys key)
        {
            if (_previusKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyDown(key)) _counterForHolding++;
            else
                _counterForHolding = 0;
            if (_counterForHolding >= _treshholdForHolding) return true;
            return false;
        }
        public static String ReadInput(String previuestext)
        {
            string text = previuestext;
            foreach (Keys k in _previusKeyboardState.GetPressedKeys())
            {
                if (HasPressedKey(k))
                    {
                    if (k == Keys.Enter)
                        return text;
                    else if (k == Keys.Back)
                    {
                        text = text.Count() > 0 ? text.Substring(0, text.Count() - 1) : text;
                    }
                    else if (k >= Keys.A && k <= Keys.Z)
                    {
                        if (_currentKeyboardState.CapsLock ^ _currentKeyboardState.GetPressedKeys().Contains(Keys.LeftShift))
                        {
                            text += k.ToString();
                        }
                        else 
                        {
                            text += (char)(k.ToString().ToCharArray()[0] + 32);
                        }

                    }
                }
            }
            return text;
        }

        internal static bool Scrollwheeldown()
        {
            if (CurrentMouseState.ScrollWheelValue < _previusMouseState.ScrollWheelValue)
                return true;
            return false;
        }

        internal static bool ScrollwheelUp()
        {
            if (CurrentMouseState.ScrollWheelValue > _previusMouseState.ScrollWheelValue)
                return true;
            return false;
        }
    }
}
