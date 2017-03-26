using System;
using System.Runtime.InteropServices;

namespace Winbot.Utils
{
    internal class KeyboardGlobalHook : IDisposable
    {
       

        private readonly IntPtr _keyboardHandle;
        private bool _disposed;

        public KeyboardGlobalHook()
        {
            _keyboardHandle = WindowsHookHelper.SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookDelegate, IntPtr.Zero, 0);
        }

        private IntPtr KeyboardHookDelegate(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                
            }

            return WindowsHookHelper.CallNextHookEx(_keyboardHandle, code, wParam, lParam);
        }

        #region WinAPI
        private const Int32 WH_KEYBOARD_LL = 13;
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
