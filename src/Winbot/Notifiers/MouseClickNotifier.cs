using System;
using Winbot.Entities;
using System.Windows.Forms;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class MouseClickNotifier : UserActionNotifier
    {
        private MouseGlobalHook _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Click";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = new MouseGlobalHook();
            _notifier.MouseClicked += OnMouseClick;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseClicked -= OnMouseClick;

            _notifier.Dispose();
            _notifier = null;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var mouseClick = new MouseClick
            {
                Time = time,
                Button = e.Button,
                X = e.X,
                Y = e.Y
            };
        
            Notify(mouseClick);
        }

        ~MouseClickNotifier()
        {
            Clean();
        }
    }
}
