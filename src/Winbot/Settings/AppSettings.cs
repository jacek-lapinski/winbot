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
        private string _databaseFilePath;
        public string DatabaseFilePath
        {
            get { return _databaseFilePath;}
            set
            {
                _databaseFilePath = value;
                OnPropertyChanged();
            }
        }

        public UserActionNotifierSetting[] Notifiers { get; }

        public AppSettings(IEnumerable<UserActionNotifier> notifiers)
        {
            Notifiers = notifiers.Select(n => new UserActionNotifierSetting(n){ Selected = true }).ToArray();
            DatabaseFilePath = @"C:\db\winbot.db";
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
