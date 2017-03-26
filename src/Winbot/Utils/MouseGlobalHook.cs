using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Winbot.Utils
{
    internal class MouseGlobalHook : IDisposable
    {
        public event EventHandler<MouseEventArgs> MouseClicked;
        public event EventHandler<MouseEventArgs> MouseDoubleClicked;

        private readonly IntPtr _mouseHandle;
        private bool _disposed;

        public MouseGlobalHook()
        {
            _mouseHandle = WindowsHookHelper.SetWindowsHookEx(WH_MOUSE_LL, KeyboardHookDelegate, IntPtr.Zero, 0);
        }

        private IntPtr KeyboardHookDelegate(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                var arg = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseEventArgs mouseEventArgs;
                switch ((int)wParam)
                {
                    case WM_LBUTTONDBLCLK:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 2, arg.pt.x, arg.pt.y, 0);
                        OnMouseDoubleClicked(mouseEventArgs);
                        break;
                    case WM_RBUTTONDBLCLK:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Right, 2, arg.pt.x, arg.pt.y, 0);
                        OnMouseDoubleClicked(mouseEventArgs);
                        break;
                    case WM_MBUTTONDBLCLK:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Middle, 2, arg.pt.x, arg.pt.y, 0);
                        OnMouseDoubleClicked(mouseEventArgs);
                        break;
                    case WM_XBUTTONDBLCLK:
                        mouseEventArgs = new MouseEventArgs(arg.mouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2, 2, arg.pt.x, arg.pt.y, 0);
                        OnMouseDoubleClicked(mouseEventArgs);
                        break;
                    case WM_LBUTTONUP:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 1, arg.pt.x, arg.pt.y, 0);
                        OnMouseClicked(mouseEventArgs);
                        break;
                    case WM_RBUTTONUP:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Right, 1, arg.pt.x, arg.pt.y, 0);
                        OnMouseClicked(mouseEventArgs);
                        break;
                    case WM_MBUTTONUP:
                        mouseEventArgs = new MouseEventArgs(MouseButtons.Middle, 1, arg.pt.x, arg.pt.y, 0);
                        OnMouseClicked(mouseEventArgs);
                        break;
                    case WM_XBUTTONUP:
                        mouseEventArgs = new MouseEventArgs(arg.mouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2, 1, arg.pt.x, arg.pt.y, 0);
                        OnMouseClicked(mouseEventArgs);
                        break;
                }
            }

            return WindowsHookHelper.CallNextHookEx(_mouseHandle, code, wParam, lParam);
        }

        protected virtual void OnMouseClicked(MouseEventArgs e)
        {
            MouseClicked?.Invoke(this, e);
        }

        protected virtual void OnMouseDoubleClicked(MouseEventArgs e)
        {
            MouseDoubleClicked?.Invoke(this, e);
        }

        #region WinAPI
        private const int WH_MOUSE_LL = 14;

        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203;

        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_RBUTTONDBLCLK = 0x0206;

        private const int WM_MBUTTONDOWN = 0x0207;
        private const int WM_MBUTTONUP = 0x0208;
        private const int WM_MBUTTONDBLCLK = 0x0209;

        private const int WM_XBUTTONDOWN = 0x020B;
        private const int WM_XBUTTONUP = 0x020C;
        private const int WM_XBUTTONDBLCLK = 0x020D;


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
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

            if (_mouseHandle != IntPtr.Zero)
            {
                WindowsHookHelper.UnhookWindowsHookEx(_mouseHandle);
            }

            _disposed = true;
        }

        ~MouseGlobalHook()
        {
            Dispose(false);
        }
        #endregion
    }
}
