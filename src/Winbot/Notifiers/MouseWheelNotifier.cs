using System;
using System.Windows.Forms;
using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class MouseWheelNotifier : UserActionNotifier
    {
        private MouseGlobalHook _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Wheel";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = new MouseGlobalHook();
            _notifier.MouseWheel += OnMouseWheel;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseWheel -= OnMouseWheel;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseWheel = new MouseWheel
            {
                Time = time,
                Delta = e.Delta
            };

            Notify(mouseWheel);
        }

        ~MouseWheelNotifier()
        {
            Clean();
        }
    }
}
