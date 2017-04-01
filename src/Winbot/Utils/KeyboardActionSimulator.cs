using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Winbot.Utils
{
    internal class KeyboardActionSimulator
    {
        public static void KeyDown(Keys keyCode)
        {
            keybd_event((byte)keyCode, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        public static void KeyUp(Keys keyCode)
        {
            keybd_event((byte)keyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        #region WinAPI
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        private const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        #endregion
    }
}
