using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedProject.Types;

namespace ServerLibrary
{
    public class ClickSimulator
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public enum MouseEvent
        {
            leftDown = 0x02,
            leftUp = 0x04,
            rightDown = 0x08,
            rightUp = 0x10,
            middleDown = 0x20,
            middleUp = 0x40
        }

        public static int xPos, yPos;

        /// <summary>
        /// Input handler, made for easy script use from the server.
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="pressLenght"></param>
        /// <param name="button"></param>
        public static void InputHandler(InputType inputType, int xPos, int yPos, int pressLength, MouseButton button)
        {
            switch (inputType)
            {
                case InputType.Click:
                    Click(xPos, yPos, pressLength, button);
                    break;
                case InputType.Move:
                    MoveCursor(xPos, yPos);
                    break;
                case InputType.Press:
                    Press(pressLength, button);
                    break;
                default:
                    break;
            }
        }

        public static void Click(int x, int y, int pressLengthInMiliSec, MouseButton button)
        {
            SetCursorPos(x, y);
            Press(pressLengthInMiliSec, button);
        }

        public static void MoveCursor(int x, int y)
        {
            xPos = x;
            yPos = y;

            SetCursorPos(xPos, yPos);
        }

        public static void Press(int pressLengthInMiliSec, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    mouse_event((int)MouseEvent.leftDown, xPos, yPos, 0, 0);
                    System.Threading.Thread.Sleep(pressLengthInMiliSec);
                    mouse_event((int)MouseEvent.leftUp, xPos, yPos, 0, 0);
                    break;
                case MouseButton.Right:
                    mouse_event((int)MouseEvent.rightDown, xPos, yPos, 0, 0);
                    System.Threading.Thread.Sleep(pressLengthInMiliSec);
                    mouse_event((int)MouseEvent.rightUp, xPos, yPos, 0, 0);
                    break;
                case MouseButton.Middle:
                    mouse_event((int)MouseEvent.leftDown, xPos, yPos, 0, 0);
                    System.Threading.Thread.Sleep(pressLengthInMiliSec);
                    mouse_event((int)MouseEvent.leftUp, xPos, yPos, 0, 0);
                    break;
                default:
                    Console.Error.WriteLine("An error have occurred :)!");
                    break;
            }
        }
    }
}
