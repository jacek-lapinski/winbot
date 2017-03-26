using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Winbot.Entities;

namespace Winbot.Notifiers
{
    internal class KeyUpNotifier : UserActionNotifier
    {
        private IKeyboardMouseEvents _notifier;
        private DateTime _referenceStartTime;

        public override string Label => "Key Up";
        public override void Start(DateTime referenceStartTime)
        {
            _referenceStartTime = referenceStartTime;
            Clean();
            _notifier = Hook.GlobalEvents();
            _notifier.KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var now = DateTime.Now;
            var time = now - _referenceStartTime;

            var keyUp = new KeyUp
            {
                Time = time,
                KeyCode = e.KeyCode
            };

            Notify(keyUp);
        }


        public override void Stop()
        {
            Clean();
        }

        private void Clean()
        {
            if (_notifier == null)
                return;

            _notifier.KeyUp -= OnKeyUp;

            _notifier.Dispose();
            _notifier = null;
        }

        ~KeyUpNotifier()
        {
            Clean();
        }
    }
}
