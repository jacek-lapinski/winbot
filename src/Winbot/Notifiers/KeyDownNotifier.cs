using System;
using Winbot.Entities;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Winbot.Utils;

namespace Winbot.Notifiers
{
    internal class KeyDownNotifier : UserActionNotifier
    {
        private IKeyboardMouseEvents _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Key down";

        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = Hook.GlobalEvents();
            _notifier.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var keyDown = new KeyDown
            {
                Time = time,
                KeyCode = e.KeyCode
            };

            Notify(keyDown);
        }

        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.KeyDown -= OnKeyDown;

            _notifier.Dispose();
            _notifier = null;
        }

        ~KeyDownNotifier()
        {
            Clean();
        }
    }
}
