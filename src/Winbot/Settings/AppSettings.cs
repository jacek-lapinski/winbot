using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Winbot.Annotations;
using Winbot.Notifiers;

namespace Winbot.Settings
{
    internal class AppSettings : INotifyPropertyChanged
    {
        public UserActionNotifierSetting[] Notifiers { get; }

        public AppSettings(IEnumerable<UserActionNotifier> notifiers)
        {
            Notifiers = notifiers.Select(n => new UserActionNotifierSetting(n){ Selected = n.InitiallySelected }).ToArray();
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
