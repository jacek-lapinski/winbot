using System;
using System.Windows.Forms;
using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class MouseUpNotifier : UserActionNotifier
    {
        private MouseGlobalHook _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Up";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = new MouseGlobalHook();
            _notifier.MouseUp += OnMouseUp;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseUp -= OnMouseUp;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseUp = new MouseUp
            {
                Time = time,
                Button = e.Button,
                X = e.X,
                Y = e.Y
            };

            Notify(mouseUp);
        }

        ~MouseUpNotifier()
        {
            Clean();
        }
    }
}
