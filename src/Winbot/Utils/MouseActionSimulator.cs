using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Winbot.Utils
{
    internal static class MouseActionSimulator
    {
        public static void MoveTo(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MouseUp(MouseButtons button, int x, int y)
        {
            SetCursorPos(x, y);
            switch (button)
            {
                case MouseButtons.Left:
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.Right:
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.Middle:
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.XButton1:
                    mouse_event(MOUSEEVENTF_XUP, 0, 0, XBUTTON1, 0);
                    break;
                case MouseButtons.XButton2:
                    mouse_event(MOUSEEVENTF_XUP, 0, 0, XBUTTON2, 0);
                    break;
                default:
                    return;
            }
        }

        public static void MouseDown(MouseButtons button, int x, int y)
        {
            SetCursorPos(x, y);
            switch (button)
            {
                case MouseButtons.Left:
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    break;
                case MouseButtons.Right:
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    break;
                case MouseButtons.Middle:
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    break;
                case MouseButtons.XButton1:
                    mouse_event(MOUSEEVENTF_XDOWN, 0, 0, XBUTTON1, 0);
                    break;
                case MouseButtons.XButton2:
                    mouse_event(MOUSEEVENTF_XDOWN, 0, 0, XBUTTON2, 0);
                    break;
                default:
                    return;
            }
        }

        public static void MouseClick(MouseButtons button, int x, int y)
        {
            SetCursorPos(x, y);
            switch (button)
            {
                case MouseButtons.Left:
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.Right:
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.Middle:
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    break;
                case MouseButtons.XButton1:
                    mouse_event(MOUSEEVENTF_XDOWN, 0, 0, XBUTTON1, 0);
                    mouse_event(MOUSEEVENTF_XUP, 0, 0, XBUTTON1, 0);
                    break;
                case MouseButtons.XButton2:
                    mouse_event(MOUSEEVENTF_XDOWN, 0, 0, XBUTTON2, 0);
                    mouse_event(MOUSEEVENTF_XUP, 0, 0, XBUTTON2, 0);
                    break;
                default:
                    return;
            }
        }

        #region WinAPI
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_XDOWN = 0x0080;
        private const int MOUSEEVENTF_XUP = 0x0100;
        private const int XBUTTON1 = 0x0001;
        private const int XBUTTON2 = 0x0002;
        #endregion
    }
}
