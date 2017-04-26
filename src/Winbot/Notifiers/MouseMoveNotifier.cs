using System;
using System.Windows.Forms;
using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class MouseMoveNotifier : UserActionNotifier
    {
        private MouseGlobalHook _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Move";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = new MouseGlobalHook();
            _notifier.MouseMoved += OnMouseMoved;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseMoved -= OnMouseMoved;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseMoved(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseMove = new MouseMove
            {
                Time = time,
                X = e.X,
                Y = e.Y
            };

            Notify(mouseMove);
        }

        ~MouseMoveNotifier()
        {
            Clean();
        }
    }
}
