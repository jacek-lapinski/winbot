using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Winbot.Entities;

namespace Winbot.Notifiers
{
    internal class MouseDoubleClickNotifier : UserActionNotifier
    {
        private IKeyboardMouseEvents _notifier;
        private DateTime _referenceStartTime;

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = Hook.GlobalEvents();
            _notifier.MouseDoubleClick += OnMouseDoubleClick;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseDoubleClick -= OnMouseDoubleClick;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseDoubleClick = new MouseDoubleClick
            {
                Time = time,
                Button = e.Button,
                X = e.X,
                Y = e.Y
            };

            Notify(mouseDoubleClick);
        }

        ~MouseDoubleClickNotifier()
        {
            Clean();
        }
    }
}
