using System;
using System.Windows.Forms;
using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class MouseDownNotifier : UserActionNotifier
    {
        private MouseGlobalHook _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Down";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = new MouseGlobalHook();
            _notifier.MouseDown += OnMouseDown;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseDown -= OnMouseDown;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseDown = new MouseDown
            {
                Time = time,
                Button = e.Button,
                X = e.X,
                Y = e.Y
            };

            Notify(mouseDown);
        }

        ~MouseDownNotifier()
        {
            Clean();
        }
    }
}
