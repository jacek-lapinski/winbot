using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Winbot.Utils
{
    internal class KeyboardGlobalHook : IDisposable
    {
        public event EventHandler<KeyEventArgs> KeyDown;
        public event EventHandler<KeyEventArgs> KeyUp;

        private readonly WindowsHookHelper.HookDelegate _delegate;
        private readonly IntPtr _keyboardHandle;
        private bool _disposed;

        public KeyboardGlobalHook()
        {
            _delegate = KeyboardHookDelegate;
            _keyboardHandle = WindowsHookHelper.SetWindowsHookEx(WH_KEYBOARD_LL, _delegate, IntPtr.Zero, 0);
        }

        private IntPtr KeyboardHookDelegate(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    var vkCode = (Keys)Marshal.ReadInt32(lParam);
                    var keyEventArgs = new KeyEventArgs(vkCode);
                    OnKeyDown(keyEventArgs);
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    var vkCode = (Keys)Marshal.ReadInt32(lParam);
                    var keyEventArgs = new KeyEventArgs(vkCode);
                    OnKeyUp(keyEventArgs);
                }
            }

            return WindowsHookHelper.CallNextHookEx(_keyboardHandle, code, wParam, lParam);
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }

        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }

        #region WinAPI
        private const Int32 WH_KEYBOARD_LL = 13;
        public static int WM_KEYDOWN = 0x0100;
        public static int WM_KEYUP = 0x0101;
        #endregion
        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (_keyboardHandle != IntPtr.Zero)
            {
                WindowsHookHelper.UnhookWindowsHookEx(_keyboardHandle);
            }

            _disposed = true;
        }

        ~KeyboardGlobalHook()
        {
            Dispose(false);
        }
        #endregion
    }
}
