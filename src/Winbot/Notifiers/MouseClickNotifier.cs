using System;
using Gma.System.MouseKeyHook;
using Winbot.Entities;
using System.Windows.Forms;

namespace Winbot.Notifiers
{
    internal class MouseClickNotifier : UserActionNotifier
    {
        private IKeyboardMouseEvents _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Mouse Click";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = Hook.GlobalEvents();
            _notifier.MouseClick += OnMouseClick;
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.MouseClick -= OnMouseClick;

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
