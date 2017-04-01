using System.ComponentModel;
using System.Runtime.CompilerServices;
using Winbot.Annotations;
using Winbot.Notifiers;

namespace Winbot.Settings
{
    internal class UserActionNotifierSetting : INotifyPropertyChanged
    {
        public UserActionNotifier Notifier { get; }

        private bool _isSelected;
        public bool Selected
        {
            get { return _isSelected;}
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public UserActionNotifierSetting(UserActionNotifier notifier)
        {
            Notifier = notifier;
            Selected = false;
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
