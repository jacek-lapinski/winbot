using System.Collections.Generic;
using System.Linq;
using Winbot.Notifiers;

namespace Winbot.Settings
{
    internal class AppSettings
    {
        public string DatabaseFilePath { get; set; }
        public string StartShortcut { get; set; }
        public string StopShortcut { get; set; }
        public IEnumerable<UserActionNotifierSetting> Notifiers { get; }

        public AppSettings(IEnumerable<UserActionNotifier> notifiers)
        {
            Notifiers = notifiers.Select(n => new UserActionNotifierSetting(n){ Selected = true });
            DatabaseFilePath = @"C:\db\winbot.db";
        }
    }
}
