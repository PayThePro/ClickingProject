using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject
{
    public class Types
    {
        public enum InputType : byte
        {
            Click,
            Move,
            Press
        }

        public enum MouseButton : byte
        {
            Left,
            Right,
            Middle
        }
    }
}
