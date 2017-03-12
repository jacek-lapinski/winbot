using Winbot.Notifiers;

namespace Winbot.Settings
{
    internal class UserActionNotifierSetting
    {
        public UserActionNotifier Notifier { get; }

        public bool Selected { get; set; }

        public UserActionNotifierSetting(UserActionNotifier notifier)
        {
            Notifier = notifier;
            Selected = false;
        }
    }
}
